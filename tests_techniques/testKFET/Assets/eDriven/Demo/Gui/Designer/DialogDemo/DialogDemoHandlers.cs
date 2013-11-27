using System;
using eDriven.Animation;
using eDriven.Audio;
using eDriven.Gui.Components;
using eDriven.Gui.Designer;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Managers;
using eDriven.Playground.Demo.Tweens;
using UnityEngine;
using Action=eDriven.Animation.Action;
using Component=eDriven.Gui.Components.Component;
using Event=eDriven.Core.Events.Event;

public class DialogDemoHandlers : MonoBehaviour {

	public void ClickHandler(Event e)
	{
        //Debug.Log("ClickHandler: " + e.Target);
        Alert.Show("Event", string.Format(@"[{0}] received:

Type: {1}
Target: {2}
CurrentTarget: {3}", e.GetType(), e.Type, e.Target, e.CurrentTarget), AlertButtonFlag.Ok);
	}

    public void ChangeButtonColor(Event e)
    {
        // GUI lookup example
        Button button = GuiLookup.GetAdapter<Button>(gameObject, "button1");
        if (null != button)
            button.Color = Color.green;
    }

    public void MouseOverHandler(Event e)
    {
        Debug.Log("MouseOverHandler: " + e.Target);
    }

    public void MouseOutHandler(Event e)
    {
        Debug.Log("MouseOutHandler: " + e.Target);
    }

    public void RightClickHandler(Event e)
    {
        Debug.Log("RightClickHandler: " + e.Target);
        Debug.Log("Loading level");
        Application.LoadLevel(1);
    }

    public void RightMouseUp(Event e)
    {
        Debug.Log("RightMouseUp: " + e.Target);
    }

    public void Test(Event e)
    {
        Debug.Log("Test: " + e.Target);
    }

    public void LoadLevel(Event e)
    {
        Debug.Log("Loading level 0");
        Application.LoadLevel(0);
    }

    public void Info(Event e)
    {
        Alert.Show(
            "Info", 
            "This is the info message. The time is: " + DateTime.Now.ToLongTimeString(), 
            AlertButtonFlag.Ok, 
            new AlertOption(AlertOptionType.HeaderIcon, Resources.Load("Icons/information")),
            new AlertOption(AlertOptionType.AddedEffect, _alertEffect)
        );
    }

    private readonly TweenFactory _alertEffect = new TweenFactory(
        new Sequence(
            new Action(delegate { AudioPlayerMapper.GetDefault().PlaySound("dialog_open"); }), // dialog_open
            new Jumpy()
        )
    );

    public void PopupDialog(Event e)
    {
        //Debug.Log("Popping up dialog");
        Component component = GuiLookup.Produce("factory1");
        //Debug.Log("Popping up " + component);
        if (null != component)
            PopupManager.Instance.AddPopup(component, false); // modal = false
    }

    public void RemoveDialog(Event e)
    {
        Button button = e.Target as Button;
        if (button != null)
        {
            Dialog dialog = GuiLookup.FindParent<Dialog>(button);
            PopupManager.Instance.RemovePopup(dialog);
        }
    }

    public void MaximizeDialog(Event e)
    {
        Button button = e.Target as Button;
        if (button != null)
        {
            Dialog dialog = GuiLookup.FindParent<Dialog>(button);

            if (DialogResizeUp.DialogSizes.ContainsKey(dialog))
            {
                button.Icon = (Texture)Resources.Load("Icons/maximize");
                button.Tooltip = "Maximize";
                dialog.Draggable = true;
                dialog.Resizable = true;
            }
            else
            {
                button.Icon = (Texture)Resources.Load("Icons/restore");
                button.Tooltip = "Restore";
                dialog.Draggable = false;
                dialog.Resizable = false;
            }

            _resizeEffect.Play(dialog);
        }
    }

    private readonly TweenFactory _resizeEffect = new TweenFactory(
        new DialogResizeUp()
    );
	
	public void PopupLogin(Event e)
    {
        //Debug.Log("Popping up dialog");
        Component component = GuiLookup.Produce("login");
        //Debug.Log("Popping up " + component);
        if (null != component)
            PopupManager.Instance.AddPopup(component, true); // modal = false
    }

    #region Resize demo

    //private Rectangle _dialogBounds;
    //private Dialog _dialog;
    //private Point _clickPoint;
    
    //public void StartResize(Event e)
    //{
    //    Debug.Log("StartResize");

    //    MouseEvent me = (MouseEvent) e;
    //    _clickPoint = me.GlobalPosition;

    //    Button button = e.Target as Button;
    //    if (button != null)
    //    {
    //        _dialog = GuiLookup.FindParent<Dialog>(button);
    //        _dialogBounds = _dialog.GlobalBounds; // copy old bounds (automatic clonning)
    //        SystemEventDispatcher.Instance.MouseMove += MouseMoveHandler; // subscribe
    //        SystemEventDispatcher.Instance.MouseUp += MouseUpHandler; // subscribe;
    //    }
    //}

    //private void MouseMoveHandler(Event e)
    //{
    //    MouseEvent me = (MouseEvent) e;
    //    Point delta = me.GlobalPosition.Subtract(_clickPoint);
    //    _dialog.Bounds = Rectangle.FromPositionAndDimensions(_dialogBounds.Position, _dialogBounds.Dimensions.Add(delta).Max(new Point(_dialog.MinWidth, _dialog.MinHeight)));
    //}

    //private void MouseUpHandler(Event e)
    //{
    //    SystemEventDispatcher.Instance.MouseMove -= MouseMoveHandler; // unsubscribe
    //    SystemEventDispatcher.Instance.MouseUp -= MouseUpHandler; // unsubscribe
    //}

    #endregion

}