using System.Collections;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Components;
using eDriven.Playground.Demo.Styles;
using UnityEngine;

public class SlidersComposite : eDriven.Gui.Gui
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        LayoutDescriptor = LayoutDescriptor.Absolute;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        Button button = new HesitantButton {
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
                                "Composite Slider Demo", 
                                "Created with eDriven GUI system",
                                "Author: Danko Kozar",
                                "Multiple sliders could be grouped into a single component"
                            },
                Callback = delegate (string line){ button.Text = line; }
            }
            .Start();

        AddChild(new Spacer { Height = 20 });

        HBox hbox = new HBox
                        {
                            HorizontalCenter = 0,
                            VerticalCenter = 0,
                            HorizontalSpacing = 30
                        };
        AddChild(hbox);

        WrapVBox(hbox, new Label {Text = "RGB", Styles = labelStyles, PercentWidth = 100}, 
                 new RgbSliders {RgbColor = Color.red});

        WrapVBox(hbox, new Label { Text = "RGB", Styles = labelStyles, PercentWidth = 100 },
                 new RgbSliders { RgbColor = Color.green });

        WrapVBox(hbox, new Label { Text = "RGB", Styles = labelStyles, PercentWidth = 100 },
                 new RgbSliders { RgbColor = Color.grey });
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