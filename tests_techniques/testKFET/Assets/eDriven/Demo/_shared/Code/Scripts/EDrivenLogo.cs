using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Stages;
using UnityEngine;

public class EDrivenLogo : Gui
{
    public bool LogoVisible;
    private TweenFactory _showEffect;
    private Image _img;

    private static object GetEndValue(object target)
    {
        DisplayObject displayObject = target as DisplayObject;

        float width = 0f;

        if (null != displayObject)
            width = displayObject.Width;

        return -width;
    }

    protected override void CreateChildren()
    {
        base.CreateChildren();

        var texture = (Texture) Resources.Load("edriven");

        _img = new Image
                   {
                       //IncludeInLayout = false, // disable stage invalidation -> no no doesn't work
                       Texture = texture,
                       Right = -texture.width,
                       Bottom = 10,
                       MouseEnabled = false
                   };

        GuiInspectorStage.Instance.AddChild(_img);
    }

    protected override void OnCreationComplete()
    {
        base.OnCreationComplete();

        if (LogoVisible)
        {
            RunLogoAnimation();
        }
    }

    private void RunLogoAnimation()
    {
        //Debug.Log("RunLogoAnimation");

        _showEffect = new TweenFactory(

            new Sequence(

               new Pause { Duration = Application.isWebPlayer ? 9 : 1 },

               //new Action(delegate { Debug.Log("Started"); }),

               Tween.New()
                   .SetOptions(
                   new TweenOption(TweenOptionType.Property, "Right"),
                   new TweenOption(TweenOptionType.Duration, 1f),
                   new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Bounce.EaseOut),
                   new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Right")),
                   new TweenOption(TweenOptionType.EndValue, 10f)
                ),

                new Pause { Duration = 4f },

                Tween.New()
                   .SetOptions(
                   new TweenOption(TweenOptionType.Property, "Right"),
                   new TweenOption(TweenOptionType.Duration, 0.7f),
                   new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Back.EaseIn),
                   new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Right")),
                   new TweenOption(TweenOptionType.EndValueReaderFunction, (Tween.PropertyReaderFunction)GetEndValue)
                ),

                new Action(delegate { GuiInspectorStage.Instance.RemoveChild(_img); })

            ){Name = "Logo animation"}
        );

        _showEffect.Play(_img);
    }
}