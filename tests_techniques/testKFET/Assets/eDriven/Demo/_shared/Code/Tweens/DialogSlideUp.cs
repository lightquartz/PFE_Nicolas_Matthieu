using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Core.Managers;
using eDriven.Gui.Components;

namespace eDriven.Playground.Demo.Tweens
{
    public class DialogSlideUp : Parallel
    {
        private static object GetStartValue(object target)
        {
            return SystemManager.Instance.ScreenSize.Y;
        }

        private static object GetEndValue(object target)
        {
            var y = SystemManager.Instance.ScreenSize.Y;

            DisplayObject displayObject = target as DisplayObject;

            if (null != displayObject)
                y -= displayObject.Height;

            return y/2;
        }

        public DialogSlideUp()
        {
            Add(
                Tween.New()
                    .SetProperty("Y")
                    .SetOptions(
                    new TweenOption(TweenOptionType.Duration, 1f),
                    new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction) Bounce.EaseOut),
                    new TweenOption(TweenOptionType.StartValueReaderFunction, (Tween.PropertyReaderFunction) GetStartValue),
                    new TweenOption(TweenOptionType.EndValueReaderFunction, (Tween.PropertyReaderFunction) GetEndValue)
                    ),

                // tween alpha to 0.4
                Tween.New()
                    .SetProperty("Alpha")
                    .SetOptions(
                        new TweenOption(TweenOptionType.Duration, 1f),
                        new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction) Expo.EaseOut),
                        new TweenOption(TweenOptionType.StartValue, 0f),
                        new TweenOption(TweenOptionType.EndValue, 1f)
                    )
            );
                
        }
    }
}