using System.Collections;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Components;
using eDriven.Playground.Demo.Styles;

public class AbsoluteLayoutDemo : Gui
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        LayoutDescriptor = LayoutDescriptor.Absolute;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        Button button = new HesitantButton
        {
            X = 10,
            Y = 10,
            FocusEnabled = false,
            Styles = new Hashtable 
                                { 
                                    {"buttonStyle", EDrivenButtonStyle.Instance} 
                                }
        };
        AddChild(button);

        var buttonStyles = new Hashtable
                               {
                                   {"buttonStyle", ButtonStyle2.Instance}
                               };

        new TextRotator
            {
                Delay = 5, // 5 seconds delay
                Lines = new[]
                                {
                                    "Absolute Layout", 
                                    "Created with eDriven GUI system",
                                    "Author: Danko Kozar"
                                },
                Callback = delegate(string line) { button.Text = line; }
            }
            .Start();

        Label l = new Label { Text = "[resize your screen to see layout changes]", Bottom = 10, HorizontalCenter = 0 };
        l.SetStyle("labelStyle", BlueLabelStyle.Instance);
        AddChild(l);

        Button btn1 = new Button
        {
            Left = 100,
            Top = 100,
            Width = 300,
            Height = 300,
            FocusEnabled = false,
            Text = @"Left = 100,
Top = 100,
Width = 200,
Height = 200",
            Tooltip = @"This is the tooltip
Nice, eh? :)",
            ToggleMode = true,
            Styles = buttonStyles
        };
        AddChild(btn1);

        Button btn2 = new Button
        {
            Left = 500,
            Right = 100,
            Top = 100,
            Height = 300,
            MinWidth = 200,
            FocusEnabled = false,
            Text = @"Left = 500,
Right = 100,
Top = 100,
Height = 300,
MinWidth = 200",
            Tooltip = @"This is the tooltip of the second button
(this one is constrained in width, so resizes with screen width)",
            ToggleMode = true,
            Styles = buttonStyles
        };
        AddChild(btn2);

        Button btn3 = new Button
        {
            Left = 100,
            Top = 500,
            Bottom = 100,
            Width = 300,
            MinHeight = 200,
            FocusEnabled = false,
            Text = @"Left = 100,
Top = 500,
Bottom = 100,
Width = 300,
MinHeight = 200",
            Tooltip = @"This is the tooltip of the third button
(this one is constrained in height, so resizes with screen height)",
            ToggleMode = true,
            Styles = buttonStyles
        };
        AddChild(btn3);

        Button btn4 = new Button
        {
            Left = 500,
            Right = 100,
            Top = 500,
            Bottom = 100,
            MinWidth = 300,
            MinHeight = 300,
            FocusEnabled = false,
            Text = @"Left = 500,
Right = 100,
Top = 500,
Bottom = 100,
MinWidth = 300,
MinHeight = 300",
            ToggleMode = true,
            Styles = buttonStyles
        };
        AddChild(btn4);
    }
}