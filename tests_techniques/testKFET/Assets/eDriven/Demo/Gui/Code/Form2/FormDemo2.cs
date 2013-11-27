using System.Collections;
using System.Collections.Generic;
using System.Text;
using eDriven.Core.Caching;
using eDriven.Core.Data.Collections;
using eDriven.Core.Events;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Cursor;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Form;
using eDriven.Gui.Plugins;
using UnityEngine;
using Event = eDriven.Core.Events.Event;

public class FormDemo2 : eDriven.Gui.Gui
{
    #region Tweens

    //private readonly TweenFactory _modalOverlayFadeIn = new TweenFactory(new ZeroFadeIn { Duration = 0.35f });

    //private readonly TweenFactory _dialogShowEffect = new TweenFactory(
    //    new Sequence(
    //        new Action(delegate { AudioPlayerMapper.Instance.PlaySound("dialog_open"); }), // dialog_open
    //        new FadeIn { Duration = 0.35f }
    //    )
    //);

    #endregion

    protected override void OnStart()
    {
        base.OnStart();

        LayoutDescriptor = eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleCenter;
        CursorManager.Enabled = false;
    }

    override protected void CreateChildren()
    {
        base.CreateChildren();

        Dialog dialog = new Dialog {Padding = 10, Title = "Form Demo 2"};
        AddChild(dialog);

        Form form = new Form {PercentWidth = 100, Padding = 0};
        dialog.AddContentChild(form);

        #region Form items

        List list = new List
        {
            //Width = 250,
            PercentWidth = 100,
            //PercentHeight = 100,
            RequireSelection = true,
            SelectedValue = "Sine",
            DataProvider = new List<object>
                                               {
                                                   new ListItem("Back", "Back"),
                                                   new ListItem("Bounce", "Bounce"),
                                                   new ListItem("Circ", "Circ"),
                                                   new ListItem("Cubic", "Cubic"),
                                                   new ListItem("Elastic", "Elastic"),
                                                   new ListItem("Expo", "Expo"),
                                                   new ListItem("Linear", "Linear"),
                                                   new ListItem("Quad", "Quad"),
                                                   new ListItem("Quart", "Quart"),
                                                   new ListItem("Quint", "Quint"),
                                                   new ListItem("Sine", "Sine")
                                               }
        };
        form.AddField("list", "List:", list);

        ComboBox cb = new ComboBox
        {
            //Width = 250,
            PercentWidth = 100,
            //Editable = true,
            DataProvider = new List<object>
                                                 {
                                                     new ListItem("Back", "Back"),
                                                     new ListItem("Bounce", "Bounce"),
                                                     new ListItem("Circ", "Circ"),
                                                     new ListItem("Cubic", "Cubic"),
                                                     new ListItem("Elastic", "Elastic"),
                                                     new ListItem("Expo", "Expo"),
                                                     new ListItem("Linear", "Linear"),
                                                     new ListItem("Quad", "Quad"),
                                                     new ListItem("Quart", "Quart"),
                                                     new ListItem("Quint", "Quint"),
                                                     new ListItem("Sine", "Sine")
                                                 }
        };
        form.AddField("combo1", "Combo 1:", cb);

        //cb.Opening += delegate (Event evt)
        //                  {
        //                      Debug.Log("Opening");
        //                      //evt.PreventDefault();
        //                  };
        //cb.Closing += delegate(Event evt) { 
        //    Debug.Log("Closing");
        //    //evt.PreventDefault();
        //};
        //cb.Open += delegate { Debug.Log("Open"); };
        //cb.Close += delegate { Debug.Log("Close"); };
        
        //cb.AddEventListener(IndexChangeEvent.SELECTED_INDEX_CHANGED, delegate (Event e)
        //                                     {
        //                                         IndexChangeEvent ice = (IndexChangeEvent)e;
        //                                         Debug.Log("Index changed from " + ice.OldIndex + " to " + ice.Index);
        //                                     });
        //cb.SelectedIndexChangedHandler += delegate(Event e)
        //                                    {
        //                                        IndexChangeEvent ice = (IndexChangeEvent)e;
        //                                        Debug.Log("Index changed from " + ice.OldIndex + " to " + ice.Index);
        //                                    };
        
        ComboBox cb2 = new ComboBox
        {
            //Width = 250,
            PercentWidth = 100,
            DataProvider = new List<object>
                                                 {
                                                     new ListItem("Failure", "Failure"),
                                                     new ListItem("Teaches", "Teaches"),
                                                     new ListItem("Success", "Success")
                                                 }
        };
        form.AddField("combo2", "Combo 2:", cb2);

        ComboBox cb3 = new ComboBox
        {
            //Width = 250,
            PercentWidth = 100,
            MaxPopupHeight = 200,
            DataProvider = new List<object>
                                                 {
                                                     new ListItem("Back", "Back"),
                                                     new ListItem("Bounce", "Bounce"),
                                                     new ListItem("Circ", "Circ"),
                                                     new ListItem("Cubic", "Cubic"),
                                                     new ListItem("Elastic", "Elastic"),
                                                     new ListItem("Expo", "Expo"),
                                                     new ListItem("Linear", "Linear"),
                                                     new ListItem("Quad", "Quad"),
                                                     new ListItem("Quart", "Quart"),
                                                     new ListItem("Quint", "Quint"),
                                                     new ListItem("Sine", "Sine")
                                                 }
        };
        form.AddField("combo3", "Combo 3:", cb3);

        TextField txtSubject = new TextField
                                   {
                                       FocusEnabled = true,
                                       PercentWidth = 100,
                                       Optimized = true
                                   };
        form.AddField("subject", "Subject:", txtSubject/*, eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleLeft*/);

        CheckBox chk = new CheckBox { StyleMapper = "checkbox1" };
        form.AddField("checkbox", "Checkbox:", chk, eDriven.Gui.Layout.LayoutDescriptor.HorizontalMiddleLeft);

        #endregion

        #region Buttons

        Button btnSet = new Button
                            {
                                Text = "Set data (true/Expo/Expo)",
                                StyleMapper = "miki",
                                Icon = ImageLoader.Instance.Load("Icons/arrow_up")
                            };
        btnSet.Press += delegate
                            {
                                form.Data = new Hashtable
                                                {
                                                    {"subject", "The subject"},
                                                    //{"message", "This is the message..."}
                                                    {"checkbox", true},
                                                    {"combo1", "Expo"},
                                                    {"list", "Expo"}
                                                };
                            };
        dialog.ButtonGroup.AddChild(btnSet);
        
        Button btnSet2 = new Button
        {
            Text = "Set data (false/Circ/Sine)",
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/arrow_up")
        };
        btnSet2.Press += delegate
        {
            form.Data = new Hashtable
                                                {
                                                    {"subject", "The subject 2"},
                                                    //{"message", "This is the message..."}
                                                    {"checkbox", false},
                                                    {"combo1", "Circ"},
                                                    {"list", "Sine"}
                                                };
        };
        dialog.ButtonGroup.AddChild(btnSet2);
        
        dialog.ButtonGroup.AddChild(new Spacer {Width = 20});

        Button btnGet = new Button
                            {
                                Text = "Get data",
                                StyleMapper = "miki",
                                Icon = ImageLoader.Instance.Load("Icons/arrow_down")
                            };
        btnGet.Press += delegate
                            {
                                StringBuilder sb = new StringBuilder();
                                int count = form.Data.Count;
                                int index = 0;
                                foreach (DictionaryEntry entry in form.Data)
                                {
                                    if (index < count - 1)
                                        sb.AppendLine(string.Format(@"[{0}]: {1}", entry.Key, entry.Value));
                                    else
                                        sb.Append(string.Format(@"[{0}]: {1}", entry.Key, entry.Value));
                                    //sb.AppendLine();
                                    index++;
                                }

                                Alert.Show("This is the form data", sb.ToString(), AlertButtonFlag.Ok);
                            };
        dialog.ButtonGroup.AddChild(btnGet);

        #endregion

        // focus
        list.SetFocus();

        dialog.Plugins.Add(new TabManager());
    }
}