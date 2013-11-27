using System;
using System.Collections.Generic;
using eDriven.Animation;
using eDriven.Audio;
using eDriven.Core.Caching;
using eDriven.Core.Control.Keyboard;
using eDriven.Core.Data.Collections;
using eDriven.Core.Events;
using eDriven.Core.Geom;
using eDriven.Core.Util;
using eDriven.Gui;
using eDriven.Gui.Animation;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Cursor;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Layout;
using eDriven.Gui.Managers;
using eDriven.Gui.Plugins;
using eDriven.Networking.Rpc;
using eDriven.Networking.Rpc.Loaders;
using eDriven.Playground.Demo.Gui;
using eDriven.Playground.Demo.Gui.Flickr;
using eDriven.Playground.Demo.Gui.Styles;
using eDriven.Playground.Demo.Helpers;
using eDriven.Playground.Demo.Models;
using eDriven.Playground.Demo.Tweens;
using JsonFx.Json;
using UnityEngine;
using Action=eDriven.Animation.Action;
using Component=eDriven.Gui.Components.Component;
using Event=eDriven.Core.Events.Event;

public class LoadImages : Gui
{
    #region PanelPropertiesTitle

    // exposed to editor
    public string FlickrAppKey = "19a0493934d366a8d534c578e9385b49"; // please use this API key for this demo only!
    public string FlickrSearchUrl = "http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&page={2}&per_page={3}&format=json&nojsoncallback=1";
    public bool CacheThumbnails;
    public string ThumbnailSize = "q";
    public string ImageSize = "z";

    #endregion

    #region Loaders

    private readonly HttpConnector _httpConnector = new HttpConnector {ResponseMode = ResponseMode.WWW}; // loads data
    private readonly TextureLoader _thumbnailLoader = new TextureLoader { Cache = false }; // loads textures
    private readonly TextureLoader _imageLoader = new TextureLoader {Cache = false}; // loads textures

    #endregion

    #region Effects

