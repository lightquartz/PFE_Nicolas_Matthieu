using System;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Gui.Managers;
using eDriven.Gui.Stages;
using eDriven.Networking.Util;
using eDriven.Playground.Demo.Dialogs;
using eDriven.Playground.Demo.Models;
using Event=eDriven.Core.Events.Event;
using EventHandler=eDriven.Core.Events.EventHandler;

namespace eDriven.Playground.Demo.Components
{
    public class Options : Container
    {
        private CheckBox _chkInspect;
        private CheckBox _chkDetails;
        private CheckBox _chkRunInBackground;

        private Slider _sliderVolume;

        public Options()
        {
            PercentWidth = 100;
            StyleMapper = "options";
            Layout = new BoxLayout { HorizontalAlign = HorizontalAlign.Left, VerticalSpacing = 6 };
            Padding = 8;
            PaddingTop = 6;
            PaddingBottom = 10;
        }

        protected override void CreateChildren()
        {
            base.CreateChildren();

            _chkInspect = new CheckBox
                              {
                                  Text = "Inspector",
                                  StyleMapper = "checkbox",
                                  Right = 10,
                                  Top = 10,
                                  Padding = 0,
                                  ResizeWithContent = true,
                                  ToggleMode = true,
                                  FocusEnabled = false, // true
                                  Selected = OptionsModel.Instance.InspectorActive
                              };
            _chkInspect.Click += delegate
                                     {
                                         OptionsModel.Instance.InspectorActive = _chkInspect.Selected;
                                     };
            AddContentChild(_chkInspect);

            _chkDetails = new CheckBox
                              {
                                  Text = "Details",
                                  StyleMapper = "checkbox",
                                  Right = 10,
                                  Top = 10,
                                  Padding = 0,
                                  ResizeWithContent = true,
                                  ToggleMode = true,
                                  FocusEnabled = false,
                                  Selected = OptionsModel.Instance.DetailsActive
                              };
            _chkDetails.Change += new EventHandler(delegate
                                      {
                                          OptionsModel.Instance.DetailsActive = _chkDetails.Selected;
                                      });
            AddContentChild(_chkDetails);

            _chkRunInBackground = new CheckBox
                                      {
                                          Text = "Run in the background",
                                          StyleMapper = "checkbox",
                                          Right = 10,
                                          Top = 10,
                                          Padding = 0,
                                          ResizeWithContent = true,
                                          ToggleMode = true,
                                          FocusEnabled = false,
                                          Selected = OptionsModel.Instance.RunInBackground
                                      };
            _chkRunInBackground.Change += delegate
                                              {
                                                  //Application.runInBackground = _chkRunInBackground.Selected;
                                                  OptionsModel.Instance.RunInBackground = _chkRunInBackground.Selected;
                                                  Logger.Log("Application.runInBackground changed to: " + OptionsModel.Instance.RunInBackground);
                                              };
            AddContentChild(_chkRunInBackground);

            AddChild(new Label {Text = "Volume: ", StyleMapper = "checkbox"});

            _sliderVolume = new Slider
                                {
                                    Value = OptionsModel.Instance.Volume,
                                    MinValue = 0,
                                    MaxValue = 1, 
                                    PercentWidth = 100, 
                                    Tooltip = "Volume",
                                    StyleMapper = "slider_volume"
                                };
            _sliderVolume.Change += new EventHandler(delegate //(Core.Events.Event e)
                                        {
                                            //ValueChangedEvent vce = (ValueChangedEvent) e;
                                            var tt = Math.Round(_sliderVolume.Value * 100) + " %";
                                            _sliderVolume.Tooltip = tt; 
                                            TooltipManagerStage.Instance.UpdateTooltip(tt);
                                            OptionsModel.Instance.Volume = _sliderVolume.Value;
                                        });
            AddChild(_sliderVolume);

            Button button = new Button
                                {
                                    Text = "Set resolution",
                                    FocusEnabled = false,
                                    StyleMapper = "miki"
                                };
            button.Click += delegate
                                {
                                    ResolutionDialog dialog = new ResolutionDialog();
                                    PopupManager.Instance.AddPopup(dialog);
                                };
            AddChild(button);
        }

        protected override void CreationComplete()
        {
            base.CreationComplete();

            OptionsModel.Instance.AddEventListener(OptionsModel.INSPECTOR_ACTIVE_CHANGED, OnInspectorActiveChanged);
            OptionsModel.Instance.AddEventListener(OptionsModel.DETAILS_ACTIVE_CHANGED, OnDetailsActiveChanged);
            OptionsModel.Instance.AddEventListener(OptionsModel.RUN_IN_BACKGROUND_CHANGED, OnRunInBackgroundChanged);
            OptionsModel.Instance.AddEventListener(OptionsModel.VOLUME_CHANGED, OnVolumeChanged);
        }

        private void OnInspectorActiveChanged(Event e)
        {
            if (_chkInspect.Selected != OptionsModel.Instance.InspectorActive)
                _chkInspect.Selected = OptionsModel.Instance.InspectorActive;
        }

        private void OnDetailsActiveChanged(Event e)
        {
            if (_chkDetails.Selected != OptionsModel.Instance.DetailsActive)
                _chkDetails.Selected = OptionsModel.Instance.DetailsActive;
        }

        #region Handlers

        private void OnRunInBackgroundChanged(Event e)
        {
            if (_chkRunInBackground.Selected != OptionsModel.Instance.RunInBackground)
                _chkRunInBackground.Selected = OptionsModel.Instance.RunInBackground;
        }

        private void OnVolumeChanged(Event e)
        {
            if (_sliderVolume.Value != OptionsModel.Instance.Volume)
                _sliderVolume.Value = OptionsModel.Instance.Volume;
        }

        #endregion

        public void UpdateValues()
        {
            _sliderVolume.Value = OptionsModel.Instance.Volume;
        }

        public override void Dispose()
        {
            base.Dispose();

            OptionsModel.Instance.RemoveEventListener(OptionsModel.INSPECTOR_ACTIVE_CHANGED, OnInspectorActiveChanged);
            OptionsModel.Instance.RemoveEventListener(OptionsModel.DETAILS_ACTIVE_CHANGED, OnDetailsActiveChanged);
            OptionsModel.Instance.RemoveEventListener(OptionsModel.RUN_IN_BACKGROUND_CHANGED, OnRunInBackgroundChanged);
            OptionsModel.Instance.RemoveEventListener(OptionsModel.VOLUME_CHANGED, OnVolumeChanged);
        }
    }
}