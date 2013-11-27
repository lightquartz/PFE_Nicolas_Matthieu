#region License

/*
 
Copyright (c) 2013 Danko Kozar. All rights reserved.
 
*/

#endregion License

using System.Collections.Generic;
using eDriven.Core.Caching;
using eDriven.Gui.Components;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Plugins;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

namespace eDriven.Playground.eDriven.Demo.Gui.Code.Console
{
    /// <summary>
    /// The console window
    /// This is an example of a dialog containing the (custom) control
    /// The point of coding the control separately is being able to use it in other scenarios (not only within the dialog)
    /// </summary>
    public class ConsoleWindow : Dialog
    {
        #region Members

        private ConsoleField _console;
        private Button _btnHelp;
        private Button _btnExit;
        private Button _btnClose;

        #endregion

        #region Constructor

        public ConsoleWindow()
        {
            Title = "Console"; // default title
            TitleLabel.Texture = ImageLoader.Instance.Load("Icons/comment_edit");
            Padding = 10;
            Width = 400;
            MinWidth = 400;
            MinHeight = 300;
            Resizable = true;
            Padding = 8;
            PaddingBottom = 0; // because we have a button group
            CloseOnEsc = true;
            MouseDown += OnMouseDown;
        }

        #endregion

        #region Writing to console

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
                if (value == _text)
                    return;

                _text = value;
                _textChanged = true;
                InvalidateProperties();
            }
        }

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

        #endregion

        #region Methods

        override protected void CreateChildren()
        {
            base.CreateChildren();

            #region Console

            _console = new ConsoleField
                           {
                               FocusEnabled = true,
                               PercentWidth = 100,
                               PercentHeight = 100,
                               MinWidth = 150,
                               MinHeight = 100,
                               ProcessKeys = true,
                               InputMode = true
                           };
            AddContentChild(_console);

            #endregion

            #region Buttons

            ButtonGroup.AddChild(new Spacer { PercentWidth = 100 });

            _btnHelp = new Button
                           {
                               Text = "Help",
                               FocusEnabled = true,
                               StyleMapper = "miki",
                               Icon = ImageLoader.Instance.Load("Icons/information")
                           };
            _btnHelp.Press += Help;
            ButtonGroup.AddChild(_btnHelp);

            _btnExit = new Button
                           {
                               Text = "Exit",
                               // ReSharper disable RedundantNameQualifier
                               Icon = ImageLoader.Instance.Load("Icons/cancel"),
                               StyleMapper = "miki"
                               // ReSharper restore RedundantNameQualifier
                           };
            _btnExit.Press += Cancel;
            ButtonGroup.AddChild(_btnExit);

            _btnClose = new Button
                            {
                                Icon = (Texture)Resources.Load("Icons/close_16"),
                                Width = 20,
                                Height = 20,
                            };
            _btnClose.Click += Cancel;
            Tools.AddChild(_btnClose);

            #endregion

            Plugins.Add(new TabManager());
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (_textChanged)
            {
                _textChanged = false;
                _console.Text = _text;
            }
        }

        private void OnMouseDown(Event e)
        {
            SetFocus();
        }

        public override void SetFocus()
        {
            _console.SetFocus();
        }

        public override List<DisplayListMember> GetTabChildren()
        {
            var list = new List<DisplayListMember> {_console, _btnHelp, _btnExit};
            return  list;
        }

        #endregion

        #region Handlers

        private static void Help(Event e)
        {
            Alert.Show("Info", "Help button pressed", AlertButtonFlag.Ok);
            //DeferManager.Instance.Defer(delegate() { ExecCallback(SUBMIT); }, 1);
        }

        private void Cancel(Event e)
        {
            //deffering action for one frame (not to be re-clicked by the same click that closed the alert)
            Defer(delegate { ExecCallback(CLOSE); }, 1);
            //PopupManager.Instance.RemovePopup(this);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposing is particularly important in this demo,
        /// since we need to make sure we got rid of keyboard listeners!
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            MouseDown -= OnMouseDown;
            
            _btnHelp.Press -= Help;
            _btnExit.Press -= Cancel;
            _btnClose.Press -= Cancel;
        }

        #endregion

    }
}