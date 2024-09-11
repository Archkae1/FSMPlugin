using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NewStatePanel : CreateablePanel
{
    private readonly string _STATE_TEMPLATE_TEXT;
    private readonly string _DICT_ITEM_TEMPLATE;

    private SelectionFSMPanel _selectionFSMPanel;

    public NewStatePanel(VisualElement utilityPanelRoot, SelectionFSMPanel selectionFSMPanel) : base(utilityPanelRoot)
    {
        _selectionFSMPanel = selectionFSMPanel;
        _STATE_TEMPLATE_TEXT = File.ReadAllText(Application.dataPath + "/Plugins/FSMPlugin/Templates/StateTemplate.txt");
        _DICT_ITEM_TEMPLATE = File.ReadAllText(Application.dataPath + "/Plugins/FSMPlugin/Templates/DictItemTemplate.txt");

        Create();
        
    }

    protected override void Create()
    {
        // Create new FSM head text element.
        TextElement headTextElement = new TextElement();
        headTextElement.name = "newStateHeadTextElement";
        headTextElement.text = "New state for current FSM";
        headTextElement.style.alignSelf = Align.Center;
        headTextElement.style.fontSize = 16f;
        headTextElement.style.marginTop = 5;
        headTextElement.style.unityFontStyleAndWeight = FontStyle.Bold;
        _thisRoot.Add(headTextElement);

        // Create text element for text field.
        TextElement fieldTextElement = new TextElement();
        fieldTextElement.name = "newStateFieldTextElement";
        fieldTextElement.text = "Enter new state name";
        _thisRoot.Add(fieldTextElement);

        // Create example text element for text field.
        TextElement expampleTextElement = new TextElement();
        expampleTextElement.name = "newStateExampleTextElement";
        expampleTextElement.text = "Example: if you write \"Load\" and FSM name is \"GameFSM\" it will create “LoadGameState”";
        expampleTextElement.style.unityFontStyleAndWeight = FontStyle.Italic;
        expampleTextElement.style.fontSize = 11f;
        _thisRoot.Add(expampleTextElement);

        // Create new FSM text field.
        TextField textField = new TextField();
        textField.name = "newStateTextField";
        textField.value = "New";
        _thisRoot.Add(textField);

        // Create new FSM button.
        Button button = new Button();
        button.name = "newStateButton";
        button.text = "Add new state";
        button.style.unityFontStyleAndWeight = FontStyle.Bold;
        button.style.fontSize = 14f;
        button.clicked += () => CreateNewState(textField.value);
        _thisRoot.Add(button);
    }

    private void CreateNewState(string value)
    {
        // Create value with State ended.
        string FSMName = _selectionFSMPanel.selectedFSMName;

        if (FSMName == null)
        {
            Debug.LogError("Choose current FSM!");
            return;
        }

        // Create directories.
        DirectoryInfo statesDirectory = new DirectoryInfo(Application.dataPath + "/Scripts/Logic/FSM/" + FSMName + "/States");
        FileInfo[] states = statesDirectory.GetFiles();

        foreach (FileInfo state in states)
        {
            if (state.Name.Contains(value))
            {
                Debug.LogError("U already have " + value + FSMName.Replace("FSM", "") + "State");
                return;
            }
        }

        // Create files.
        string stateMachineScript = Application.dataPath + "/Scripts/Logic/FSM/" + FSMName + "/" + FSMName.Replace("FSM", "") + "StateMachine.cs";
        string newStateScript = Application.dataPath + "/Scripts/Logic/FSM/" + FSMName + "/States/" + value + FSMName.Replace("FSM", "") + "State.cs";
        File.WriteAllText(newStateScript, "");

        string stateText = _STATE_TEMPLATE_TEXT.Replace("Template", value).Replace("FSM", FSMName.Replace("FSM", ""));
        string dictItem = _DICT_ITEM_TEMPLATE.Replace("Template", value).Replace("FSM", FSMName.Replace("FSM", ""));

        string findString = "_states = new Dictionary<Type, I" + FSMName.Replace("FSM", "") + "State>()\r\n        {";
        string stateMachineText = File.ReadAllText(stateMachineScript);
        int index = stateMachineText.IndexOf(findString);
        stateMachineText = stateMachineText.Insert(index + findString.Length, dictItem);
        File.AppendAllText(newStateScript, stateText);
        File.WriteAllText(stateMachineScript, "");
        File.AppendAllText(stateMachineScript, stateMachineText);

        AssetDatabase.Refresh();
    }


}
