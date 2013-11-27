using System.Collections;
using System.Collections.Generic;
using eDriven.Animation;
using eDriven.Audio;
using eDriven.Core.Caching;
using eDriven.Core.Data.Collections;
using eDriven.Core.Events;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.DragDrop;
using eDriven.Gui.Layout;
using eDriven.Playground.Demo.Components;
using eDriven.Playground.Demo.Gui;
using eDriven.Playground.Demo.Gui.Styles;
using eDriven.Playground.Demo.Models;
using eDriven.Playground.Demo.Styles;
using eDriven.Playground.Demo.Tweens;
using UnityEngine;
using Component=eDriven.Gui.Components.Component;
using Event=eDriven.Core.Events.Event;
using LayoutDescriptor = eDriven.Gui.Layout.LayoutDescriptor;

public class DragDropDemo : Gui
{
    public int NumberOfSourceItems = 100;
    public int NumberOfDestinationItems = 100;

    private Box _box;

    private ButtonBar _bbMode;

    private Panel _pnlSource;
    private Panel _pnlDest;

    #region Effects

    private readonly TweenFactory _searchShowEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("portlet_add"); }),
            new FadeInLeft2()
        )
    ) { Delay = 0.07f };

    private readonly TweenFactory _panelShowEffect = new TweenFactory(
        new Sequence(
            new FadeIn()
        )
    ) { Delay = 0.07f };

    #endregion

    public enum DragMode
    {
        Move, CopyText, CopyColor, CopyTextAndColor, CopyData
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        OptionsModel.Instance.Volume = 0.5f;

        Stage.AutoLayout = false;
        _searchShowEffect.Callback = delegate(IAnimation anim)
        {
            Stage.AutoLayout = true;
        };

        Layout = new AbsoluteLayout();
    }

    protected override void CreateChildren()
    {
        base.CreateChildren();


        #region Heading

        Button button = new HesitantButton
        {
            HorizontalCenter = 0,
            Bottom = 10,
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
                                "Drag and Drop Demo", 
                                "Created using eDriven.Gui",
                                "Author: Danko Kozar",
                                "Drag items from the left panel (source)",
                                "Drop them to the right panel (destination)"
                            },
            Callback = delegate(string line) { button.Text = line; }
        }
            .Start();


        #endregion

        #region Box

        _box = new Box
                   {
                       HorizontalCenter = 0, VerticalCenter = 0,
                       LayoutDescriptor = LayoutDescriptor.HorizontalTopCenter,
                       ScrollContent = true
                   };

        // mandatory listeners
        _box.AddEventListener(MouseEvent.MOUSE_DOWN, OnMouseDown); // mouse down
        _box.AddEventListener(DragEvent.DRAG_ENTER, OnDragEnter); // drag enter
        _box.AddEventListener(DragEvent.DRAG_DROP, OnDragDrop); // drag drop (on drop target)

        // optional listeners
        _box.AddEventListener(DragEvent.DRAG_START, OnDragStart); // drag start(on drag initiator)
        _box.AddEventListener(DragEvent.DRAG_EXIT, OnDragExit); // drag exit (on drop target)
        _box.AddEventListener(DragEvent.DRAG_COMPLETE, OnDragComplete); // drag complete (on drag initiator)

        //_box.AddEventListener(MouseEvent.MOUSE_OVER, OnMouseOver);
        //_box.AddEventListener(MouseEvent.MOUSE_OUT, OnMouseOut);
        //_box.AddEventListener(MouseEvent.MOUSE_UP, OnMouseUp);

        AddChild(_box);

        #endregion

        #region Source

        _pnlSource = new Panel
                       {
                           Title = "Source",
                           MinWidth = 420,
                           MinHeight = 200,
                           MaxHeight = 600,
                           MouseEnabled = true,
                           Padding = 10,
                           Layout = new TileLayout
                           {
                               Direction = LayoutDirection.Horizontal,
                               HorizontalSpacing = 10,
                               VerticalSpacing = 10
                           }
                       };
        _pnlSource.SetStyle("addedEffect", _panelShowEffect);

        _box.AddChild(_pnlSource);

        #endregion

        #region Destination

        _pnlDest = new Panel
                          {
                              Title = "Destination",
                              MinWidth = 420,
                              MinHeight = 200,
                              MaxHeight = 600,
                              MouseEnabled = true,
                              Layout = new TileLayout
                              {
                                  Direction = LayoutDirection.Horizontal,
                                  HorizontalSpacing = 10,
                                  VerticalSpacing = 10
                              },
                              Padding = 10
                          };
        _pnlDest.SetStyle("addedEffect", _panelShowEffect);

        _box.AddChild(_pnlDest);

        InitChildren();

        #endregion

        #region Toolbar

        HBox hbox = new HBox { Padding = 10, VerticalAlign = VerticalAlign.Middle };
        hbox.SetStyle("showBackground", true);
        hbox.SetStyle("backgroundStyle", SearchContainerBackgroundStyle.Instance);
        hbox.SetStyle("addedEffect", _searchShowEffect);
        AddChild(hbox);

        Label lbl = new Label
        {
            Text = "Mode:"
        };
        lbl.SetStyle("labelStyle", SearchLabelStyle.Instance);
        hbox.AddChild(lbl);

        _bbMode = new ButtonBar
        {
            DataProvider = new List<object>
                                             {
                                                 new ListItem(DragMode.Move, "Move"),
                                                 new ListItem(DragMode.CopyText, "Copy text"),
                                                 new ListItem(DragMode.CopyColor, "Copy color"),
                                                 new ListItem(DragMode.CopyTextAndColor, "Copy text + color"),
                                                 new ListItem(DragMode.CopyData, "Copy data")
                                             },
            SelectedIndex = 0,
            FocusEnabled = false
        };
        hbox.AddChild(_bbMode);

        hbox.AddChild(new Spacer { Width = 50 });

        Button btnReset = new Button
        {
            Text = "Reset",
            Padding = 3,
            PaddingLeft = 4,
            PaddingRight = 4,
            Icon = ImageLoader.Instance.Load("Icons/arrow_refresh")
        };
        btnReset.Press += delegate { InitChildren(); };
        hbox.AddChild(btnReset);

        #endregion
    }

    private void InitChildren()
    {
        _pnlSource.RemoveAllContentChildren();
        _pnlDest.RemoveAllContentChildren();

        for (int i = 1; i <= NumberOfSourceItems; i++)
        {
            Label label = new Label { Text = "Green " + i, Width = 120, Height = 120, ProcessClicks = true, Data = i*100};
            label.SetStyle("labelStyle", GreenLabelStyle.Instance);
            _pnlSource.AddContentChild(label);
        }

        for (int i = 1; i <= NumberOfDestinationItems; i++)
        {
            Label label = new Label { Text = "Yellow" + i, Width = 120, Height = 120, ProcessClicks = true };
            label.SetStyle("labelStyle", YellowLabelStyle.Instance);
            _pnlDest.AddContentChild(label);
        }
    }

    #region Drag & drop handlers

    private void OnMouseDown(Event e)
    {
        Label comp = e.Target as Label;

        if (null == comp)
            return;

        // check if dragged item is child of _pnlSource or _pnlDest
        if (_pnlSource.ContentContains(comp) || _pnlDest.ContentContains(comp))
        {
            DragSource dataSource = new DragSource();
            dataSource.AddData(comp.Text, "text"); // add text for COPY_TEXT mode
            //dataSource.AddData(comp.StyleName, "style"); // add text for COPY_STYLE mode
            //dataSource.AddData(comp, "control"); // add reference to control for Move mode

            DragDropManager.DoDrag(comp, dataSource, (MouseEvent)e, null, 0, 0, 0.5f, false);
            /*new DragOption(DragOptionType.ProxyVisible, false),
            new DragOption(DragOptionType.FeedbackVisible, false)*/
        }
    }

    private void OnDragEnter(Event e)
    {
        //Debug.Log("OnDragEnter: " + e.Target.GetType().Name);
        Component comp = (Component)e.Target;
        if (null == comp)
            return;

        DragMode mode = (DragMode)_bbMode.SelectedValue;

        AudioPlayerMapper.GetDefault().PlaySound("pager_click", new AudioOption(AudioOptionType.Volume, 0.3f));

        // check if drag enter item is child of _pnlDest or _pnlSource itself
        // allow the whole box to be drop target when moving
        if (_pnlDest.ContentContains(comp) || (mode == DragMode.Move && comp == _pnlDest.ContentGroup))
        {
            DragDropManager.AcceptDragDrop((Component) e.Target);
            //new DragOption(DragOptionType.ProxyVisible, false), 
            //    new DragOption(DragOptionType.OverlayVisible, false),
            //    new DragOption(DragOptionType.FeedbackVisible, false)
            //if ((DragMode)_bbMode.SelectedValue != DragMode.Move)
            //    DragDropManager.ShowFeedback(DragDropManager.Action.Copy);
            DragDropManager.ShowFeedback(DragDropManager.Action.Move);
        }
    }

    private void OnDragDrop(Event e)
    {
        DragEvent dragEvent = (DragEvent) e;

        //Debug.Log("OnDragDrop: " + e.Target.GetType().Name);
        Component src = dragEvent.DragInitiator; //(UiComponent)dragEvent.DragSource.Formats["control"];
        Component dest = (Component)e.Target;

        switch ((DragMode)_bbMode.SelectedValue)
        {
            case DragMode.Move:
                // check if drag drop item is child of _pnlDest
                if (dest == _pnlDest.ContentGroup)
                {
                    src.Parent.RemoveChild(src); //src.RemoveFromParent();
                    _pnlDest.AddContentChild(src);
                } 
                else if (_pnlDest.ContentContains(dest))
                {
                    src.Parent.RemoveChild(src); // NOTE: needed for drag&drop, because src and dest could be the same
                    _pnlDest.AddContentChildAt(_pnlDest.GetContentChildIndex(dest), src);
                }
                break;

            case DragMode.CopyText:
                // check if drag drop item is child of _pnlDest
                if (_pnlDest.ContentContains(dest) && _pnlDest != dest)
                {
                    ((Label)dest).Text = (string)dragEvent.DragSource.Formats["text"];
                }
                break;

            case DragMode.CopyColor:
                // check if drag drop item is child of _pnlDest
                if (_pnlDest.ContentContains(dest) && _pnlDest != dest)
                {
                    dest.SetStyle("labelStyle", dragEvent.DragInitiator.GetStyle("labelStyle"));
                }
                break;
            case DragMode.CopyTextAndColor:
                // check if drag drop item is child of _pnlDest
                if (_pnlDest.ContentContains(dest) && _pnlDest != dest)
                {
                    ((Label)dest).Text = (string)dragEvent.DragSource.Formats["text"];
                    dest.SetStyle("labelStyle", dragEvent.DragInitiator.GetStyle("labelStyle"));
                    //dest.StyleName = (string)dragEvent.DragSource.Formats["style"];
                }
                break;

            case DragMode.CopyData:
                // check if drag drop item is child of _pnlDest
                if (_pnlDest.ContentContains(dest) && _pnlDest != dest)
                {
                    dest.Data = dragEvent.DragInitiator.Data;
                }
                break;
        }

        AudioPlayerMapper.GetDefault().PlaySound("drag_drop", new AudioOption(AudioOptionType.Volume, 0.3f));
    }

    private void OnDragStart(Event e)
    {
        //Debug.Log("OnDragStart: " + e.Target.GetType().Name);
    }

    private void OnDragComplete(Event e)
    {
        //Debug.Log("OnDragComplete: " + e.Target.GetType().Name);
    }

    private void OnDragExit(Event e)
    {
        //Debug.Log("OnDragExit: " + e.Target.GetType().Name);
    }

    #endregion

}