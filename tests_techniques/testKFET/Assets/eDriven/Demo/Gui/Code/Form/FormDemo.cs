using System.Collections;
using System.Text;
using eDriven.Core.Caching;
using eDriven.Gui.Components;
using eDriven.Gui.Dialogs;
using eDriven.Gui.Dialogs.Alert;
using eDriven.Gui.Form;
using eDriven.Gui.Plugins;

public class FormDemo : eDriven.Gui.Gui
{
    protected override void OnStart()
    {
        base.OnStart();

        LayoutDescriptor = eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleCenter;
    }

    #region Dummy text

    private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Aliquam non urna purus. Suspendisse tincidunt scelerisque euismod. 
Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. 
Praesent ipsum elit, consectetur ac scelerisque vitae, rhoncus porta nulla. 
In semper placerat sem nec consectetur. 
Donec mi arcu, tristique at viverra eget, accumsan at erat. 
Nulla ut ligula nibh, sit amet consequat neque. 
Aliquam a turpis sem, at dictum leo.";

    #endregion

    override protected void CreateChildren()
    {
        base.CreateChildren();

        Dialog dialog = new Dialog {Width = 350, Padding = 10, Title = "Form Demo"};
        AddChild(dialog);

        Form form = new Form {PercentWidth = 100, Padding = 0};
        dialog.AddContentChild(form);

        #region Text Fields

        TextField txtSubject = new TextField
                                   {
                                       FocusEnabled = true,
                                       PercentWidth = 100,
                                       Text = "Input text",
                                       Optimized = true
                                       //AlowedCharacters = "a1"
                          };
        form.AddField("subject", "Subject:", txtSubject, eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleLeft);

        TextField txtMessage = new TextField
                       {
                           FocusEnabled = true,
                           PercentWidth = 100,
                           Height = 200,
                           Multiline = true,
                           ScrollContent = true,
                           Text = LoremIpsum,
                           Optimized = true
                           //StyleMapper = "miki",
                           //Styles = buttonStyles
                       };
        form.AddField("message", "Message:", txtMessage, eDriven.Gui.Layout.LayoutDescriptor.VerticalMiddleLeft);

        #endregion

        #region Buttons

        dialog.ButtonGroup.AddChild(new Spacer {PercentWidth = 100});

        Button btnSet = new Button
                       {
                           Text = "Set data",
                           StyleMapper = "miki",
                           Icon = ImageLoader.Instance.Load("Icons/arrow_up")
                       };
        btnSet.Press += delegate
        {
            form.Data = new Hashtable
                            {
                                {"subject", "The subject"},
                                {"message", "This is the message..."}
                            };
        };
        dialog.ButtonGroup.AddChild(btnSet);
        btnSet.SetFocus();

        Button btnGet = new Button
        {
            Text = "Get data",
            StyleMapper = "miki",
            Icon = ImageLoader.Instance.Load("Icons/arrow_down")
        };
        btnGet.Press += delegate
        {
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry entry in form.Data)
            {
                sb.AppendLine(string.Format(@"""{0}"": {1}", entry.Key, entry.Value));
                sb.AppendLine();
            }

            Alert.Show("This is the form data", sb.ToString(), AlertButtonFlag.Ok);
        };
        dialog.ButtonGroup.AddChild(btnGet);

        #endregion

        dialog.Plugins.Add(new TabManager());
    }
}