#region License

/*
 
Copyright (c) 2013 Danko Kozar. All rights reserved.
 
*/

#endregion License

using eDriven.Animation;
using eDriven.Animation.Easing;
using eDriven.Animation.Interpolators;
using eDriven.Core.Events;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

namespace eDriven.Playground.eDriven.Demo.Gui.Code.Console
{
    /// <summary>
    /// The console field
    /// This is an example of a composite control
    /// The console field is a scrollable container, containing a label that is resizing with text
    /// Container reacts on label resize by scrolling to the last line
    /// There's a hidden, focusable TextField used for input (but not hidden using Visible=false because then it wouldn't render at all)
    /// The focus for the control is being routed to this TextField (usign FocusRouting)
    /// </summary>
    public class ConsoleField : Container
    {
        #region Properties

        public static string PromptText = ">";
        public static string Cursor = "_";
        public bool ScrollTweening = true;

        #endregion

        #region Members

        private string _currentCursor = " ";

        private bool _textChanged;
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                _textChanged = true;
                InvalidateProperties();
                //Debug.Log("Text: " + _text);
            }
        }

        private Label _display;
        private TextField _txtInput;

        private Container _inner;

        public ConsoleField()
        {
            _currentCursor = Cursor;
            
            //_timer = new Timer(1);
            //_timer.Tick += OnTimerTick;
            //_timer.Start();
        }

        private bool _showCursor = true;
        public bool ShowCursor
        {
            get { return _showCursor; }
            set
            {
                _showCursor = value;
                _currentCursor = _showCursor ? Cursor : " ";
            }
        }

        #region TODO

        //private readonly Timer _timer;

        //private float _delay;
        //private float _last;

        //private void OnTimerTick(Event e)
        //{
        //    ShowCursor = !ShowCursor;
        //    _display.Text = _text + _currentCursor;
        //}

//// ReSharper disable ConvertToConstant.Global
//        public float Fps = 500;
//// ReSharper restore ConvertToConstant.Global

        #endregion
        private bool _inputModeChanged;
        private bool _inputMode;
        public bool InputMode
        {
            get { 
                return _inputMode;
            }
            set
            {
                if (value == _inputMode)
                    return;
        
                _inputMode = value;
                _inputModeChanged = true;
                InvalidateProperties();
            }
        }

        #endregion

        #region TODO

