using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Gui.Mappers.Styles.LoadingMask;
using eDriven.Playground.Demo.Gui.Flickr;
using UnityEngine;

namespace eDriven.Playground.Demo.Gui
{
    /// <summary>
    /// Thumbnail is a box with absolute layout
    /// There are two children:
    /// 1) "Progress indicator" box with white background and [label as a child -> or just the loading mask (I have commented the image for the sake of example)]
    /// 2) The image
    /// When started loading, we are hiding the (old) image and showing the progress indicator
    /// When loading finished (setting the texture from outside) we are showing the image and hiding the progress indicator
    /// </summary>
    public class Thumbnail : Box
    {
        private Image _image;
        private Box _box;
        
        public Thumbnail()
        {
            Width = 150;
            Height = 150;
            SetStyle("cursor", "pointer");
            MouseChildren = false;
            Layout = new AbsoluteLayout();
        }

        private bool _photoChanged;
        private Photo _photo;
        public Photo Photo
        {
            get { 
                return _photo;
            }
            set
            {
                if (value == _photo)
                    return;

                _photo = value;
                _photoChanged = true;
                InvalidateProperties();
            }
        }

        private bool _textureChanged;
        private Texture _texture;
        /// <summary>
        /// Called to set the loaded texture
        /// </summary>
        public Texture Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                if (value == _texture)
                    return;
                {
                    _texture = value;
                    _textureChanged = true;
                    InvalidateProperties();
                }
            }
        }

        //private Label _anim;

        private LoadingMask _mask;

        override protected void CreateChildren()
        {
            base.CreateChildren();

            _image = new Image
                         {
                             PercentWidth = 100,
                             PercentHeight = 100,
                             HorizontalCenter = 0, // the image is centered (important when not square image)
                             VerticalCenter = 0
                         };
            AddChild(_image);

            _box = new Box { PercentWidth = 100, PercentHeight = 100, Layout = new AbsoluteLayout() };
            _box.SetStyle("showBackground", true);
            _box.SetStyle("backgroundStyle", LoadingMaskStyleProxy.Instance.Default.BoxBackgroundStyle); // blue line
            AddChild(_box);

            //_anim = new Label { Text = "Loading...", HorizontalCenter = 0, VerticalCenter = 0 };
            //_box.AddChild(_anim);
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (_photoChanged)
            {
                _photoChanged = false;
                _image.Texture = null;
                _box.Visible = true;
                Tooltip = _photo.Title;

                _mask = new LoadingMask(_box, "Loading...");
            }

            if (_textureChanged)
            {
                _textureChanged = false;
                if (null != _texture)
                {
                    _box.Visible = false;
                    _image.Texture = _texture;
                    _image.Width = _image.MinWidth = _texture.width; // resizing the image to fit the texture
                    _image.Height = _image.MinHeight = _texture.height;
                    
                    _mask.Unmask();
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _image.Texture = null;
            _mask.Unmask();
        }
    }
}