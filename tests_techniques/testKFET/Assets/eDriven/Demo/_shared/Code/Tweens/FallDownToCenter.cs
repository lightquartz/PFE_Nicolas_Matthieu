using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Core.Managers;
using eDriven.Gui.Components;
using UnityEngine;

namespace eDriven.Playground.Demo.Tweens
{
    public class FallDownToCenter : Sequence
    {
        private static object GetStartValue(object target)
        {
            float y = 0;
            DisplayObject displayObject = target as DisplayObject;
            if (null != displayObject)
                y = -displayObject.Height;
            else
                Debug.LogWarning(target + " is not a DisplayObject");

            return y;
        }

        private static object GetEndValue(object target)
        {
            var y = SystemManager.Instance.ScreenSize.Y;

            DisplayObject displayObject = target as DisplayObject;
            if (null != displayObject)
                y -= displayObject.Height;
            else
                Debug.LogWarning(target + " is not a DisplayObject");

            return y / 2; 
        }

        public FallDownToCenter()
        {
            Add(

                new SetProperty("Y", -1000),

                Tween.New()
                    .SetProperty("Y")
                    .SetOptions(
                        new TweenOption(TweenOptionType.Duration, 1f),
                        new TweenOption(TweenOptionType.Easer, (Tween.EasingFunction) Bounce.EaseOut),
                        new TweenOption(TweenOptionType.StartValueReaderFunction, (Tween.PropertyReaderFunction) GetStartValue),
                        new TweenOption(TweenOptionType.EndValueReaderFunction, (Tween.PropertyReaderFunction) GetEndValue)
                    )
                );
        }
    }
}