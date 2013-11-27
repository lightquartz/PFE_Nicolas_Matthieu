using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Animation.Interpolators;
using UnityEngine;

namespace eDriven.Playground.Demo.Tweens
{
    public class Jumpy : Parallel
    {
        //private static object StartValueReaderFunc(object target)
        //{
        //    return (float)target.GetType().GetProperty("Y").GetValue(target, new object[] { }) + 100;
        //}

        //private static object EndValueReaderFunc(object target)
        //{
        //    return (float)target.GetType().GetProperty("Y").GetValue(target, new object[] { });
        //}

        public Jumpy()
        {
            Add(
                new Tween
                {
                    Property = "Alpha",
                    Duration = 0.6f,
                    Easer = Linear.EaseNone,
                    StartValue = 0f,
                    EndValue = 1f
                },
                //new Tween
                //{
                //    Property = "Y",
                //    Duration = 0.5f,
                //    Easer = Expo.EaseOut,
                //    StartValueReaderFunction = StartValueReaderFunc,
                //    EndValueReaderFunction = EndValueReaderFunc
                //},
                //new Tween
                //{
                //    Property = "Rotation",
                //    Duration = 0.8f,
                //    Easer = Elastic.EaseOut,
                //    StartValue = 180f,
                //    EndValue = 0f
                //},
                new Tween
                {
                    Property = "Scale",
                    Interpolator = new Vector2Interpolator(),
                    Duration = 0.8f,
                    Easer = Elastic.EaseOut,
                    StartValue = new Vector2(0f, 0.5f),
                    EndValue = new Vector2(1f, 1f)
                }
            );
        }
    }
}