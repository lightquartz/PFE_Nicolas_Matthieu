using System.Collections;
using eDriven.Audio;
using eDriven.Core.Events;
using eDriven.Core.Util;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Cursor;
using eDriven.Gui.Layout;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

public class CursorDemo : Gui
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        LayoutDescriptor = LayoutDescriptor.Absolute;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        Hashtable buttonStyles = new Hashtable
                                      {
                                          {"cursor", "pointer"}
                                      };
        #region VBox

        VBox vbox = new VBox
                        {
                            HorizontalCenter = 0,
                            VerticalCenter = 0,
                            Padding = 10,
                            VerticalSpacing = 10
                        };
        AddChild(vbox);

        #endregion

        #region HBox

        HBox hbox = new HBox();
        hbox.Click += delegate(Event e)
        {
            MouseEvent me = (MouseEvent)e;
            if (me.Target is Button)
                AudioPlayerMapper.GetDefault().PlaySound("button_click");
        };
        vbox.AddChild(hbox);

        #endregion

        #region Cursor package 1

        Button btnCursor1 = new Button
        {
            Text = "Load cursor package 1",
            FocusEnabled = false,
            StyleMapper = "miki",
            Padding = 10,
            Styles = buttonStyles,
            Icon = (Texture) Resources.Load("Icons/drive_disk")
        };
        btnCursor1.Press += delegate
        {
            CursorManager.Instance.LoadPackage("Cursors/antialiased-classic/package");
        };
        hbox.AddChild(btnCursor1);

        #endregion

        #region Cursor package 2

        Button btnCursor2 = new Button
        {
            Text = "Load cursor package 2",
            FocusEnabled = false,
            StyleMapper = "miki",
            Padding = 10,
            Styles = buttonStyles,
            Icon = (Texture)Resources.Load("Icons/drive_disk")
        };
        btnCursor2.Press += delegate
        {
            CursorManager.Instance.LoadPackage("Cursors/blueglass-vista/package");
        };
        hbox.AddChild(btnCursor2);

        #endregion

        #region Spacer

        vbox.AddChild(new Spacer {Height = 30});

        #endregion

        #region Crosshair

        Button btnCrosshair = new Button
                                  {
                                      Text = "Crosshair",
                                      FocusEnabled = false,
                                      StyleMapper = "miki",
                                      Padding = 10,
                                      Styles = new Hashtable {{"cursor", "crosshair"}}
                                  };
        vbox.AddChild(btnCrosshair);

        #endregion

        #region Move

        Button btnMove = new Button
        {
            Text = "Move",
            FocusEnabled = false,
            StyleMapper = "miki",
            Padding = 10,
            Styles = new Hashtable { { "cursor", "move" } }
        };
        vbox.AddChild(btnMove);

        #endregion

        #region Help

        Button btnHelp = new Button
        {
            Text = "Help",
            FocusEnabled = false,
            StyleMapper = "miki",
            Padding = 10,
            Styles = new Hashtable { { "cursor", "help" } }
        };
        vbox.AddChild(btnHelp);

        #endregion

        #region Help

        Button btnEResize = new Button
        {
            Text = "E-Resize",
            FocusEnabled = false,
            StyleMapper = "miki",
            Padding = 10,
            Styles = new Hashtable { { "cursor", "e-resize" } }
        };
        vbox.AddChild(btnEResize);

        #endregion

        #region Spacer

        vbox.AddChild(new Spacer { Height = 30 });

        #endregion

        #region Cursor progress

        Button btnProgress = new Button
        {
            Text = "Show progress cursor (5 sec)",
            FocusEnabled = false,
            StyleMapper = "miki",
            Padding = 10,
            Styles = buttonStyles
        };
        btnProgress.Press += delegate
        {
            int id = CursorManager.Instance.SetCursor("wait", 1);
            Timer t = new Timer(5, 1);
            t.Complete += delegate
            {
                t.Dispose();
                CursorManager.Instance.RemoveCursor(id);
            };
            t.Start();

        };
        vbox.AddChild(btnProgress);

        #endregion
    }
}