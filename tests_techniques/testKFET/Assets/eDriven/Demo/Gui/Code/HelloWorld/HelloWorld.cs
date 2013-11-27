using eDriven.Animation;
using eDriven.Audio;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Playground.Demo.Tweens;
using UnityEngine;

public class HelloWorld : Gui
{
    //private readonly TweenFactory _dlgOpen = new TweenFactory(
    //    new Sequence(
    //        new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("dialog_open"); }),
    //        new FadeIn()
    //    )  
    //);

    private readonly TweenFactory _dlgOpen2 = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("dialog_open"); }),
            new FallDownToCenter()
        )
    );

    private readonly TweenFactory _overlayShow = new TweenFactory(
        new Sequence(
            new FadeIn()
        )
    );

    override protected void OnStart()
    {
        base.OnStart();

        LayoutDescriptor = eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleCenter;

        Dialog.AddedEffect = _dlgOpen2;
        ModalOverlay.AddedEffect = _overlayShow;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        VBox vbox = new VBox();
        AddChild(vbox);

        Button btn = new Button
        {
            Text = "Button 1",
            Icon = (Texture)Resources.Load("Icons/star")
        };
        vbox.AddChild(btn);

        btn = new Button
        {
            Text = "Button 2",
            Icon = (Texture)Resources.Load("Icons/star"),
            StyleMapper = "button2"
        };
        vbox.AddChild(btn);

        btn = new Button
        {
            Text = "Button 3",
            Icon = (Texture)Resources.Load("Icons/star"),
            StyleMapper = "button3"
        };
        vbox.AddChild(btn);

        vbox.Click += delegate
        {
            Alert.Show(
                "Checking",
                "Are you sure you want to greet the world?",
                AlertButtonFlag.Yes | AlertButtonFlag.No,
                delegate (string action)
                    {
                        switch (action)
                        {
                            case "yes":
                                Alert.Show(
                                    "Hello",
                                    "Hello world!",
                                    AlertButtonFlag.Ok
                                );
                                break;
                            case "no":
                                Alert.Show(
                                    "Going to sleep",
                                    "Good night.",
                                    AlertButtonFlag.Ok
                                );
                                break;
                        }
                    }
            );
        };
    }
}