    private readonly TweenFactory _searchShowEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("portlet_add"); }),
            new FadeInLeft2(),
            new Action(delegate(IAnimation anim) { ((Component)anim.Target).Visible = true; })
        )
    ) { Delay = 1f };
    
    private readonly TweenFactory _thumbShowEffect = new TweenFactory(
        new Sequence(
            new FadeInLeft2()
        )
    ) { Delay = 0.07f };

    private readonly TweenFactory _popupShowEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("dialog_open"); }), // dialog_open
            new FadeIn { Duration = 0.6f }
        )
    );
    private readonly TweenFactory _modalOverlayShowEffect = new TweenFactory(new ZeroFadeIn { Duration = 0.35f });
    private readonly TweenFactory _dialogAddedEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("dialog_open"); }), // dialog_open
            new FadeIn2 { Duration = 0.35f }
        )
    );

    private readonly TweenFactory _dialogRemovedEffect = new TweenFactory(
        new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("dialog_close"); })
    );

    private readonly TweenFactory _optionsAddedEffect = new TweenFactory(
        new Sequence(
            new FadeIn { Duration = 0.35f }
        )
    );

    #endregion

    #region Members

    private Box _box;
    private TextField _txtSearch;
    private VBox _popup;
    private Texture _texture;
    private Pager _pager;
    private ComboBox _combo;
    private Button _btnOptions;
    private Button _btnSearch;

    private OptionsPopup _optionsPopup;

    private readonly TileLayout _layout = new TileLayout(LayoutDirection.Horizontal) { HorizontalSpacing = 10, VerticalSpacing = 10 };

    private readonly ObjectPool<Thumbnail> _pool = new ObjectPool<Thumbnail>();

    private readonly Dictionary<string, Point> _thumbSizes = new Dictionary<string, Point>
    {
        {"q", new Point(150, 150)},
        {"t", new Point(100, 75)},
        {"s", new Point(240, 180)},
        {"n", new Point(320, 240)},
    };

    private bool _newSearch;

    #endregion

    protected override void OnInitialize()
    {
        base.OnInitialize();

        _thumbnailLoader.Connector.MaxConcurrentRequests = 3;
        _thumbnailLoader.Connector.ConcurencyMode = ConcurencyMode.Multiple;

        _imageLoader.Connector.MaxConcurrentRequests = 1;
        _imageLoader.Connector.ConcurencyMode = ConcurencyMode.Multiple;
        _imageLoader.Connector.ProcessingMode = ProcessingMode.Sync;

        ModalOverlay.AddedEffect = _modalOverlayShowEffect;
        Dialog.AddedEffect = _dialogAddedEffect;
        Dialog.RemovedEffect = _dialogRemovedEffect;

        Stage.AutoLayout = false;
        _searchShowEffect.Callback = delegate(IAnimation anim)
        {
            Stage.AutoLayout = true;
        };

        KeyboardMapper.Instance.Map(
            new KeyCombination(KeyboardEvent.KEY_DOWN, KeyCode.Escape, false, false, false),
            delegate
            {
                if (null != _popup)
                {
                    PopupManager.Instance.RemovePopup(_popup);
                }

                if (PopupManager.Instance.HasPopup(_optionsPopup))
                {
                    PopupManager.Instance.RemovePopup(_optionsPopup);
                    _btnOptions.SetFocus();
                }
            }
        );

        OptionsModel.Instance.Volume = 0.5f;

        Timer timer = new Timer(3, 1); // wait 3 seconds...
        timer.Start();
        timer.Complete += delegate { iTween.LookTo(Camera.main.gameObject, iTween.Hash("looktarget", new Vector3(0, 10, 1), "time", 3, "easetype", iTween.EaseType.easeInOutQuad)); }; // then look at the sky
    }

    protected override void CreateChildren()
    {
        base.CreateChildren();

        VBox vbox = new VBox { Padding = 10, Visible = false };
        vbox.SetStyle("showBackground", true);
        vbox.SetStyle("backgroundStyle", SearchContainerBackgroundStyle.Instance);
        vbox.SetStyle("addedEffect", _searchShowEffect);
        AddChild(vbox);

        HBox hbox = new HBox { HorizontalSpacing = 10, VerticalAlign = VerticalAlign.Middle, CircularTabs = true, CircularArrows = false };
        hbox.Plugins.Add(new TabManager { ArrowsEnabled = true });
        vbox.AddChild(hbox);

        Label lbl = new Label
        {
            Text = "Flickr photo search:"
        };
        lbl.SetStyle("labelStyle", SearchLabelStyle.Instance);
        hbox.AddChild(lbl);

        #region Text field

        _txtSearch = new TextField
                         {
                             Text = "croatian coast",
                             FocusEnabled = true,
                             Width = 400
                         };
        _txtSearch.SetFocus();
        _txtSearch.KeyUp += delegate (Event e)
                                {
                                    KeyboardEvent ke = (KeyboardEvent) e;
                                    if (ke.KeyCode == KeyCode.Return)
                                    {
                                        _newSearch = true; 
                                        Search();
                                    }
                                        
                                };
        hbox.AddChild(_txtSearch);

        #endregion

        #region Combo

        _combo = new ComboBox
                     {
                         Width = 140,
                         DataProvider = new List<object>
                                            {
                                                new ListItem(10, "10 items per page"),
                                                new ListItem(20, "20 items per page"),
                                                new ListItem(30, "30 items per page"),
                                                new ListItem(40, "40 items per page"),
                                                new ListItem(50, "50 items per page"),
                                                new ListItem(60, "60 items per page"),
                                                new ListItem(80, "80 items per page"),
                                                new ListItem(100, "100 items per page"),
                                                new ListItem(1000, "Fit to screen"),
                                            },
                         SelectedValue = 1000 //SelectedIndex = 1 // default: 20 items per page
                         
                     };
        hbox.AddChild(_combo);

        #endregion

        #region Options popup

        // creating options dialog (not adding it to the display list, this will become a popup)
        _optionsPopup = new OptionsPopup
        {
            //Callback = OptionsHandler,
            CacheThumbnails = CacheThumbnails, // filling in the defaults
            ThumbnailSize = ThumbnailSize,
            ImageSize = ImageSize
        };
        _optionsPopup.SetStyle("addedEffect", _optionsAddedEffect);
        _optionsPopup.SetStyle("removedEffect", null);

        #endregion

        #region Options Button

        _btnOptions = new Button
                          {
                              //Text = "Options",
                              StyleMapper = "miki",
                              Padding = 2,
                              Icon = ImageLoader.Instance.Load("Icons/cog")
                          };
        _btnOptions.Press += delegate
        {
             _optionsPopup.ValidateNow(); // forcing measure (important for the first addition)

             /**
             * The example of creating a non-modal popup
             * We want to display our dialog just below the button (the combo box effect)
             * */
             Point globalCoords = _btnOptions.LocalToGlobal(new Point(_btnOptions.Width, _btnOptions.Height)); // global button coordinates
             _optionsPopup.X = globalCoords.X - _optionsPopup.Width;
             _optionsPopup.Y = globalCoords.Y;
             PopupManager.Instance.AddPopup(_optionsPopup, 
                 new PopupOption(PopupOptionType.Modal, false), // non modal
                 new PopupOption(PopupOptionType.Centered, false), // not centered
                 new PopupOption(PopupOptionType.RemoveOnMouseDownOutside, true),
                 new PopupOption(PopupOptionType.RemoveOnMouseWheelOutside, true),
                 new PopupOption(PopupOptionType.RemoveOnScreenResize, true)
             );
        };
        hbox.AddChild(_btnOptions);

        //_optionsPopup.Opener = _btnOptions; // just for tab management

        #endregion

        #region Button

        _btnSearch = new Button
                         {
                             Text = "Search",
                             StyleMapper = "miki",
                             Padding = 2, PaddingLeft = 4, PaddingRight = 4,
                             Icon = ImageLoader.Instance.Load("Icons/magnifier")
                         };
        _btnSearch.Press += delegate
                                {
                                    _newSearch = true;
                                    Search();
                                };
        hbox.AddChild(_btnSearch);

        #endregion

        #region Pager

        _pager = new Pager { Visible = false, IncludeInLayout = false, FocusEnabled = false, MaxButtons = 20 };
        _pager.AddEventListener(IndexChangeEvent.SELECTED_INDEX_CHANGED, PageChangedHandler);
        vbox.AddChild(_pager);

        #endregion

        _box = new Box
                   {
                       PercentWidth = 100,
                       PercentHeight = 100,
                       Layout = _layout, //LayoutDescriptor = LayoutDescriptor.TileListHorizontal,
                       ScrollContent = true,
                       MouseEnabled = true
                   };

        _box.Click += delegate (Event e)
                          {
                              Thumbnail thumb = e.Target as Thumbnail;
                              if (null == thumb)
                                  return;

                              LoadBigPhoto(thumb.Photo);
                          };

        _thumbShowEffect.Callback = delegate { _box.AutoLayout = true; }; // reverts to auto layout after the effect finished playing

        AddChild(_box);
    }

    private void PageChangedHandler(Event e)
    {
        if (_newSearch)
            return;

        Search();
    }

    private Point GetOuterThumbSize()
    {
        var size = _thumbSizes[_optionsPopup.ThumbnailSize];
        var s = Mathf.Max(size.X, size.Y);
        return new Point(s, s);
    }

    private void Search()
    {
        if (_newSearch)
        {
            _pager.CurrentPageIndex = 0;
            _newSearch = false;
        }

        var size = GetOuterThumbSize();

        string url = string.Format(
            FlickrSearchUrl, // http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=19a0493934d366a8d534c578e9385b49&text=croatian+island&format=json&nojsoncallback=1&page=2&per_page=20
            FlickrAppKey,
            _txtSearch.Text.Replace(" ", "+"),
            _pager.CurrentPageIndex + 1,
            (int) _combo.SelectedValue == 1000 ?
                Math.Min(TileHelper.CalculateItemsPerPage(size.X, size.Y, _box.Width, _box.Height, _layout.HorizontalSpacing, _layout.VerticalSpacing), 150) : 
                _combo.SelectedValue
        );

        // make a Flickr request
        GlobalLoadingMask.Show("Searching...");
        var cursor = CursorManager.Instance.SetCursor(CursorType.Wait);
        _httpConnector.Send(
            url,
            delegate (object data)
            {
                GlobalLoadingMask.Hide();
                CursorManager.Instance.RemoveCursor(cursor);
                DisplayPhotos(data);
            }
        );
    }

    private void DisplayPhotos(object data)
    {
        _box.Children.ForEach(delegate (DisplayListMember child)
                                  {
                                      child.Dispose(); // unregisters handlers
                                      _pool.Put(child as Thumbnail);
                                  });
        _box.RemoveAllChildren();
        _thumbShowEffect.Stop();

        string json = ((WWW)data).text; //Debug.Log(json);

        try // deserialize
        {
            PhotosSearchResponse response = (PhotosSearchResponse)JsonReader.Deserialize(json, typeof(PhotosSearchResponse));
            if (response.Stat != "ok")
            {
                Alert.Show("Error", response.Message, AlertButtonFlag.Ok);
                return;
            }

            _pager.TotalPages = response.Stat == "ok" ? response.Photos.Pages : 1;
            _pager.Visible = _pager.IncludeInLayout = _pager.TotalPages > 1;

            // load images
            response.Photos.Photo.ForEach(delegate(Photo photo)
            {
                Thumbnail thumb = _pool.Get(); // new Thumbnail();
                thumb.Visible = false; // because of effects
                thumb.Photo = photo;

                var size = GetOuterThumbSize();
                thumb.Width = size.X;
                thumb.Height = size.Y;

                _box.AddChild(thumb);

                _thumbnailLoader.Cache = _optionsPopup.CacheThumbnails;
                _thumbnailLoader.Load(photo.GetUrl(_optionsPopup.ThumbnailSize), delegate(Texture texture)
                   {
                       thumb.Texture = texture;
                   }
                );
            });

            _box.ValidateNow();
            _box.AutoLayout = false; // turn off auto layout because of the effect
            _thumbShowEffect.Play(_box.Children); // play effect
        }
        catch (Exception)
        {
            _pager.Visible = _pager.IncludeInLayout = false;
            Alert.Show("Error", "No images found", AlertButtonFlag.Ok);
        }
    }

    private void LoadBigPhoto(Photo photo)
    {
        GlobalLoadingMask.Show("Loading image...");
        var cursor = CursorManager.Instance.SetCursor(CursorType.Wait);

        _imageLoader.Load(photo.GetUrl(_optionsPopup.ImageSize), delegate(Texture texture2)
         {
             _popup = new VBox { VerticalSpacing = 0 };
             _popup.SetStyle("showBackground", true);

             // dispose the old texture
             DestroyImmediate(_texture, true);

             _texture = texture2;

             Image img = new Image
                             {
                                 Texture = _texture,
                                 Tooltip = photo.Title
                             };
             _popup.AddChild(img);

             Label label = new Label { Text = photo.Title };
             _popup.AddChild(label);
             _popup.SetStyle("cursor", "pointer");
             _popup.SetStyle("addedEffect", _popupShowEffect);
             _popup.Click += delegate
             {
                 PopupManager.Instance.RemovePopup(_popup);
             };

             PopupManager.Instance.AddPopup(_popup,
                new PopupOption(PopupOptionType.Modal, true),
                new PopupOption(PopupOptionType.Centered, true),
                new PopupOption(PopupOptionType.KeepCenter, true),
                new PopupOption(PopupOptionType.RemoveOnMouseDownOutside, true),
                new PopupOption(PopupOptionType.RemoveOnMouseWheelOutside, true),
                new PopupOption(PopupOptionType.RemoveOnScreenResize, true)
             );

             GlobalLoadingMask.Hide();
             CursorManager.Instance.RemoveCursor(cursor);
         });
    }
}