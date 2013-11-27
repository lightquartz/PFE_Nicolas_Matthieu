using System;
using System.Reflection;
using eDriven.Gui.Components;
using eDriven.Gui.Designer;
using eDriven.Playground.Demo.Components;

[Obfuscation(Exclude = true)]
[Toolbox(Label = "AnimatedLabel", Group = "My controls", Icon = "eDriven/Editor/Controls/label")]
public class AnimatedLabelAdapter : LabelAdapter
{
    public override Type ComponentType
    {
        get { return typeof (HesitantLabel); }
    }

    public override Component NewInstance()
    {
        return new HesitantLabel();
    }
}