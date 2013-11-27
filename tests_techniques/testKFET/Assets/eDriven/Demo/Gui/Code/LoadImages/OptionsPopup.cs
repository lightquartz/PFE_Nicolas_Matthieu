using eDriven.Core.Data.Collections;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Form;
using eDriven.Gui.Plugins;
using eDriven.Playground.Demo.Gui.Styles;
using UnityEngine;
using System.Collections.Generic;

namespace eDriven.Playground.Demo.Gui
{
    public class OptionsPopup : Box //Dialog
    {
        #region Members

        private CheckBox _chkCache;
        private List _listThumbSize;
        private List _listImageSize;
        
        #endregion

        #region PanelPropertiesTitle

        private bool _cacheThumbnailsChanged;
        private bool _cacheThumbnails;
        public bool CacheThumbnails
        {
            get { 
                return _cacheThumbnails;
            }
            set
            {
                if (value == _cacheThumbnails)
                    return;
                {
                    _cacheThumbnails = value;
                    _cacheThumbnailsChanged = true;
                    InvalidateProperties();
                }
            }
        }

        private bool _thumbnailSizeChanged;
        private string _thumbnailSize;
        public string ThumbnailSize
        {
            get { 
                return _thumbnailSize;
            }
            set
            {
                if (value == _thumbnailSize)
                    return;
                
                _thumbnailSize = value;
                _thumbnailSizeChanged = true;
                InvalidateProperties();
            }
        }
        
        private bool _imageSizeChanged;
        private string _imageSize;
        public string ImageSize
        {
            get { 
                return _imageSize;
            }
            set
            {
                if (value == _imageSize)
                    return;
                
                _imageSize = value;
                _imageSizeChanged = true;
                InvalidateProperties();
            }
        }

        public Button Opener;

        #endregion

        public OptionsPopup()
        {
            Padding = 10;
            Width = 400;
            SetStyle("showBackground", true);
            SetStyle("backgroundStyle", OptionsPopupBackgroundStyle.Instance);
        }

        override protected void CreateChildren()
        {
            base.CreateChildren();

            Form form = new Form {PercentWidth = 100, Padding = 0};
            AddContentChild(form);

            // full screen checkbox
            _chkCache = new CheckBox
                                 {
                                     StyleMapper = "checkbox",
                                     Right = 10,
                                     Top = 10,
                                     Padding = 0,
                                     ResizeWithContent = true,
                                     ToggleMode = true,
                                     Selected = _cacheThumbnails
                                 };
            form.AddField("cache", "Cache thumbnails:", _chkCache);

            // resolution list
            _listThumbSize = new List
                        {
                            PercentWidth = 100,
                            MinWidth = 150,
                            RequireSelection = true,
                            DataProvider = new List<object>
                                            {
                                                new ListItem("t", "Thumbnail"),
                                                new ListItem("q", "Square"), // Large Square
                                                //new ListItem("s", "Small"),
                                                new ListItem("n", "Large") // Small 320
                                            },
                            SelectedValue = _thumbnailSize
                        };
            _listThumbSize.SelectedIndexChanged += delegate
                                              {
                                                  _thumbnailSize = (string) _listThumbSize.SelectedValue;
                                              };
            form.AddField("thumb_size", "Thumbnail size:", _listThumbSize);

            _listImageSize = new List
            {
                PercentWidth = 100,
                MinWidth = 150,
                DataProvider = new List<object>
                                            {
                                                new ListItem("m", "Medium"),
                                                new ListItem("z", "Medium 640"),
                                                //new ListItem("c", "Medium 800") // doesn't work well
                                                //new ListItem("l", "Large"), // commercial Flickr only
                                                //new ListItem("o", "Original") // commercial Flickr only
                                            },
                SelectedValue = _imageSize
            };
            _listImageSize.SelectedIndexChanged += delegate
            {
                _imageSize = (string)_listImageSize.SelectedValue;
            };
            form.AddField("image_size", "Image size:", _listImageSize);

            Plugins.Add(new TabManager());
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (_cacheThumbnailsChanged)
            {
                _cacheThumbnailsChanged = false;
                _chkCache.Selected = _cacheThumbnails;
            }

            if (_thumbnailSizeChanged)
            {
                _thumbnailSizeChanged = false;
                //Debug.Log("_thumbnailSizeChanged: " + _thumbnailSize);
                _listThumbSize.SelectedValue = _thumbnailSize;
            }

            if (_imageSizeChanged)
            {
                _imageSizeChanged = false;
                //Debug.Log("_imageSizeChanged: " + _imageSize);
                _listImageSize.SelectedValue = _imageSize;
            }
        }

        public override List<DisplayListMember> GetTabChildren()
        {
            var list = new List<DisplayListMember> { _chkCache, _listThumbSize, _listImageSize };
            if (null != Opener)
                list.Add(Opener);
            return  list;
        }

        public override void SetFocus()
        {
            _chkCache.SetFocus();
        }
    }
}