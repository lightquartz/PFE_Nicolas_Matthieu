using eDriven.Core.Util;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;

public class LoadingMaskDemo : Gui
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        LayoutDescriptor = LayoutDescriptor.Absolute;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        #region VBox

        VBox vbox = new VBox
                        {
                            HorizontalCenter = 0,
                            VerticalCenter = 0,
                            Padding = 10,
                            VerticalSpacing = 10
                        };
        AddChild(vbox);

        #endregion

        HBox hbox = new HBox {VerticalAlign = VerticalAlign.Middle};
        vbox.AddChild(hbox);

        hbox.AddChild(new LoadingMaskAnimator { Width = 200, Height = 100, Message = "Loading something..." });
        hbox.AddChild(new LoadingMaskAnimator { Width = 300, Height = 200, Message = "Loading something else..." });
        hbox.AddChild(new LoadingMaskAnimator { Width = 300, Height = 300, Message = "And yet something else..." });

        vbox.AddChild(new Spacer {Height = 40});

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
            Tooltip = @"Click me to mask me for 5 seconds :)",
            ToggleMode = true
        };
        btn1.Click += delegate
                          {
                              int count = 0;

                              LoadingMask mask = new LoadingMask(btn1);
                              mask.SetMessage(string.Format("Masking... {0} seconds", count));

                              Timer t = new Timer(1, 5);
                              t.Tick += delegate
                                            {
                                                count++;
                                                mask.SetMessage(string.Format("Masking... {0} seconds", count));
                                            };
                              t.Complete += delegate { mask.Unmask(); };
                              t.Start();

                              // test: choppy animation is because of the simultaneous start. investigate!
                              //hbox.AddChild(new LoadingMaskAnimator { Width = 300, Height = 300, Message = "And yet something else..." });
                          };
        vbox.AddChild(btn1);
    }
}