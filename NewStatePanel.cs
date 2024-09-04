using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class NewStatePanel : CreatablePanel
{
    private readonly string _StateTemplateText;

    public NewStatePanel(VisualElement utilityPanelRoot) : base(utilityPanelRoot)
    {
        _StateTemplateText = File.ReadAllText(Application.dataPath + "/Plugins/FSMModule/StateTemplate.txt");
        
        Create();
    }

    protected override void Create()
    {

    }
}
