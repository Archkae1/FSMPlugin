using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FSMUtilityPanel : EditorWindow
{
    private const string _TITLE = "FSM Utility Panel";
    private const string _VERSION = "0.0.2";
    private static readonly Vector2 _WIN_SIZE = new Vector2(370, 650);

    private CurrentFSMPanel _currentFSMPanel;
    private NewFSMPanel _newFSMPanel;

    [MenuItem("Tools/FSM/" + _TITLE, priority = -1)]
    public static void ShowUtilityPanel()
    {
        // Create utility window.
        FSMUtilityPanel window = GetWindow<FSMUtilityPanel>(true, _TITLE, true);
        window.titleContent = new GUIContent(_TITLE);
        window.minSize = _WIN_SIZE;
        window.maxSize = _WIN_SIZE;
    }

    public void CreateGUI()
    {
        // Create visual elements root.
        VisualElement root = rootVisualElement;

        // Set root style.
        root.style.marginLeft = 5;
        root.style.marginRight = 5;

        // Create utility panel Label.
        Label label = new Label(_TITLE + " ver. " + _VERSION);
        label.style.alignSelf = Align.Center;
        label.style.fontSize = 13f;
        label.style.marginTop = 5;
        label.style.marginBottom = 5;
        root.Add(label);

        // Create GUI panels.
        _currentFSMPanel = new CurrentFSMPanel(root, _WIN_SIZE);
        _newFSMPanel = new NewFSMPanel(root);

        // Append panels to root.
        root.Add(_currentFSMPanel.root);
        root.Add(_newFSMPanel.root);
    }
}
