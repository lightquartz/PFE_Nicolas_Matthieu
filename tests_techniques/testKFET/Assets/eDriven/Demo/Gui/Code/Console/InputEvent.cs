using eDriven.Core.Events;

namespace eDriven.Playground.eDriven.Demo.Gui.Code.Console
{
    public class InputEvent : Event
    {
        #region CONSTANTS

        // ReSharper disable InconsistentNaming
        public const string INPUT = "input";
        // ReSharper restore InconsistentNaming

        #endregion

        /// <summary>
        /// The last typed character
        /// </summary>
        public string Input;

        public InputEvent(string type) : base(type)
        {
        }

        public InputEvent(string type, object target) : base(type, target)
        {
        }

        public InputEvent(string type, bool bubbles) : base(type, bubbles)
        {
        }

        public InputEvent(string type, bool bubbles, bool cancelable) : base(type, bubbles, cancelable)
        {
        }

        public InputEvent(string type, object target, bool bubbles, bool cancelable) : base(type, target, bubbles, cancelable)
        {
        }
    }
}