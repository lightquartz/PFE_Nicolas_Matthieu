#region License

/*
 
Copyright (c) 2012 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using System;
using System.Collections;
using System.Collections.Generic;
using eDriven.Core.Caching;
using eDriven.Core.Util;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Components;
using eDriven.Playground.Demo.Styles;
using eDriven.Playground.eDriven.Demo.Gui.Code.Animation;
using UnityEngine;
using Component=eDriven.Gui.Components.Component;
using Event=eDriven.Core.Events.Event;

public class AnimationDemo : Gui
{
    #region Properties

    public float Speed = 300;
    public float Offset = 100;

    #endregion

    #region Members

    private readonly List<Component> _components = new List<Component>();
    private readonly List<Component> _toDestroy = new List<Component>();

    private Button _btnAddButton;
    private Button _btnAddPanel;

    private CheckBox _chkMove;
    private CheckBox _chkAuto;
    private CheckBox _chkRotate;
    private CheckBox _chkScaleX;
    private CheckBox _chkScaleY;
    private CheckBox _chkTint;
    private CheckBox _chkAlpha;
    private Slider _slider;

    private Container _canvas;

    private Hashtable _buttonStyles;

    private readonly Timer _autoTimer = new Timer(1) { TickOnStart = true };

    #endregion

    protected override void OnInitialize()
    {
        base.OnInitialize();

        Layout = new AbsoluteLayout();
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        Button button = new HesitantButton
        {
            Bottom = 10,
            HorizontalCenter = 0,
            FocusEnabled = false,
            Styles = new Hashtable 
                                { 
                                    {"buttonStyle", EDrivenButtonStyle.Instance} 
                                }
        };
        AddChild(button);

        new TextRotator
            {
                Delay = 5, // 5 seconds delay
                Lines = new[]
                                {
                                    "Animation Demo :)", 
                                    "Created using eDriven.Gui system",
                                    "Author: Danko Kozar"
                                },
                Callback = delegate(string line) { button.Text = line; }
            }
            .Start();

        #region Controls

        HBox hbox = new HBox
        {
            X = 10,
            Y = 10,
            Padding = 10,
            VerticalAlign = VerticalAlign.Middle
        };
        hbox.SetStyle("showBackground", true);
        AddChild(hbox);

        _btnAddButton = new Button
        {
            Text = "New button",
            FocusEnabled = false,
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/shape_square_add")
        };
        _btnAddButton.Click += delegate { CreateControl(ControlType.Button); };
        hbox.AddChild(_btnAddButton);

        _btnAddPanel = new Button
        {
            Text = "New panel",
            FocusEnabled = false,
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/application_add")
        };
        _btnAddPanel.Click += delegate { CreateControl(ControlType.Panel); };
        hbox.AddChild(_btnAddPanel);

        Button btnClear = new Button
        {
            Text = "Clear",
            FocusEnabled = false,
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/cancel")
        };
        btnClear.Click += delegate
        {
            var length = _components.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                _canvas.RemoveChild(_components[i]);
            }
            _components.Clear();
        };
        hbox.AddChild(btnClear);

        _chkAuto = new CheckBox
        {
            Text = "Auto",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = false
        };
        _chkAuto.Click += delegate
                              {
                                  _autoTimer.Delay = 1/_slider.Value;
                                  if (_chkAuto.Selected)
                                  {
                                      _autoTimer.Tick += OnTimerTick;
                                      _autoTimer.Start();
                                  } else
                                  {
                                      _autoTimer.Tick -= OnTimerTick;
                                      _autoTimer.Stop();
                                  }
                                  _slider.Visible = _slider.IncludeInLayout = _chkAuto.Selected;
                              };
        hbox.AddChild(_chkAuto);

        _slider = new Slider
        {
            Width = 100,
            Orientation = SliderOrientation.Horizontal,
            StyleMapper = "slider_volume",
            MinValue = 0.3f,
            MaxValue = 10,
            Value = 3,
            Visible = false,
            IncludeInLayout = false
        };
        hbox.AddChild(_slider);

        _slider.Change += delegate
                              {
                                  _autoTimer.Delay = 1/_slider.Value;
                              };

        _chkMove = new CheckBox
        {
            Text = "Move Y",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        //_chkMove.Click += delegate { };
        hbox.AddChild(_chkMove);

        _chkRotate = new CheckBox
        {
            Text = "Rotate",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        hbox.AddChild(_chkRotate);

        _chkScaleX = new CheckBox
        {
            Text = "Scale X",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        hbox.AddChild(_chkScaleX);

        _chkScaleY = new CheckBox
        {
            Text = "Scale Y",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        hbox.AddChild(_chkScaleY);

        _chkTint = new CheckBox
        {
            Text = "Tint",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        hbox.AddChild(_chkTint);

        _chkAlpha = new CheckBox
        {
            Text = "Alpha",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = false
        };
        hbox.AddChild(_chkAlpha);

        #endregion

        _canvas = new Container {Left = 0, Right = 0, Top = 0, Bottom = 0, ClipContent = false, MouseEnabled = false};
        AddChild(_canvas);

        hbox.BringToFront();
    }

    private void OnTimerTick(Event e)
    {
        CreateControl(ControlType.Button);
    }

    private void CreateControl(ControlType type)
    {
        if (null == _buttonStyles)
        {
            _buttonStyles = new Hashtable
                               {
                                   {"buttonStyle", ButtonStyle2.Instance}
                               };
        }

        Component cmp;
        switch (type)
        {
            case ControlType.Button:
                cmp = new Button
                {
                    Width = 300,
                    Height = 300,
                    FocusEnabled = false,
                    Text = @"eDriven.Gui
---
Unity3d GUI
Framework",
                    Tooltip = @"Nice, eh? :)",
                    ToggleMode = true,
                    Styles = _buttonStyles
                };
                break;
            case ControlType.Panel:
                cmp = new ThePanel { ImageUrl = "http://dankokozar.com/images/danko3.jpg" };
                break;
            default:
                throw new Exception("Unknown type");
        }

        cmp.X = Screen.width;
        cmp.Y = (Screen.height - 300)*0.5f;

        _canvas.AddChild(cmp);
        _components.Add(cmp);
    }

    private void DestroyControl(Component child)
    {
        _canvas.RemoveChild(child);
        child.Dispose();
        _components.Remove(child);
    }

// ReSharper disable UnusedMember.Local
    void Update()
// ReSharper restore UnusedMember.Local
    {
        //Debug.Log("Children.Count: " + Children.Count);
        foreach (var child in _components)
        {
            child.X-=Speed*Time.deltaTime;

            if (child.X < 0 - child.Width - Offset) {
                _toDestroy.Add(child);
            }

            var factor = Mathf.Sin(child.X/100);

            if (_chkMove.Selected)
            {
                var h = Screen.height - child.Height;
                var jump = h - h * (0.2f + (1 - Mathf.Abs(Mathf.Sin(child.X / 100))) * 0.6f);
                child.Y = Screen.height - child.Height - jump;
            }

            if (_chkRotate.Selected)
            {
                child.Rotation--;
            }

            if (_chkScaleX.Selected || _chkScaleY.Selected)
            {
                var scale = 1 + factor / 2; // the [0.5, 1.5] range
                child.Scale = new Vector2(
                    _chkScaleX.Selected ? scale : 1, 
                    _chkScaleY.Selected ? scale : 1
                );
            }

            if (_chkTint.Selected)
            {
                var r = (1 + Mathf.Sin(child.X/100)) * 0.5f; // the [0, 1] range
                var g = (1 + Mathf.Sin(child.X / 200)) * 0.5f; // the [0, 1] range
                var b = (1 + Mathf.Sin(child.X / 300)) * 0.5f; // the [0, 1] range
                child.Color = new Color(r, g, b);
            }

            if (_chkAlpha.Selected)
            {
                var alpha = (1 + Mathf.Sin(child.X / 100)) * 0.5f; // the [0, 1] range
                child.Alpha = alpha;
            }
        }

        if (_toDestroy.Count > 0)
        {
            foreach (Component comp in _toDestroy)
            {
                DestroyControl(comp);
            }
            _toDestroy.Clear();
        }
    }

    private enum ControlType
    {
        Button, Panel
    }
}