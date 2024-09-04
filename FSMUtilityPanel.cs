using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FSMUtilityPanel : EditorWindow
{
    private const string _Title = "FSM Utility Panel";
    private static readonly Vector2 _WinSize = new Vector2(370, 650);
    private const string _Version = "0.1";

    private CurrentFSMPanel _currentFSMPanel;
    private NewFSMPanel _newFSMPanel;

    [MenuItem("Tools/FSM/" + _Title, priority = -1)]
    public static void ShowUtilityPanel()
    {
        // Create utility window.
        FSMUtilityPanel window = GetWindow<FSMUtilityPanel>(true, _Title, true);
        window.titleContent = new GUIContent(_Title);
        window.minSize = _WinSize;
        window.maxSize = _WinSize;
    }

    public void CreateGUI()
    {
        // Create visual elements root.
        VisualElement root = rootVisualElement;

        // Set root style.
        root.style.marginLeft = 5;
        root.style.marginRight = 5;

        // Create utility panel Label.
        Label label = new Label(_Title + " ver. " + _Version);
        label.style.alignSelf = Align.Center;
        label.style.fontSize = 13f;
        label.style.marginTop = 5;
        label.style.marginBottom = 5;
        root.Add(label);

        // Create GUI panels.
        _currentFSMPanel = new CurrentFSMPanel(root, _WinSize);
        _newFSMPanel = new NewFSMPanel(root);

        // Append panels to root.
        root.Add(_currentFSMPanel.root);
        root.Add(_newFSMPanel.root);
    }
}
