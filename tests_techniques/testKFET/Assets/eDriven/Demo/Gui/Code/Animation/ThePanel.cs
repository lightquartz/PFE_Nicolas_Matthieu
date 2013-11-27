#region License

/*
 
Copyright (c) 2012 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using System.Collections;
using eDriven.Core.Caching;
using eDriven.Core.Util;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Layout;
using eDriven.Networking.Rpc.Loaders;
using eDriven.Playground.eDrivenSite;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

namespace eDriven.Playground.eDriven.Demo.Gui.Code.Animation
{
    public class ThePanel : Panel
    {
        #region Static

        private static readonly IAsyncLoader<Texture> TextureLoader = new TextureLoader();

        private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean eget felis turpis. Curabitur venenatis augue auctor tellus fermentum consectetur. Pellentesque et purus id libero ultricies porta sed vitae mi. Sed viverra feugiat nibh, at pulvinar nisi sollicitudin congue. Morbi mauris justo, semper quis suscipit sit amet, malesuada at arcu. Nulla elementum erat at justo ornare porta. Duis eu vestibulum nunc. Aliquam lacinia, lectus nec sagittis mollis, elit urna egestas mauris, at vehicula dolor odio vel ligula. Integer laoreet venenatis ante. Curabitur bibendum varius justo, a dictum dolor bibendum a. Cras tincidunt sollicitudin odio sit amet condimentum. Ut pretium ultricies lectus, ac malesuada ipsum laoreet ut. Pellentesque non arcu est, quis mattis odio.

Curabitur adipiscing lectus at dui aliquet eget sodales justo dignissim. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ac ipsum in diam volutpat rhoncus quis quis mauris. Aliquam erat volutpat. Vestibulum sagittis mi quis quam blandit fermentum. Fusce urna urna, facilisis ut ornare non, facilisis et nisl. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam at ipsum massa, nec interdum nisi. Nunc lacinia mattis neque, vel ullamcorper nisi aliquam sed. Quisque aliquet viverra nisl, nec vehicula nisl bibendum nec. ";

        #endregion

        #region Members

        private TextField _txtBody;

        private VBox _vbox;
        private Label _lblLibs;
        private Button _btnCore;
        private Button _btnGui;
        private Image _imgPicture;

        private LoadingMask _mask;

        #endregion

        #region Properties

        public string ImageUrl;

        #endregion

        #region Constructor

        public ThePanel()
        {
            Title = "Panel";
            Padding = 10;
            MinWidth = 300;
            //MinHeight = 300;
            TitleLabel.Texture = ImageLoader.Instance.Load("Icons/monitor");
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            Layout = new BoxLayout {Direction = LayoutDirection.Horizontal, HorizontalSpacing = 10};
        }

        protected override void CreationComplete()
        {
            base.CreationComplete();

            _mask = new LoadingMask(_imgPicture);
            _mask.SetMessage("Loading...");
            TextureLoader.Load(ImageUrl, OnTextureLoaded);
        }

        private void OnTextureLoaded(Texture resource)
        {
            //Debug.Log("OnTextureLoaded: " + resource);
            Image = resource;
            _mask.Unmask();
        }

        override protected void CreateChildren()
        {
            base.CreateChildren();

            #region Tools

            var btnClose = new Button
                               {
                                   Icon = ImageLoader.Instance.Load("Icons/close_16"),
                                   Width = 20,
                                   Height = 20,
                                   FocusEnabled = false
                               };
            Tools.AddChild(btnClose);

            #endregion
            
            _vbox = new VBox
                        {
                            MinWidth = 150,
                            PercentHeight = 100,
                            VerticalAlign = VerticalAlign.Top,
                            HorizontalAlign = HorizontalAlign.Center,
                            VerticalSpacing = 4//, Padding = 0
                        };
            AddContentChild(_vbox);

            _imgPicture = new Image
                              {
                                  MinWidth = 150, // border
                                  MinHeight = 150,
                                  Tooltip = "The image"
                              };
            _imgPicture.SetStyle("imageStyle", ImageStyle.Instance);
            _imgPicture.SetStyle("cursor", "pointer");
            _vbox.AddChild(_imgPicture);

            Hashtable linkStyles = new Hashtable { { "cursor", "pointer" } };

            #region Libs

            _lblLibs = new Label {Text = "Featured libraries: "};
            _vbox.AddChild(_lblLibs);

            _btnCore = new Button
                           {
                               Text = "eDriven.Core",
                               StyleMapper = "miki",
                               Styles = linkStyles,
                               MinWidth = 110,
                               Tooltip = "Click to find out more",
// ReSharper disable RedundantNameQualifier
                               Icon = ImageLoader.Instance.Load("Icons/compress")
// ReSharper restore RedundantNameQualifier
                           };
            _btnCore.SetStyle("buttonStyle", GreenButtonStyle.Instance);
            _btnCore.Click += BtnCoreClickHandler;
            _vbox.AddChild(_btnCore);

            _btnGui = new Button
                          {
                              Text = "eDriven.Gui",
                              StyleMapper = "miki",
                              Styles = linkStyles,
                              MinWidth = 110,
                              Tooltip = "Click to find out more",
                              Icon = ImageLoader.Instance.Load("Icons/compress")
                          };
            _btnGui.SetStyle("buttonStyle", PurpleButtonStyle.Instance);
            _btnGui.Click += BtnGuiClickHandler;
            _vbox.AddChild(_btnGui);

            #endregion

            #region Text Field

            _txtBody = new TextField
                           {
                               Multiline = true,
                               ScrollContent = true,
                               MinWidth = 300,
                               Width = 300,
                               PercentHeight = 100,
                               //Editable = false,
                               Optimized = true,
                               Text = LoremIpsum
                           };
            _txtBody.SetStyle("showOverlay", true);
            AddContentChild(_txtBody);

            #endregion
        }

        private bool _imageChanged;
        private Texture _image;
        public Texture Image
        {
// ReSharper disable UnusedMember.Global
            get
// ReSharper restore UnusedMember.Global
            {
                return _image;
            }
            set
            {
                if (value == _image)
                    return;
                {
                    _image = value;
                    _imageChanged = true;
                    InvalidateProperties();
                }
            }
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (_imageChanged)
            {
                _imageChanged = false;
                _imgPicture.Texture = _image;
            }
        }

        private static void BtnCoreClickHandler(Event e)
        {
            Alert.Show("Info", "Core clicked", AlertButtonFlag.Ok);
        }

        private static void BtnGuiClickHandler(Event e)
        {
            Alert.Show("Info", "Gui clicked", AlertButtonFlag.Ok);
        }

        public override void Dispose()
        {
            base.Dispose();

            _btnCore.Click -= BtnCoreClickHandler;
            _btnGui.Click -= BtnGuiClickHandler;
        }
    }
}