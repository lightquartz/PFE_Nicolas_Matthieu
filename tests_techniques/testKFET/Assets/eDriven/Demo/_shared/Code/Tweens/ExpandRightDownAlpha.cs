using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Core.Managers;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;

namespace eDriven.Playground.Demo.Tweens
{
    public class ExpandRightDownAlpha : Sequence
    {
        public Stage Stage;

        public ExpandRightDownAlpha()
        {
            Add(

                // expand width & height to 400x400 first
                //new Parallel(

                //    Tween.New()
                //        .SetProperty("Width")
                //        .SetOptions(
                //        new TweenOption(TweenOptionType.Duration, 1.3f),
                //        new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Elastic.EaseOut),
                //        new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Width")),
                //        new TweenOption(TweenOptionType.EndValue, 400f)
                //        ),

                //    Tween.New()
                //        .SetProperty("Height")
                //        .SetOptions(
                //        new TweenOption(TweenOptionType.Duration, 1.3f),
                //        new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Elastic.EaseOut),
                //        new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Height")),
                //        new TweenOption(TweenOptionType.EndValue, 400f)
                //        )
                //    ),

                // expand width to stage width
                Tween.New()
                    .SetProperty("Width")
                    .SetOptions(
                    new TweenOption(TweenOptionType.Duration, 1f),
                    new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Bounce.EaseOut),
                    new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Width")),
                    new TweenOption(TweenOptionType.EndValueReaderFunction, (Tween.PropertyReaderFunction)delegate(object target)
                                                                                                              {
                                                                                                                  if (target is Button)
                                                                                                                  {
                                                                                                                      //Button btn = (Button) anim.Target;
                                                                                                                      return SystemManager.Instance
                                                                                                                                 .ScreenSize.X -
                                                                                                                             Stage.PaddingLeft -
                                                                                                                             Stage.PaddingRight - 30 // scrollbar
                                                                                                                          ;
                                                                                                                  }
                                                                                                                  return null;
                                                                                                              })
                    ),

                // expand height to stage height
                Tween.New()
                    .SetProperty("Height")
                    .SetOptions(
                    new TweenOption(TweenOptionType.Duration, 1f),
                    new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Bounce.EaseOut),
                    new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Height")),
                    new TweenOption(TweenOptionType.EndValueReaderFunction, (Tween.PropertyReaderFunction)delegate(object target)
                                                                                                              {

                                                                                                                  if (target is Button)
                                                                                                                  {
                                                                                                                      Button btn = (Button)target;
                                                                                                                      return SystemManager.Instance
                                                                                                                                 .ScreenSize.Y -
                                                                                                                             //Stage.PaddingTop -
                                                                                                                             btn.Y -
                                                                                                                             30 - // scrollbar
                                                                                                                             Stage.PaddingBottom;
                                                                                                                  }
                                                                                                                  return null;
                                                                                                              })
                    ),

                // tween alpha to 0.4
                Tween.New()
                    .SetProperty("Alpha")
                    .SetOptions(
                    new TweenOption(TweenOptionType.Duration, 2f),
                    new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction)Expo.EaseOut),
                    new TweenOption(TweenOptionType.StartValueReader, new PropertyReader("Alpha")),
                    new TweenOption(TweenOptionType.EndValue, 0.4f)
                    )
                );
        }
    }
}