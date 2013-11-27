using eDriven.Gui.Components;
using eDriven.Gui.Graphics.Base;
using eDriven.Gui.Mappers;
using UnityEngine;
using Rect = eDriven.Gui.Graphics.Rect;

namespace eDriven.Playground.Demo.Gui.Styles
{
    public class SearchContainerBackgroundStyle
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

        private SearchContainerBackgroundStyle()
        {
            // constructor is protected
        }

        #endregion

        private static ProgramaticStyle _style;

        private const int Weight = 3;

        private static void Initialize()
        {
            _instance.name = "SearchContainerBackgroundStyle";

            const int w = (Weight + 1) * 2;
            
            _style = new ProgramaticStyle
                         {
                             Style = _instance,
                             Border = new RectOffset(Weight + 1, Weight + 1, Weight + 1, Weight + 1),
                             Padding = new RectOffset(2, 2, 2, 2),
                             NormalGraphics = new Rect(w, w, new Fill(new Color(0, 0, 0, 0.5f))),
                             Font = FontMapper.GetDefault()
                         };

            _style.Validate();
        }
    }
}