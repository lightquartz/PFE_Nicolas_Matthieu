using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Gui;
using eDriven.Playground.Demo.Styles;

public class AbsoluteLayoutDemo2 : Gui
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        LayoutDescriptor = LayoutDescriptor.Absolute;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        VBox vbox = new VBox
                         {
                             X = 10,
                             Y = 10,
                             HorizontalAlign = HorizontalAlign.Left
                         };
        AddChild(vbox);

        var l = new Label { Text = "Constrain layout" };
        l.SetStyle("labelStyle", BlueLabelStyle.Instance);
        vbox.AddChild(l);

        l = new Label { Text = "[resize your screen]" };
        l.SetStyle("labelStyle", BlueLabelStyle.Instance);
        vbox.AddChild(l);

        Button btn1 = new Button
        {
            Left = 100,
            Top = 100,
            Width = 200,
            Height = 200,
            FocusEnabled = false,
            Text = @"Left = 100,
Top = 100,
Width = 200,
Height = 200",
            Tooltip = @"This is the tooltip
Nice, eh? :)",
            ToggleMode = true
        };
        AddChild(btn1);

        Button btn2 = new Button
        {
            Left = 400,
            Right = 100,
            Top = 100,
            MinWidth = 200,
            Height = 200,
            FocusEnabled = false,
            Text = @"Left = 400,
Right = 100,
Top = 100,
MinWidth = 200,
Height = 200",
            Tooltip = @"This is the tooltip of the second button
(this one is constrained in width, so resizes with screen width)",
            ToggleMode = true
        };
        AddChild(btn2);

        Button btn3 = new Button
        {
            Left = 100,
            Top = 400,
            Bottom = 100,
            Width = 200,
            MinHeight = 200,
            FocusEnabled = false,
            Text = @"Left = 100,
Top = 400,
Bottom = 100,
Width = 200,
MinHeight = 200",
            Tooltip = @"This is the tooltip of the third button
(this one is constrained in height, so resizes with screen height)",
            ToggleMode = true
        };
        AddChild(btn3);

//        Button btn4 = new Button
//        {
//            Left = 400,
//            Right = 100,
//            Top = 400,
//            Bottom = 100,
//            FocusEnabled = false,
//            Text = @"Left = 400,
//Right = 100,
//Top = 400,
//Bottom = 100",
//            ToggleMode = true
//        };
//        AddChild(btn4);

        Stuff btn4 = new Stuff
        {
            Left = 400,
            Right = 100,
            Top = 400,
            Bottom = 100
        };
        AddChild(btn4);

        l = new Label
        {
            HorizontalCenter = 0,
            Bottom = 10,
            Text = "[Ctrl + click = sticky, Ctrl + Return toggles inspector, Ctrl + arrows for walking the DOM]"
        };
        l.SetStyle("labelStyle", BlueLabelStyle.Instance);
        AddChild(l);
    }
}