using eDriven.Core.Caching;
using eDriven.Core.Data.Collections;
using eDriven.Core.Events;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using UnityEngine;
using Event = eDriven.Core.Events.Event;

public class AutoLayoutDemo : Gui
{
    private ComboBox _combo;

    protected override void OnCreationComplete()
    {
        base.OnCreationComplete();

        _panel.AddEventListener(MouseEvent.CLICK, delegate(Event e)
        {
            Button btn = e.Target as Button;
            if (null == btn)
                return;

//            Alert.Show(string.Format(@"Control clicked: {0}
//Data: {1}", e.Target, btn.Data), "Mouse click event!");

            //btn.Move(btn.X + 10, btn.Y);

            switch (_combo.SelectedIndex)
            {
                case 0:
                    //btn.XNonValidating += 10;
                    //btn.YNonValidating += 10;
                    btn.X += 10;
                    btn.Y += 10;
                    //_panel.ContentGroup.InvalidateDisplayList();
                    break;
                case 1:
                    //btn.Width += 10;
                    //btn.Height += 10;
                    //btn.SetActualSize(btn.Width + 10, btn.Height + 10);
                    btn.Width += 10;
                    btn.Height += 10;
                    break;
            }

            //btn.X += 10;
            //Debug.Log("btn.X: " + btn.X);
            //btn.ValidateNow();
        });
    }

    private Button _btnAddRect;
    //private Button _btnAddText;

    //private readonly TweenFactory _tweenFactory = new TweenFactory(typeof(FadeInUp));

    private int _count;

    private Panel _panel;

    #region Dummy text

//    private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam non urna purus. Suspendisse tincidunt scelerisque euismod. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent ipsum elit, consectetur ac scelerisque vitae, rhoncus porta nulla. In semper placerat sem nec consectetur. Donec mi arcu, tristique at viverra eget, accumsan at erat. Nulla ut ligula nibh, sit amet consequat neque. Aliquam a turpis sem, at dictum leo. Sed ut lacinia quam. Aenean facilisis vehicula lorem a rutrum.
//
//Nam dignissim consectetur sem, in ultricies turpis elementum at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam venenatis massa id velit eleifend ultrices. Cras sed arcu nec nisi hendrerit sollicitudin vel non eros. Sed nunc eros, auctor ut laoreet et, sodales non justo. Nulla id varius tortor. Sed ac malesuada lectus. Praesent lobortis mauris est. Nunc convallis ultrices augue vitae bibendum. Aenean non diam lacus. Quisque quis dui at erat viverra dictum. Vestibulum dolor risus, iaculis non laoreet vitae, laoreet nec ante. Integer rutrum sagittis lacus et vulputate. Nulla id fringilla tortor. Proin commodo nisl ut tortor tempor condimentum quis ut nibh. Sed lacinia sapien dolor, dapibus semper arcu.
//
//Nulla facilisi. Aenean interdum porta nisl non blandit. Nunc interdum faucibus nisi, eu iaculis libero consequat consectetur. Pellentesque tempus, neque facilisis tristique porta, dolor nisi elementum sapien, id auctor sem justo vel metus. Nulla non mi sit amet est imperdiet accumsan id ac odio. Aliquam semper lacinia lorem, eu sollicitudin libero ultricies vel. Proin a ultricies elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In pretium lorem in leo pretium non convallis magna ullamcorper. In bibendum erat a sem iaculis sit amet sollicitudin enim molestie. Vestibulum ut massa eget sem tempus varius ac in dolor. Nunc vitae nisl arcu. Fusce facilisis diam eu lectus posuere semper. Nullam metus nulla, malesuada in pharetra sit amet, dapibus sit amet enim. Etiam semper, felis quis venenatis hendrerit, mauris velit hendrerit lacus, sit amet semper arcu lacus eget mauris. Suspendisse vitae varius velit.";

    #endregion

    override protected void CreateChildren()
    {
        base.CreateChildren();

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

        _btnAddRect = new Button
                          {
                              Text = "New button",
                              FocusEnabled = false,
                              StyleMapper = "miki",
                              Icon = ImageLoader.Instance.Load("Icons/shape_square_add")
                          };
        _btnAddRect.Click += delegate { AddButton(); };
        hbox.AddChild(_btnAddRect);

        //_btnAddText = new Button
        //                  {
        //                      Text = "New text",
        //                      FocusEnabled = false,
        //                      StyleMapper = "miki",
        //                      Icon = ImageLoader.Instance.Load("Icons/page_white_text")
        //                  };
        //_btnAddText.Click += delegate { AddTextField(); };
        //hbox.AddChild(_btnAddText);

        Button btnClear = new Button
                              {
                                  Text = "Clear",
                                  FocusEnabled = false,
                                  StyleMapper = "miki",
                                  Icon = ImageLoader.Instance.Load("Icons/cancel")
                              };
        btnClear.Click += delegate
                              {
                                  _panel.RemoveAllContentChildren();
                                  _count = 0;
                              };
        hbox.AddChild(btnClear);

        //CheckBox chkScroll = new CheckBox
        //{
        //    Text = "Scroll content",
        //    FocusEnabled = false,
        //    StyleMapper = "checkbox",
        //    Selected = true
        //};
        //chkScroll.Click += delegate
        //{
        //    _panel.ScrollContent = chkScroll.Selected;
        //};
        //hbox.AddChild(chkScroll);

        hbox.AddChild(new Label {Text = "Action: "});

        _combo = new ComboBox
                             {
                                 //Width = 250,
                                 PercentWidth = 100,
                                 MaxPopupHeight = 200,
                                 DataProvider = new System.Collections.Generic.List<object>
                                                    {
                                                        new ListItem("Position", "Position"),
                                                        new ListItem("Size", "Size")
                                                    },
                                 SelectedIndex = 0
                             };
        hbox.AddChild(_combo);

        CheckBox chkAutoLayout = new CheckBox
        {
            Text = "Auto layout",
            FocusEnabled = false,
            StyleMapper = "checkbox",
            Selected = true
        };
        chkAutoLayout.Click += delegate
        {
            Debug.Log("Setting panel's auto layout to " + chkAutoLayout.Selected);
            _panel.AutoLayout = chkAutoLayout.Selected;
        };
        hbox.AddChild(chkAutoLayout);

        _panel = new Panel
                     {
                         Width = 600, 
                         Height = 600, 
                         Title = "Panel", 
                         Padding = 10, 
                         ScrollContent = true
                     };
        AddChild(_panel);

        #endregion
    }

    #region Helper

    private void AddButton()
    {
        _count++;
        Button btn = new Button
                         {
                             FocusEnabled = false,
                             Width = 200,
                             Height = 200,
                             MinWidth = 200,
                             MinHeight = 200,
                             StyleMapper = "miki",
                             Text = "Button " + _count,
                             Tooltip = "Resizable Button",
                             Data = _count
                         };

        //btn.SetStyle("addedEffect", _tweenFactory);
        _panel.AddContentChild(btn);
    }

    //private void AddTextField()
    //{
    //    TextField field = new TextField
    //                          {
    //                              Width = 300,
    //                              Height = 200,
    //                              MinWidth = 100,
    //                              MinHeight = 100,
    //                              ScrollContent = true,
    //                              Text = LoremIpsum,
    //                              Multiline = true,
    //                              Tooltip = "Resizable TextField"
    //                          };

    //    field.SetStyle("addedEffect", _tweenFactory);
    //    _panel.AddContentChild(field);
    //}

    #endregion

}