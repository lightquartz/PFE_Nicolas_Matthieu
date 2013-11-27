using eDriven.Gui.Components;
using eDriven.Gui.Graphics.Base;
using eDriven.Gui.Util;
using UnityEngine;
using Rect=eDriven.Gui.Graphics.Rect;

namespace eDriven.Playground.Demo.Gui.Styles
{
    public class OptionsPopupBackgroundStyle
    {
        #region Quasi-Singleton

        private static GUIStyle _instance;
        public static GUIStyle Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GUIStyle();
                    Initialize();
                }
                return _instance;
            }
        }

        private OptionsPopupBackgroundStyle()
        {
            // constructor is protected
        }

        #endregion

        private static ProgramaticStyle _style;

        private const int Weight = 1;

        private static void Initialize()
        {
            _instance.name = "OptionsPopupBackgroundStyle";

            const int w = (Weight + 1) * 2;

            _style = new ProgramaticStyle
                         {
                             Style = _instance,
                             Border = new RectOffset(Weight + 1, Weight + 1, Weight + 1, Weight + 1),
                             Padding = new RectOffset(2, 2, 2, 2),
                             NormalGraphics = new Rect(w, w,
                                                       new Fill(RgbColor.FromHex(0xBBD4F6).ToColor()), // RgbColor.FromHex(0xC0C0C0)
                                                       new Stroke(RgbColor.FromHex(0x404040).ToColor(), Weight))
                         };

            _style.Validate();
        }
    }
}