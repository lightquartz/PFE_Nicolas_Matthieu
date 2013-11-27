using eDriven.Animation;
using eDriven.Audio;
using eDriven.Core;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Layout;
using eDriven.Gui.Managers;
using eDriven.Playground.Demo.Tweens;
using UnityEngine;
using Action=eDriven.Animation.Action;

public class LoadLevelDemo : Gui
{
    // exposed to inspector
    public float ButtonWidth = 400;
    public float ButtonHeight = 200;
    public bool ShowLoadAdditiveButton;

    private VBox _vbox;

    private readonly TweenFactory _showEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("portlet_add"); }),
            new FadeInLeftBounce()
        )
    ) { Delay = 0.2f };

    protected override void OnInitialize()
    {
        base.OnInitialize();

        Layout = new AbsoluteLayout();
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        _vbox = new VBox { VerticalSpacing = 30, HorizontalCenter = 0, VerticalCenter = 0, AutoLayout = false };
        AddChild(_vbox);

        Button btn1 = new Button
        {
            Width = ButtonWidth,
            Height = ButtonHeight,
            FocusEnabled = false,
            Text = "Load level 1",
            Tooltip = "Loads scene 1",
            StyleMapper = "load_level",
            Color = Color.green,
            Visible = false // for effect
        };
        btn1.Click += delegate
        {
            Application.LoadLevel(1);
        };
        _vbox.AddChild(btn1);

        Button btn2 = new Button
        {
            Width = ButtonWidth,
            Height = ButtonHeight,
            FocusEnabled = false,
            Text = "Load level 2",
            Tooltip = "Loads scene 2",
            StyleMapper = "load_level",
            Color = Color.green,
            Visible = false // for effect
        };
        btn2.Click += delegate
        {
            Application.LoadLevel(2);
        };
        _vbox.AddChild(btn2);

        if (ShowLoadAdditiveButton)
        {
            Button btn3 = new Button
            {
                Width = ButtonWidth,
                Height = ButtonHeight,
                FocusEnabled = false,
                Text = "LoadLevelAdditive(1);",
                Tooltip = "Loads scene additive",
                StyleMapper = "load_level",
                Visible = false // for effect
            };
            btn3.Click += delegate
            {
                Framework.LoadLevelAdditive(1);
            };
            _vbox.AddChild(btn3);
        }
    }

    protected override void OnCreationComplete()
    {
        base.OnCreationComplete();
        _showEffect.Play(_vbox.Children);
    }
}