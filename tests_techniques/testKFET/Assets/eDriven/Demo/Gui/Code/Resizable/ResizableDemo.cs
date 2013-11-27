using eDriven.Animation;
using eDriven.Core.Caching;
using eDriven.Core.Events;
using eDriven.Gui;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Gui.Plugins;
using eDriven.Playground.Demo.Tweens;
using Component=eDriven.Gui.Components.Component;
using Event = eDriven.Core.Events.Event;
using Random = System.Random;

public class ResizableDemo : Gui
{
    protected override void OnStart()
    {
        base.OnStart();

        AddEventListener(MouseEvent.MOUSE_DOWN, delegate(Event e)
        {
            Component doc = e.Target as Component;
            if (null != doc)
                doc.BringToFront();
        });
    }

    private Button _btnAddRect;
    private Button _btnAddText;
    private Container _canvas;

    private readonly TweenFactory _tweenFactory = new TweenFactory(typeof(FadeInUp));

    private readonly Random _random = new Random();
    private int _count = 1;

    #region Dummy text

    private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam non urna purus. Suspendisse tincidunt scelerisque euismod. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent ipsum elit, consectetur ac scelerisque vitae, rhoncus porta nulla. In semper placerat sem nec consectetur. Donec mi arcu, tristique at viverra eget, accumsan at erat. Nulla ut ligula nibh, sit amet consequat neque. Aliquam a turpis sem, at dictum leo. Sed ut lacinia quam. Aenean facilisis vehicula lorem a rutrum.

Nam dignissim consectetur sem, in ultricies turpis elementum at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam venenatis massa id velit eleifend ultrices. Cras sed arcu nec nisi hendrerit sollicitudin vel non eros. Sed nunc eros, auctor ut laoreet et, sodales non justo. Nulla id varius tortor. Sed ac malesuada lectus. Praesent lobortis mauris est. Nunc convallis ultrices augue vitae bibendum. Aenean non diam lacus. Quisque quis dui at erat viverra dictum. Vestibulum dolor risus, iaculis non laoreet vitae, laoreet nec ante. Integer rutrum sagittis lacus et vulputate. Nulla id fringilla tortor. Proin commodo nisl ut tortor tempor condimentum quis ut nibh. Sed lacinia sapien dolor, dapibus semper arcu.

Nulla facilisi. Aenean interdum porta nisl non blandit. Nunc interdum faucibus nisi, eu iaculis libero consequat consectetur. Pellentesque tempus, neque facilisis tristique porta, dolor nisi elementum sapien, id auctor sem justo vel metus. Nulla non mi sit amet est imperdiet accumsan id ac odio. Aliquam semper lacinia lorem, eu sollicitudin libero ultricies vel. Proin a ultricies elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In pretium lorem in leo pretium non convallis magna ullamcorper. In bibendum erat a sem iaculis sit amet sollicitudin enim molestie. Vestibulum ut massa eget sem tempus varius ac in dolor. Nunc vitae nisl arcu. Fusce facilisis diam eu lectus posuere semper. Nullam metus nulla, malesuada in pharetra sit amet, dapibus sit amet enim. Etiam semper, felis quis venenatis hendrerit, mauris velit hendrerit lacus, sit amet semper arcu lacus eget mauris. Suspendisse vitae varius velit.";

    #endregion

    override protected void CreateChildren()
    {
        base.CreateChildren();

        #region Controls

        HBox hbox = new HBox
                        {
                            X = 10,
                            Y = 10,
                        };
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

        _btnAddText = new Button
                          {
                              Text = "New text",
                              FocusEnabled = false,
                              StyleMapper = "miki",
                              Icon = ImageLoader.Instance.Load("Icons/page_white_text")
                          };
        _btnAddText.Click += delegate { AddTextField(); };
        hbox.AddChild(_btnAddText);

        Button btnClear = new Button
                              {
                                  Text = "Clear",
                                  FocusEnabled = false,
                                  StyleMapper = "miki",
                                  Icon = ImageLoader.Instance.Load("Icons/cancel")
                              };
        btnClear.Click += delegate
                              {
                                  _canvas.RemoveAllChildren();
                                  _count = 1;
                              };
        hbox.AddChild(btnClear);

        #endregion

        #region Canvas

        _canvas = new Container
                      {
                          LayoutDescriptor = LayoutDescriptor.Absolute,
                          PercentWidth = 100,
                          PercentHeight = 100
                      };
        AddChild(_canvas);

        #endregion

        #region Default controls

        var button = new Button
        {
            X = 200,
            Y = 200,
            Width = 300,
            Height = 200,
            MinWidth = 200,
            MinHeight = 200,
            FocusEnabled = false,
            StyleMapper = "miki",
            Text = "Button 1",
            Tooltip = "Resizable Button"
        };
        button.Plugins.Add(new Resizable());
        _canvas.AddChild(button);

        var textField = new TextField
        {
            X = 600,
            Y = 400,
            Width = 300,
            Height = 200,
            MinWidth = 100,
            MinHeight = 100,
            ScrollContent = true,
            //StyleMapper = "miki",
            Text = LoremIpsum,
            Multiline = true,
            Tooltip = "Resizable TextField",
            Optimized = true
        };
        textField.Plugins.Add(new Resizable());
        _canvas.AddChild(textField);

        #endregion
    }

    #region Helper

    private void AddButton()
    {
        _count++;
        Button btn = new Button
                         {
                             FocusEnabled = false,
                             Width = 200 + _random.Next((int)(Stage.Dimensions.X / 2 - 200)),
                             Height = 200 + _random.Next((int)(Stage.Dimensions.Y / 2 - 200)),
                             MinWidth = 200,
                             MinHeight = 200,
                             StyleMapper = "miki",
                             Text = "Button " + _count,
                             Tooltip = "Resizable Button"
                         };
        btn.X = _random.Next((int)(_canvas.Width - btn.Width));
        btn.Y = _random.Next((int)(_canvas.Height - btn.Height));

        btn.Plugins.Add(new Resizable()); //new Draggable {Constraints = new ConstraintMetrics(0, 0, 0, null, null, 35)}
        btn.SetStyle("addedEffect", _tweenFactory);
        _canvas.AddChild(btn);
    }

    private void AddTextField()
    {
        TextField field = new TextField
                              {
                                  X = _random.Next((int)(Stage.Dimensions.X)),
                                  Y = _btnAddText.Height + 10 + _random.Next((int)(Stage.Dimensions.Y - _btnAddText.Height - 10)),
                                  Width = 100 + _random.Next(400),
                                  Height = 100 + _random.Next(400),
                                  MinWidth = 100,
                                  MinHeight = 100,
                                  ScrollContent = true,
                                  Text = LoremIpsum,
                                  Multiline = true,
                                  Tooltip = "Resizable TextField",
                                  Optimized = true
                              };

        field.SetStyle("addedEffect", _tweenFactory);
        field.Plugins.Add(new Resizable());
        _canvas.AddChild(field);
    }

    #endregion

}