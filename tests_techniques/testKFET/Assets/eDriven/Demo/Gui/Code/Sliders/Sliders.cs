using System.Collections;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Components;
using eDriven.Playground.Demo.Styles;

public class Sliders : eDriven.Gui.Gui
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        LayoutDescriptor = eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleCenter;
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

        WrapHBox(Stage, button, new Spacer { PercentWidth = 100 })
            .PercentWidth = 100;

        var labelStyles = new Hashtable
                              {
                                  {"labelStyle", BlueLabelStyle.Instance}
                              };
    
        new TextRotator
            {
                Delay = 5, // 5 seconds delay
                Lines = new[]
                            {
                                "Slider Demo", 
                                "Created with eDriven GUI system",
                                "Author: Danko Kozar",
                                "Slider could be confugired as horizontal or vertical",
                                "Slider could be sized absolute or relative (percentage of parent)"
                            },
                Callback = delegate (string line){ button.Text = line; }
            }
            .Start();

        AddChild(new Spacer { Height = 20 });

        // horizontal sliders

        WrapHBox(Stage, new Label { Text = "100% slider", Styles = labelStyles }, new Slider { PercentWidth = 100 })
            .PercentWidth = 100;

        WrapHBox(Stage, new Label { Text = "400px slider", Styles = labelStyles }, new Slider { Width = 400, MaxValue = 400, Value = 200 });

        WrapHBox(Stage, new Label { Text = "400px slider, disabled", Styles = labelStyles }, new Slider { Width = 400, Height = 30, Enabled = false });

        WrapHBox(Stage, new Label { Text = "400x50 slider", Styles = labelStyles }, new Slider { Width = 400, Height = 50 });

        AddChild(new Spacer {Height = 20});

        // vertical sliders

        HBox hbox = new HBox
                        {
                            HorizontalAlign = HorizontalAlign.Center,
                            PercentWidth = 100,
                            PercentHeight = 100,
                            HorizontalSpacing = 30
                        };
        AddChild(hbox);

        WrapVBox(hbox, new Label { Text = "100% slider", Styles = labelStyles }, 
                 new Slider { Orientation = SliderOrientation.Vertical, PercentHeight = 100 })
            .PercentHeight = 100;

        WrapVBox(hbox, new Label { Text = "400px slider", Styles = labelStyles },
                 new Slider {Orientation = SliderOrientation.Vertical, Height = 400});

        WrapVBox(hbox, new Label { Text = "400px slider, disabled", Styles = labelStyles },
                 new Slider {Orientation = SliderOrientation.Vertical, Width = 30, Height = 400, Enabled = false});

        WrapVBox(hbox, new Label { Text = "50x400 slider", Styles = labelStyles }, 
                 new Slider { Orientation = SliderOrientation.Vertical, Width = 50, Height = 400 });

    }

    #region Helper

    protected Container WrapHBox(Container parent, params DisplayListMember[] children)
    {
        HBox wrapper = new HBox { HorizontalAlign = HorizontalAlign.Left, VerticalAlign = VerticalAlign.Middle };
        parent.AddChild(wrapper);

        foreach (DisplayListMember child in children)
        {
            wrapper.AddChild(child);
        }
        return wrapper;
    }

    protected Container WrapVBox(Container parent, params DisplayListMember[] children)
    {
        VBox wrapper = new VBox { HorizontalAlign = HorizontalAlign.Center };
        parent.AddChild(wrapper);

        foreach (DisplayListMember child in children)
        {
            wrapper.AddChild(child);
        }
        return wrapper;
    }

    #endregion
}