//private int _count;
        
        //public void PreRenderSlot(params object[] parameters)
        //{
        //    if (Fps > 0)
        //    {
        //        if (Time.time - _last < _delay)
        //            return;

        //        _last = Time.time;
        //    }

        //    _display.Text = _text.Substring(0, _count);

        //    _count++;

        //    if (_count == _text.Length)
        //    {       
        //        //Debug.Log("Disconnecting");
        //        SystemManager.Instance.PreRenderSignal.Disconnect(PreRenderSlot);
        //        _count = 0;
        //    }

        //    // hide system cursor
        //    //GUI.skin.settings.cursorColor = new Color(0, 0, 0, 0);
        //}

        #endregion

        #region Methods

        protected override void CreateChildren()
        {
            base.CreateChildren();

            /**
             * We are creating the textfield to which we will route the focus
             * Text typed in this field is going to appear in the display
             * Note: We cannot make it non-visible, since then it won't be rendered at all
             * */
            _txtInput = new TextField { X = 10, Y = 10 }; // keep it visible, but beyond the display
            _txtInput.Changing += OnInputTextChanging;
            _txtInput.Change += OnInputTextChange;
            _txtInput.KeyDown += OnInputKeyDown;
            AddChild(_txtInput);

            // resizable container
            _inner = new Container
                         {
                             PercentWidth = 100,
                             PercentHeight = 100,
                             ScrollContent = true,
                             MouseEnabled = true,
                             FocusEnabled = false,
                             StyleMapper = "ConsoleBg"
                         };
            AddChild(_inner);

            // label as its only child
            _display = new Label
                           {
                               Width = 300,
                               ExpandHeightByMeasuringText = true,
                               Multiline = true,
                               StyleMapper = "ConsoleText"
                           };
            _display.ResizeHandler += OnResize;
            _inner.AddChild(_display);
            _inner.ResizeHandler += OnResize;

            // we are routing the focus ot this component to input text field
            FocusRouting = SetFocus;
        }

        protected override void CreationComplete()
        {
            base.CreationComplete();
            _display.Width = Width - 24;
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (_textChanged)
            {
                _textChanged = false;

                #region TODO

                //if (Fps > 0)
                //    _delay = 1 / Fps;

                //_last = Time.time;

                //_timer.Stop();
                //_timer.Start();

                #endregion

                ShowCursor = true;

                //Debug.Log("Connecting");

                if (_inputMode)
                {
                    _display.Text = _text + _currentCursor;
                }
                //else
                //{
                //    SystemManager.Instance.PreRenderSignal.Connect(PreRenderSlot);
                //}
            }

            if (_inputModeChanged)
            {
                //Debug.Log("_inputModeChanged");
                _inputModeChanged = false;

                //if (_inputMode)
                //{
                //    SystemManager.Instance.PreRenderSignal.Disconnect(PreRenderSlot);
                //}
            }
        }

        public override void SetFocus()
        {
            _txtInput.SetFocus();
        }

        #endregion

        #region Handlers

        private void OnInputTextChanging(Event e)
        {
            if (!_inputMode)
                e.Cancel();
        }

        /// <summary>
        /// Process the input change
        /// </summary>
        /// <param name="e"></param>
        private void OnInputTextChange(Event e)
        {
            //Debug.Log("OnInputTextChange: " + _txtInput.Text);
            _display.Text = _text + _txtInput.Text + _currentCursor;
        }

        /// <summary>
        /// Process the ENTER key
        /// </summary>
        /// <param name="e"></param>
        private void OnInputKeyDown(Event e)
        {
            KeyboardEvent ke = (KeyboardEvent)e;
            if (ke.KeyCode == KeyCode.Return)
            {
                var input = _txtInput.Text;
                Text = _text + _txtInput.Text;
                _txtInput.Text = string.Empty;
                DispatchEvent(new InputEvent(InputEvent.INPUT) { Bubbles = true, Input = input });
            }
        }

        /// <summary>
        /// React on text area resize and scroll to the bottom
        /// </summary>
        /// <param name="e"></param>
        private void OnResize(Event e)
        {
            //Debug.Log("OnResize");
            _display.Width = Width - 24; // get rid of horiz scroller

            var diff = _display.Height - _inner.Height;

            if (diff > 0)
            {
                //Debug.Log("diff: " + diff);
                if (ScrollTweening)
                {
                    var tween = new Tween
                    {
                        Property = "ScrollPosition",
                        Interpolator = new Vector2Interpolator(),
                        Duration = 0.3f,
                        Easer = Quad.EaseInOut,
                        StartValueReader = new PropertyReader("ScrollPosition"),
                        EndValue = new Vector2(0, diff)
                    };
                    tween.Play(_inner);    
                }
                else
                {
                    //Defer(delegate
                    //          {
                    //              _inner.ScrollPosition = new Vector2(_inner.ScrollPosition.x, diff); // scroll to the max
                    //          }, 1);
                    _inner.ScrollPosition = new Vector2(_inner.ScrollPosition.x, diff); // scroll to the max
                }
            }
        }

        #endregion

        #region Console commands

        public void Write(string text)
        {
            Text += text;
        }

        public void Writeln(string text)
        {
            Text += text + "\n";
        }

        public void Writeln(string text, bool prependNewLine)
        {
            if (prependNewLine)
                text = "\n" + text;
            Writeln(text);
        }

        public void Prompt(string prompt)
        {
            Text += "\n" + prompt + " ";
        }

        public void Prompt()
        {
            Prompt(PromptText);
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            base.Dispose();
            //_timer.Dispose();

            _inner.ResizeHandler -= OnResize;
            _display.ResizeHandler -= OnResize;

            _txtInput.Changing -= OnInputTextChanging;
            _txtInput.Change -= OnInputTextChange;
            _txtInput.KeyDown -= OnInputKeyDown;
        }

        #endregion

    }
}