using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

/// <summary>
/// The example of the component class
/// </summary>
public class RgbSliders : Container
{
    private Slider _red;
    private Slider _green;
    private Slider _blue;

    public RgbSliders()
    {
        LayoutDescriptor = LayoutDescriptor.HorizontalLeft;
        MinHeight = 200;
    }

    protected override void CreateChildren()
    {
        base.CreateChildren();

        _red = new Slider { Orientation = SliderOrientation.Vertical, PercentHeight = 100, MaxValue = 255/*, BoolExample = true*/ };
        _red.Change += ChangeHandler;
        AddChild(_red);

        _green = new Slider { Orientation = SliderOrientation.Vertical, PercentHeight = 100, MaxValue = 255 };
        _green.Change += ChangeHandler;
        AddChild(_green);

        _blue = new Slider { Orientation = SliderOrientation.Vertical, PercentHeight = 100, MaxValue = 255 };
        _blue.Change += ChangeHandler;
        AddChild(_blue);
    }

    private void ChangeHandler(Event e)
    {
        //ValueChangedEvent ve = (ValueChangedEvent)e;
        //Debug.Log(ve.NewValue);

        Slider s = (Slider)e.Target;

        float percentage = MakeSliderColor(s.Percentage);

        if (e.Target == _red)
            s.Color = new Color(percentage, 0, 0);
        else if (e.Target == _green)
            s.Color = new Color(0, percentage, 0);
        else if (e.Target == _blue)
            s.Color = new Color(0, 0, percentage);

        _rgbColor = new Color(_red.Percentage, _green.Percentage, _blue.Percentage);
    }

    /// <summary>
    /// Helper function for getting a number from 0.5 to 1
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    private static float MakeSliderColor(float percentage)
    {
        return 0.5f + percentage * 0.5f;
    }

    private bool _rgbColorChanged;
    private Color _rgbColor;
    /// <summary>
    /// A collor getter/setter
    /// </summary>
    public Color RgbColor
    {
        get
        {
            return _rgbColor;
        }
        set
        {
            _rgbColor = value;
            _rgbColorChanged = true;
            InvalidateProperties();
        }
    }

    /// <summary>
    /// Delay setting on children because they might not be created yet
    /// </summary>
    protected override void CommitProperties()
    {
        base.CommitProperties();

        if (_rgbColorChanged)
        {
            //Debug.Log("_rgbColorChanged: " + _rgbColor);
            _rgbColorChanged = false;
            _red.Percentage = _rgbColor.r;
            _green.Percentage = _rgbColor.g;
            _blue.Percentage = _rgbColor.b;

            _red.Color = new Color(MakeSliderColor(_rgbColor.r), 0, 0);
            _green.Color = new Color(0, MakeSliderColor(_rgbColor.g), 0);
            _blue.Color = new Color(0, 0, MakeSliderColor(_rgbColor.b));
        }
    }
}