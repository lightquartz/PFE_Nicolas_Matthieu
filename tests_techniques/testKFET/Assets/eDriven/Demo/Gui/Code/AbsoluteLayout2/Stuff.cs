using eDriven.Core.Data.Collections;
using eDriven.Core.Events;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Layout;
using eDriven.Gui.Styles;
using eDriven.Playground.Demo.Helpers;
using UnityEngine;

namespace eDriven.Playground.Demo.Gui
{
    public class Stuff : Container
    {
        public Stuff()
        {
            SetStyle("backgroundStyle", GreyTransparentBackground.Instance);
            SetStyle("showBackground", true);
            SetStyle("showOverlay", true);
            Padding = 10;
            MinWidth = 300;
            MinHeight = 300;
            ScrollContent = true;
        }

        override protected void InitializationComplete()
        {
            base.InitializationComplete();

            Layout = new BoxLayout
                         {
                             Direction = LayoutDirection.Horizontal,
                             HorizontalAlign = HorizontalAlign.Left,
                             HorizontalSpacing = 10
                         };
        }

        override protected void CreateChildren()
        {
            base.CreateChildren();

            #region List 1

            List list1 = new List
                             {
                                 PercentWidth = 100,
                                 PercentHeight = 100,
                                 ScrollContent = true,
                                 DataProvider = EasingHelper.GetEasingList(),
                                 SelectedValue = "Bounce",
                                 ItemPercentWidth = 100,
                                 MinWidth = 200
                             };
            //list1.Height = 300;
            AddChild(list1);

            #endregion

            #region List 2

            VBox vboxPagingList = new VBox {PercentWidth = 100, PercentHeight = 100};
            //vboxPagingList.DrawBackground = true;
            //vboxPagingList.DrawOverlay = true; 
            AddChild(vboxPagingList);

            List list2 = new List
                             {
                                 PercentWidth = 100,
                                 PercentHeight = 100,
                                 ScrollContent = true,
                                 DataProvider =
                                     Application.isWebPlayer
                                         ? ResolutionHelper.GetResolutionList()
                                         : ResolutionHelper.GetDummyResolutionList(),
                                 ItemPercentWidth = 100
                             };
            //list2.Height = 450;
            vboxPagingList.AddChild(list2);

            //list2.KeyDown += delegate
            list2.AddEventListener(KeyboardEvent.KEY_DOWN, delegate
                                                               {
                                                                   var sp = list2.ScrollPosition;
                                                                   var sp2 = new Vector2(sp.x, sp.y + 10);
                                                                   list2.ScrollPosition = sp2;
                                                                   Debug.Log("Scroll down: " + sp2);
                                                               });

            HBox hboxButtons = new HBox();
            vboxPagingList.AddChild(hboxButtons);

            Button btnUp = new Button {Text = "Up"};
            btnUp.MouseDown += delegate
                                   {
                                       var sp = list2.ScrollPosition;
                                       var sp2 = new Vector2(sp.x, sp.y - 10);
                                       list2.ScrollPosition = sp2;
                                       Debug.Log("Scroll up: " + sp2);
                                   };
            hboxButtons.AddChild(btnUp);

            Button btnDown = new Button {Text = "Down"};
            btnDown.MouseDown += delegate
                                     {
                                         var sp = list2.ScrollPosition;
                                         var sp2 = new Vector2(sp.x, sp.y + 10);
                                         list2.ScrollPosition = sp2;
                                         Debug.Log("Scroll down: " + sp2);
                                     };
            hboxButtons.AddChild(btnDown);

            #endregion

            #region List 3

            VBox vbox2 = new VBox {VerticalSpacing = 10, PercentWidth = 100, PercentHeight = 100};
            AddChild(vbox2);

            ComboBox cmb = new ComboBox
                               {
                                   Width = 400,
                                   DataProvider = new System.Collections.Generic.List<object>
                                                      {
                                                          new ListItem(1, "failure"),
                                                          new ListItem(2, "failure teaches"),
                                                          new ListItem(3, "failure teaches success"),
                                                          new ListItem(4, "mikie..."),
                                                          new ListItem(5, "... by imagine")
                                                      }
                               };
            //cmb.Tooltip = "Choose quote here"; // doesn't work for now cause it has children
            //cmb.Height = 50;
            vbox2.AddChild(cmb);

            // combo 2
            cmb = new ComboBox
                      {
                          Width = 400,
                          DataProvider = new System.Collections.Generic.List<object>
                                             {
                                                 new ListItem(1, "Zagreb"),
                                                 new ListItem(2, "Slavonski Brod"),
                                                 new ListItem(3, "Kumrovec"),
                                                 new ListItem(4, "Jajce"),
                                                 new ListItem(5, "Žminj"),
                                                 new ListItem(6, "Babina Greda"),
                                                 new ListItem(7, "Mrzlo Polje"),
                                                 new ListItem(8, "Sraćinec")
                                             }
                      };
            //cmb.Tooltip = "Choose your city"; // doesn't work for now cause it has children
            //cmb.Height = 50;
            vbox2.AddChild(cmb);

            List list3 = new List
                             {
                                 PercentWidth = 100,
                                 PercentHeight = 100,
                                 ScrollContent = true,
                                 DataProvider = EasingHelper.GetEasingInOutList(),
                                 SelectedValue = "Out",
                                 Enabled = false
                             };
            //list3.Height = 300;
            //list3.ItemPercentWidth = 100;
            vbox2.AddChild(list3);

            //Button btn = new Button
            //                 {
            //                     Text = "Gimme the popup!",
            //                     //StyleDeclaration = new StyleDeclaration(RectButtonSkin.Instance, "button"),
            //                 };

            //btn.Click += delegate
            //                 {
            //                     var dialog = new FormDialog();
            //                     dialog.SetStyle("showEffect", new DialogFallDown());
            //                     PopupManager.Instance.AddPopup(dialog);
            //                 };
            //vbox2.AddChild(btn);

            Button btn = new Button
                             {
                                 Text = "Show alert",
                                 //StyleDeclaration = new StyleDeclaration(RectButtonSkin.Instance, "button"),
                             };

            btn.Click += delegate
                             {
                                 Alert.Show("Hi!", "This is the alert", AlertButtonFlag.Ok);
                             };
            vbox2.AddChild(btn);

            #endregion
        }
    }
}