using eDriven.Animation;
using eDriven.Animation.Easing;

namespace eDriven.Playground.Demo.Tweens
{
    public class FadeOut : Tween
    {
        public FadeOut()
        {
            Property = "Alpha";
            Duration = 1f;
            Easer = Linear.EaseNone;
            //StartValue = 1f;
            StartValueReader = new PropertyReader("Alpha");
            EndValue = 0f;
        }
    }
}