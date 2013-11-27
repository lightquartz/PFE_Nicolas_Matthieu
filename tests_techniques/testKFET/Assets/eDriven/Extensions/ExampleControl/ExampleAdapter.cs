using System;
using eDriven.Extensions.ExampleControl;
using eDriven.Gui.Components;
using eDriven.Gui.Designer;
using Component=eDriven.Gui.Components.Component;

[Toolbox(Label = "ExampleControl", Group = "My controls", Icon = "eDriven/Editor/Controls/example")]

public class ExampleAdapter : ComponentAdapter
{
    #region Saveable values

    [Saveable]
    public string Text = "This is a text";

    [Saveable]
    public string ButtonText = "Click me";

    [Saveable]
    public bool BoolExample;

    [Saveable]
    public SliderOrientation EnumExample = SliderOrientation.Horizontal;

    #endregion

    public ExampleAdapter()
    {
        // setting default values   
        MinWidth = 400;
        PaddingLeft = 10;
        PaddingRight = 10;
        PaddingTop = 10;
        PaddingBottom = 10;
    }

    public override Type ComponentType
    {
        get { return typeof(ExampleControl); }
    }

    public override Component NewInstance()
    {
        return new ExampleControl();
    }

    public override void Apply(Component component)
    {
        base.Apply(component);

        ExampleControl exampleControl = (ExampleControl)component;
        exampleControl.Text = Text;
        exampleControl.ButtonText = ButtonText;
    }
}