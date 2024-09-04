using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NewFSMPanel : CreatablePanel
{
    private readonly string _StateMachineTemplateText;
    private readonly string _StateInterfaceTemplateText;

    public NewFSMPanel(VisualElement utilityPanelRoot) : base(utilityPanelRoot)
    {
        _StateMachineTemplateText = File.ReadAllText(Application.dataPath + "/Plugins/FSMModule/StateMachineTemplate.txt");
        _StateInterfaceTemplateText = File.ReadAllText(Application.dataPath + "/Plugins/FSMModule/StateInterfaceTemplate.txt");
        
        Create();
    }

    protected override void Create()
    {
        // Create new FSM head text element.
        TextElement headTextElement = new TextElement();
        headTextElement.name = "newFSMHeadTextElement";
        headTextElement.text = "New Final State Machines";
        headTextElement.style.alignSelf = Align.Center;
        headTextElement.style.fontSize = 16f;
        headTextElement.style.marginTop = 5;
        headTextElement.style.unityFontStyleAndWeight = FontStyle.Bold;
        _thisRoot.Add(headTextElement);

        // Create text element for text field.
        TextElement fieldTextElement = new TextElement();
        fieldTextElement.name = "newFSMFieldTextElement";
        fieldTextElement.text = "Enter new FSM name";
        _thisRoot.Add(fieldTextElement);

        // Create example text element for text field.
        TextElement expampleTextElement = new TextElement();
        expampleTextElement.name = "newFSMExampleTextElement";
        expampleTextElement.text = "Example: if you write “Game” it will create “GameFSM”";
        expampleTextElement.style.unityFontStyleAndWeight = FontStyle.Italic;
        expampleTextElement.style.fontSize = 11f;
        _thisRoot.Add(expampleTextElement);

        // Create new FSM text field.
        TextField textField = new TextField();
        textField.name = "newFSMTextField";
        textField.value = "New";
        _thisRoot.Add(textField);

        // Create new FSM button.
        Button button = new Button();
        button.name = "newFSMButton";
        button.text = "Add new FSM";
        button.style.unityFontStyleAndWeight = FontStyle.Bold;
        button.style.fontSize = 14f;
        button.clicked += () => CreateNewFSM(textField.value);
        _thisRoot.Add(button);
    }

    private void CreateNewFSM(string value)
    {
        // Create value with FSM ended.
        string valueFSM = value + "FSM";

        // Create directories.
        DirectoryInfo newFSMDirectory = new DirectoryInfo(Application.dataPath + "/Scripts/Logic/FSM/" + valueFSM);
        DirectoryInfo newFSMStatesDirectory = new DirectoryInfo(Application.dataPath + "/Scripts/Logic/FSM/" + valueFSM + "/States");
        if (newFSMDirectory.Exists)
        {
            Debug.LogError("U already have " + valueFSM);
            return;
        }
        newFSMDirectory.Create();
        newFSMStatesDirectory.Create();

        // Create files.
        string newStateMachineScript = Application.dataPath + "/Scripts/Logic/FSM/" + valueFSM + "/" + value + "StateMachine.cs";
        string newStateInterface = Application.dataPath + "/Scripts/Logic/FSM/" + valueFSM + "/I" + value + "State.cs";
        File.WriteAllText(newStateInterface, "");
        File.WriteAllText(newStateMachineScript, "");

        string stateMachineText = _StateMachineTemplateText.Replace("Template", value);
        string stateInterfaceText = _StateInterfaceTemplateText.Replace("Template", value);

        File.AppendAllText(newStateMachineScript, stateMachineText);
        File.AppendAllText(newStateInterface, stateInterfaceText);

        AssetDatabase.Refresh();
    }
}
