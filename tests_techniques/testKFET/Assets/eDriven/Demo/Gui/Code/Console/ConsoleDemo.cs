#region License

/*
 
Copyright (c) 2013 Danko Kozar. All rights reserved.
 
*/

#endregion License

using eDriven.Animation;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Managers;
using eDriven.Playground.Demo.Tweens;
using eDriven.Playground.eDriven.Demo.Gui.Code.Console;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

public class ConsoleDemo : MonoBehaviour {

    private static readonly TweenFactory DialogEffect = new TweenFactory(new Jumpy());
    private static readonly TweenFactory ConsoleDialogEffect = new TweenFactory(new ShowEffect());

    public float ConsoleWidth = 600;
    public float ConsoleHeight = 400;

    #region Texts

    const string Text1 = @"Unity3d eDriven Console [Version 1.1.0001]
Copyright (c) 2013 Danko Kozar. All rights reserved.";

    const string Text2 = @"Unity3d eDriven Console [Version 1.1.0001]
Copyright (c) 2013 Danko Kozar. All rights reserved.";

    #endregion

    #region Object coloring

    private Color _oldColor;
    void OnMouseEnter()
    {
        if (MouseEventDispatcher.MouseTarget != null) // return if mouse over GUI
            return;
        _oldColor = renderer.material.color;
        renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        renderer.material.color = _oldColor;
    }

    #endregion

    void Start()
    {
        Dialog.AddedEffect = DialogEffect;
    }

    void OnMouseUp()
    {
        //Debug.Log("Object clicked");
        if (MouseEventDispatcher.MouseTarget != null) // return if mouse over GUI
            return;

        Camera cam = GameObject.FindWithTag("MainCamera").camera;
        Vector3 screenPos = cam.WorldToScreenPoint(gameObject.transform.position);
        
        ConsoleWindow win = new ConsoleWindow
                                   {
                                       Title = "Console for " + gameObject.name,
                                       Width = ConsoleWidth,
                                       Height = ConsoleHeight,
                                       X = screenPos.x - ConsoleWidth/2,
                                       Y = Screen.height - screenPos.y - ConsoleHeight/2
                                   };
        win.SetStyle("addedEffect", ConsoleDialogEffect);

        #region _test

        // win.Callback = delegate (string choice)
        //                    {
        //                        if (choice == Dialog.CLOSE || choice == Dialog.CANCEL)
        //                        {
        //                            Debug.Log("Dialog closed");
        //                        }
        //                    };

        #endregion

        win.AddEventListener(InputEvent.INPUT, OnInput);
        win.Writeln(gameObject.name == "PC1" ? Text1 : Text2);
        win.Prompt(@"C:\Users\Danko>"); // wait for input

        PopupManager.Instance.AddPopup(win, true, false); // modal, non centered

        renderer.material.color = _oldColor;
	}

    private static void OnInput(Event e)
    {
        InputEvent ie = (InputEvent) e;
        ConsoleField console = (ConsoleField)e.Target;

        Debug.Log("Command: " + ie.Input);
        if (ie.Input.Length > 0)
        {
            console.Writeln(string.Format("Nice to meet you {0}!", ie.Input), true);
        }

        console.Prompt(); // wait for input
    }
}
