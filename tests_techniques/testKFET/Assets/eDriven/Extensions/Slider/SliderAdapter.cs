using System;
using System.Reflection;
using eDriven.Gui.Components;
using eDriven.Gui.Designer;
using Component=eDriven.Gui.Components.Component;

[Obfuscation(Exclude = true)]
[Toolbox(Label = "Slider", Icon = "eDriven/Editor/Controls/slider")]

public class SliderAdapter : ComponentAdapter
{
    #region Saveable values

    [Saveable]
    public SliderOrientation Orientation = SliderOrientation.Horizontal;
    
    [Saveable]
    public SliderNumberMode NumberMode = SliderNumberMode.Float;
    
    [Saveable]
    public bool Reverse;

    [Saveable]
    public float Value;

    [Saveable]
    public float MinValue;

    [Saveable]
    public float MaxValue = 10000f;

    [Saveable]
    public float Step = 1f;

    #endregion

    public SliderAdapter()
    {
        MinHeight = 20;
        MinWidth = 20;

        // default: horizontal slider having 200px width
        UseWidth = true;
        UsePercentWidth = false;
        Width = 200;
    }

    public override Type ComponentType
    {
        get { return typeof(Slider); }
    }

    public override Component NewInstance()
    {
        return new Slider();
    }

    public override void Apply(Component component)
    {
        base.Apply(component);

        Slider slider = (Slider)component;
        slider.Orientation = Orientation;
        slider.NumberMode = NumberMode;
        slider.Reverse = Reverse;
        slider.Value = Value;
        slider.MinValue = MinValue;
        slider.MaxValue = MaxValue;
        slider.Step = Step;
    }
}