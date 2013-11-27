using eDriven.Gui.Mappers;
using eDriven.Gui.Components;
using eDriven.Gui.Graphics.Base;
using UnityEngine;
using Rect=eDriven.Gui.Graphics.Rect;

namespace eDriven.Playground.Demo.Components
{
    public class BluePanelSkin
    {
        #region Quasi-Singleton

        private static GUISkin _instance;
        public static GUISkin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (GUISkin) ScriptableObject.CreateInstance(typeof (GUISkin));
                    Initialize();
                }
                return _instance;
            }
        }

        private BluePanelSkin()
        {
            // constructor is protected
        }

        #endregion

        private static ProgramaticStyle _style;

        private const int Weight = 3;

        private static void Initialize()
        {
            _style = new ProgramaticStyle
                         {
                             Style = _instance.box,
                             Alignment = TextAnchor.MiddleCenter,
                             Font = FontMapper.GetDefault()
                         };

            const int w = (Weight + 1) * 2;

            _style.Border = new RectOffset(Weight + 1, Weight + 1, Weight + 1, Weight + 1);
            _style.Padding = new RectOffset(2, 2, 2, 2);

            //_style.FontSize = 8;
            _style.NormalTextColor = Color.white;
            _style.NormalGraphics = new Rect(w, w, new Fill(new Color(0, 0.3f, 1f, 1f)), new Stroke(Color.black, Weight));

            _style.Validate();

        }
    }
}