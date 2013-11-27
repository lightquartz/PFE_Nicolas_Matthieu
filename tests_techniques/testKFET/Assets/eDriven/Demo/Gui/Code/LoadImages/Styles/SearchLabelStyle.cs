using eDriven.Gui.Components;
using eDriven.Gui.Mappers;
using UnityEngine;

namespace eDriven.Playground.Demo.Gui.Styles
{
    public class SearchLabelStyle
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

        private SearchLabelStyle()
        {
            // constructor is protected
        }

        #endregion

        private static ProgramaticStyle _style;

        private const int Weight = 3;

        private static void Initialize()
        {
            _instance.name = "SearchLabelStyle";

            _style = new ProgramaticStyle
                         {
                             Style = _instance,
                             Border = new RectOffset(Weight + 1, Weight + 1, Weight + 1, Weight + 1),
                             Padding = new RectOffset(2, 2, 2, 2),
                             NormalTextColor = new Color(1, 1, 1),
                             Font = FontMapper.GetWithFallback("pixel")
                         };

            _style.Validate();
        }
    }
}