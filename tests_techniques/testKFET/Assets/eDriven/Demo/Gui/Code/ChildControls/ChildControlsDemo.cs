using System.Text;
using eDriven.Core.Caching;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Gui.ChildControls;

public class ChildControlsDemo : Gui
{
    private Button _btnAddRect;

    //private readonly TweenFactory _tweenFactory = new TweenFactory(new Sequence(
    //        new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("portlet_add"); }), // dialog_open
    //        new FadeIn2 { Duration = 0.35f }
    //    ));

    private int _count;

    private VBox _vbox;

    override protected void CreateChildren()
    {
        base.CreateChildren();

        #region Controls

        HBox hbox = new HBox
        {
            X = 10,
            Y = 10,
            Padding = 10,
            VerticalAlign = VerticalAlign.Middle
        };
        hbox.SetStyle("showBackground", true);
        AddChild(hbox);

        _btnAddRect = new Button
        {
            Text = "Add new row",
            FocusEnabled = false,
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/shape_square_add"),
            Tooltip = "Adds a new row"
        };
        _btnAddRect.Click += delegate { AddRow(); };
        hbox.AddChild(_btnAddRect);

        Button btnRemoveAll = new Button
        {
            Text = "Remove all",
            FocusEnabled = false,
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/cancel"),
            Tooltip = "Removes all rows"
        };
        btnRemoveAll.Click += delegate
        {
            _vbox.RemoveAllContentChildren();
            _count = 0;
        };
        hbox.AddChild(btnRemoveAll);

        Button btnReport = new Button
        {
            Text = "Report",
            FocusEnabled = false,
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/information"),
            Tooltip = "Displays list state in an alert"
        };
        btnReport.Click += delegate
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("VBox children:");
            sb.AppendLine();
            int count = 1;
            foreach (var child in _vbox.Children)
            {
                ListRow row = child as ListRow;
                if (null != row)
                {
                    sb.AppendLine(string.Format("{0}. Selected: {1}; Text: {2}", count, row.Selected, row.Text));
                    count++;
                }
            }
            Alert.Show("Report", sb.ToString(), AlertButtonFlag.Ok);
        };
        hbox.AddChild(btnReport);

        Spacer spacer = new Spacer { Width = 20 };
        hbox.AddChild(spacer);

        CheckBox chkScroll = new CheckBox
        {
            Text = "Scroll content",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        chkScroll.Click += delegate
        {
            _vbox.ScrollContent = chkScroll.Selected;
        };
        hbox.AddChild(chkScroll);

        CheckBox chkClip = new CheckBox
        {
            Text = "Clip content",
            FocusEnabled = false,
            StyleMapper = "checkbox"
        };
        chkClip.Click += delegate
        {
            _vbox.ClipContent = chkClip.Selected;
        };
        hbox.AddChild(chkClip);

        CheckBox chkShowBackground = new CheckBox
        {
            Text = "Show background",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        chkShowBackground.Click += delegate
        {
            _vbox.SetStyle("showBackground", chkShowBackground.Selected);
        };
        hbox.AddChild(chkShowBackground);

        #endregion

        #region VBox

        // the example of setting PercentWidth together with MinWidth
        _vbox = new VBox { Height = 300, PercentWidth = 50, MinWidth = 250, ScrollContent = true, VerticalSpacing = 0, Padding = 4, BackgroundMode = BackgroundMode.Scrollable };
        //_vbox.AddEventListener(CheckBox.CHANGE, CheckboxChangeHandler); // will work in eDriven.Gui 1.6
        _vbox.SetStyle("showBackground", true);
        _vbox.SetStyle("showOverlay", true);
        AddChild(_vbox);

        #endregion
    }

    ///// <summary>
    ///// Note: CheckBox.CHANGE event does not bubble in eDriven.Gui 1.5
    ///// However, it will bubble in version 1.6, so you could catch the event easily on VBox conteiner then
    ///// </summary>
    ///// <param name="e"></param>
    //private void CheckboxChangeHandler(Event e)
    //{
    //    CheckBox cb = e.Target as CheckBox;
    //    if (null != cb)
    //    {
    //        Debug.Log("Checkbox state changed to: " + cb.Selected);
    //    }
    //}

    private void AddRow()
    {
        _count++;
        ListRow row = new ListRow
        {
            Text = "Row " + _count,
            Tooltip = "Row " + _count,
            PercentWidth = 100
        };
        //row.SetStyle("addedEffect", _tweenFactory);
        _vbox.AddContentChild(row);
    }
}