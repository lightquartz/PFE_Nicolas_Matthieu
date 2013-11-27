using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Animation.Interpolators;
using eDriven.Core.Geom;
using UnityEngine;

namespace eDriven.Playground.Demo.Tweens
{
    public class ShowEffect : Parallel
    {
        public static float DialogWidth = 600;
        public static float DialogHeight = 400;

        private static object StartValueReaderFunc(object target)
        {
            return target.GetType().GetProperty("Bounds").GetValue(target, new object[] { });
        }
        private static object EndValueReaderFunc(object target)
        {
            return new Rectangle((Screen.width - DialogWidth)/2, (Screen.height - DialogHeight)/2, DialogWidth,
                                 DialogHeight);
        }

        public ShowEffect()
        {
            Add(
                new Tween
                {
                    Property = "Alpha",
                    Duration = 0.3f,
                    Easer = Linear.EaseNone,
                    StartValue = 0f,
                    EndValue = 1f
                },
                new Tween
                {
                    Property = "Bounds",
                    Duration = 0.65f,
                    Interpolator = new RectangleInterpolator(),
                    Easer = Quad.EaseOut,
                    StartValueReaderFunction = StartValueReaderFunc,
                    EndValueReaderFunction = EndValueReaderFunc
                },
                new Tween
                {
                    Property = "Scale",
                    Interpolator = new Vector2Interpolator(),
                    Duration = 0.65f,
                    Easer = Quad.EaseOut,
                    StartValue = new Vector2(0.001f, 0.001f),
                    EndValue = new Vector2(1f, 1f)
                }
            );
        }
    }
}