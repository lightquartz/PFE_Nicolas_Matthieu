using System.Collections;
using eDriven.Animation;
using eDriven.Audio;
using eDriven.Core.Caching;
using eDriven.Core.Events;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.DragDrop;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Components;
using eDriven.Playground.Demo.Gui.Styles;
using eDriven.Playground.Demo.Models;
using eDriven.Playground.Demo.Styles;
using eDriven.Playground.Demo.Tweens;
using UnityEngine;
using Component=eDriven.Gui.Components.Component;
using Event=eDriven.Core.Events.Event;
using LayoutDescriptor = eDriven.Gui.Layout.LayoutDescriptor;

public class DragDropDemo2 : Gui
{
    private Box _box;

    private Panel _pnlSource;
    private Panel _pnlDest;

    private readonly string[] _icons = new[]
    {
        "accessories-dictionary",
        "akonadi",
        "application-pgp",
        "application-pgp-signature",
        "applications-games",
        "applications-graphics",
        "applications-office",
        "applications-science",
        "appointment",
        "ardour",
        "battery",
        "camera-photo",
        "emblem-documents",
        "emblem-money",
        "ethereal",
        "gcrontab",
        "gtk-color-picker"
    };

    #region Effects

    private readonly TweenFactory _searchShowEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("portlet_add"); }),
            new FadeInLeft2()
        )
    ) { Delay = 0.07f };

    private readonly TweenFactory _panelShowEffect = new TweenFactory(
        new Sequence(
            new FadeIn()
        )
    ) { Delay = 0.07f };

    #endregion

    protected override void OnInitialize()
    {
        base.OnInitialize();

        OptionsModel.Instance.Volume = 0.5f;

        Stage.AutoLayout = false;
        _searchShowEffect.Callback = delegate(IAnimation anim)
        {
            Stage.AutoLayout = true;
        };

        Layout = new AbsoluteLayout();
    }

    protected override void CreateChildren()
    {
        base.CreateChildren();

        #region Heading

        Button button = new HesitantButton
        {
            HorizontalCenter = 0,
            Bottom = 10,
            FocusEnabled = false,
            Styles = new Hashtable 
                                { 
                                    {"buttonStyle", EDrivenButtonStyle.Instance} 
                                }
        };

        AddChild(button);

        new TextRotator
        {
            Delay = 5, // 5 seconds delay
            Lines = new[]
                            {
                                "Drag and Drop Demo 2", 
                                "Created using eDriven.Gui",
                                "Author: Danko Kozar",
                                "Drag items from the left panel (source)",
                                "Drop them to the right panel (destination)"
                            },
            Callback = delegate(string line) { button.Text = line; }
        }
            .Start();


        #endregion

        #region Box

        _box = new Box
                   {
                       HorizontalCenter = 0, VerticalCenter = 0,
                       LayoutDescriptor = LayoutDescriptor.HorizontalTopCenter,
                       ScrollContent = true
                   };

        // mandatory listeners
        AddEventListener(MouseEvent.MOUSE_DOWN, OnMouseDown); // mouse down
        AddEventListener(DragEvent.DRAG_ENTER, OnDragEnter); // drag enter
        AddEventListener(DragEvent.DRAG_DROP, OnDragDrop); // drag drop (on drop target)

        // optional listeners
        AddEventListener(DragEvent.DRAG_START, OnDragStart); // drag start(on drag initiator)
        AddEventListener(DragEvent.DRAG_EXIT, OnDragExit); // drag exit (on drop target)
        AddEventListener(DragEvent.DRAG_COMPLETE, OnDragComplete); // drag complete (on drag initiator)

        //_box.AddEventListener(MouseEvent.MOUSE_OVER, OnMouseOver);
        //_box.AddEventListener(MouseEvent.MOUSE_OUT, OnMouseOut);
        //_box.AddEventListener(MouseEvent.MOUSE_UP, OnMouseUp);

        AddChild(_box);

        #endregion

        #region Source

        _pnlSource = new Panel
                         {
                             Title = "Source",
                             Width = 450,
                             Height = 500,
                             ScrollContent = true,
                             MouseEnabled = true,
                             Padding = 10,
                             Layout = new TileLayout
                                          {
                                              Direction = LayoutDirection.Horizontal,
                                              HorizontalSpacing = 10,
                                              VerticalSpacing = 10
                                          }
                         };
        _pnlSource.SetStyle("addedEffect", _panelShowEffect);

        _box.AddChild(_pnlSource);

        #endregion

        #region Destination

        _pnlDest = new Panel
                       {
                           Title = "Destination",
                           Width = 450,
                           Height = 500,
                           ScrollContent = true,
                           MouseEnabled = true,
                           Layout = new TileLayout
                                        {
                                            Direction = LayoutDirection.Horizontal,
                                            HorizontalSpacing = 10,
                                            VerticalSpacing = 10
                                        },
                           Padding = 10
                       };
        _pnlDest.SetStyle("addedEffect", _panelShowEffect);

        _box.AddChild(_pnlDest);

        InitChildren();

        #endregion

        #region Toolbar

        HBox hbox = new HBox { Padding = 10, VerticalAlign = VerticalAlign.Middle };
        hbox.SetStyle("showBackground", true);
        hbox.SetStyle("backgroundStyle", SearchContainerBackgroundStyle.Instance);
        hbox.SetStyle("addedEffect", _searchShowEffect);
        AddChild(hbox);

        Button btnReset = new Button
                              {
                                  Text = "Reset",
                                  Padding = 3,
                                  PaddingLeft = 4,
                                  PaddingRight = 4,
                                  Icon = ImageLoader.Instance.Load("Icons/arrow_refresh")
                              };
        btnReset.Press += delegate { InitChildren(); };
        hbox.AddChild(btnReset);

        #endregion
    }

    private void InitChildren()
    {
        _pnlSource.RemoveAllContentChildren();
        _pnlDest.RemoveAllContentChildren();

        for (int i = 0; i < _icons.Length; i++)
        {
            Image image = new Image
                              {
                                  Texture = (Texture)Resources.Load(string.Format("Icons/{0}", _icons[i])),
                                  MouseEnabled = true,
                                  ProcessClicks = true, 
                                  Data = (i + 1) * 100
                              };
            _pnlSource.AddContentChild(image);
        }
    }

    #region Drag & drop handlers

    private void OnMouseDown(Event e)
    {
        Image comp = e.Target as Image;

        if (null == comp)
            return;

        // check if dragged item is child of _pnlSource or _pnlDest
        if (_pnlSource.ContentContains(comp) || _pnlDest.ContentContains(comp))
        {
            DragSource dataSource = new DragSource();
            //dataSource.AddData(comp.Text, "text"); // add text for COPY_TEXT mode
            //dataSource.AddData(comp.StyleName, "style"); // add text for COPY_STYLE mode
            //dataSource.AddData(comp, "control"); // add reference to control for Move mode

            Image proxy = new Image
                              {
                                  Texture = comp.Texture, // reference the same texture

                                  //// TEMP: handles the DragDropManager missing bounds clonning
                                  //Bounds = (Rectangle) comp.GlobalBounds.Clone(),

                                  //// TEMP: handles the DragDropManager missing MouseEnabled enabled turning off on the proxy
                                  //MouseEnabled = false
                              };

            DragDropManager.DoDrag(comp, dataSource, (MouseEvent)e, proxy, 0, 0, 0.5f, false);
            /*new DragOption(DragOptionType.ProxyVisible, false),
            new DragOption(DragOptionType.FeedbackVisible, false)*/
        }
    }

    private void OnDragEnter(Event e)
    {
        Debug.Log("OnDragEnter: " + e.Target.GetType().Name);
        Component comp = (Component)e.Target;
        if (null == comp)
            return;

        AudioPlayerMapper.GetDefault().PlaySound("pager_click", new AudioOption(AudioOptionType.Volume, 0.3f));

        // check if drag enter item is child of _pnlDest or _pnlSource itself
        // allow the whole box to be drop target when moving
        if (_pnlDest.ContentContains(comp) || comp == _pnlDest.ContentGroup)
        {
            DragDropManager.AcceptDragDrop((Component) e.Target);
            DragDropManager.ShowFeedback(DragDropManager.Action.Move);
        }
    }

    private void OnDragDrop(Event e)
    {
        DragEvent dragEvent = (DragEvent) e;

        //Debug.Log("OnDragDrop: " + e.Target.GetType().Name);
        Component src = dragEvent.DragInitiator; //(UiComponent)dragEvent.DragSource.Formats["control"];
        Component dest = (Component)e.Target;

        if (dest == _pnlDest.ContentGroup)
        {
            src.Parent.RemoveChild(src); //src.RemoveFromParent();
            _pnlDest.AddContentChild(src);
        }
        else if (_pnlDest.ContentContains(dest))
        {
            src.Parent.RemoveChild(src); // NOTE: needed for drag&drop, because src and dest could be the same
            _pnlDest.AddContentChildAt(_pnlDest.GetContentChildIndex(dest), src);
        }

        AudioPlayerMapper.GetDefault().PlaySound("drag_drop", new AudioOption(AudioOptionType.Volume, 0.3f));
    }

    private void OnDragStart(Event e)
    {
        //Debug.Log("OnDragStart: " + e.Target.GetType().Name);
    }

    private void OnDragComplete(Event e)
    {
        //Debug.Log("OnDragComplete: " + e.Target.GetType().Name);
    }

    private void OnDragExit(Event e)
    {
        //Debug.Log("OnDragExit: " + e.Target.GetType().Name);
    }

    #endregion

}