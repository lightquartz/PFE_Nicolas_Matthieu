using eDriven.Core.Events;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Gui.Managers;

namespace eDriven.Playground.Demo.Gui.ChildControls
{
    /// <summary>
    /// A custom list row
    /// </summary>
    public class ListRow : HBox
    {
        #region Private members

        private CheckBox _chkSelected;
        private TextField _txtText;

        #endregion

        #region PanelPropertiesTitle

        private bool _textChanged;
        private string _text;
        /// <summary>
        /// Label text
        /// </summary>
        public string Text
        {
            get
            {
                if (null != _txtText)
                    return _txtText.Text;

                return _text;
            }
            set
            {
                _text = value;
                _textChanged = true;
                InvalidateProperties();
            }
        }

        private bool _selectedChanged;
        private bool _selected;
        /// <summary>
        /// Checkbox selected state
        /// </summary>
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value == _selected)
                    return;

                _selected = value;
                _selectedChanged = true;
                InvalidateProperties();
            }
        }

        private bool _tooltipChanged;
        /// <summary>
        /// Tooltip of the component as a whole
        /// This is the example of overriding the superclass property and using the property invalidation
        /// </summary>
        public override string Tooltip
        {
            get
            {
                return base.Tooltip;
            }
            set
            {
                base.Tooltip = value;
                _tooltipChanged = true;
                InvalidateProperties();
            }
        }

        #endregion

        #region Lifecycle methods

        /// <summary>
        /// Initializing the component
        /// This method runs after constructor, but before child creation
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            VerticalAlign = VerticalAlign.Middle;
            PaddingLeft = 6;
            PaddingRight = 24;
            PaddingTop = 4;
            PaddingBottom = 4;
        }

        /// <summary>
        /// Child creation
        /// </summary>
        protected override void CreateChildren()
        {
            base.CreateChildren();

            _chkSelected = new CheckBox { FocusEnabled = false, StyleMapper = "checkbox" };
            _chkSelected.Change += CheckboxChangeHandler; // subscribe to the change event
            AddChild(_chkSelected);

            _txtText = new TextField { PercentWidth = 100, Enabled = _selected };
            AddChild(_txtText);
        }

        /// <summary>
        /// After the properties are being invalidated, this method will run
        /// It is guaranteed that by this time all of the children have been created (CreateChildren method already run)
        /// </summary>
        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (_selectedChanged)
            {
                _selectedChanged = false;
                _chkSelected.Selected = _selected;
                _txtText.Enabled = _selected; // let's toggle the enable state of text field, just for fun
            }

            if (_textChanged)
            {
                _textChanged = false;
                _txtText.Text = _text;
            }

            if (_tooltipChanged)
            {
                _tooltipChanged = false;
                // applying component tooltip to child components
                _chkSelected.Tooltip = Tooltip + " (Checkbox)";
                _txtText.Tooltip = Tooltip + " (TextField)";
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Checkbox change handler
        /// </summary>
        /// <param name="e"></param>
        private void CheckboxChangeHandler(Event e)
        {
            CheckBox cb = e.Target as CheckBox;
            if (null == cb)
                return;

            Selected = cb.Selected; // setting Selected here, tu run the invalidation and to trigger the text field enabled state

            if (cb.Selected)
            {
                _txtText.SetFocus(); // focus text field (just for fun)
            }
            else
            {
                // let's unfocus text field it previously focused
                if (FocusManager.Instance.HasFocusedComponent(_txtText))
                    FocusManager.Instance.Blur(_txtText);
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Just a good practice:
        /// In event-driven programming, we should always clean up our stuff
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            _chkSelected.Change -= CheckboxChangeHandler; // detach event handler
        }

        #endregion

    }
}