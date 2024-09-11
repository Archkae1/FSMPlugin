using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionFSMPanel : CreateablePanel
{
    private string _selectedFSMName;
    private readonly Vector2 _WinSize;
    private DirectoryInfo[] _FSMDirectories;

    public string selectedFSMName { get { return _selectedFSMName; } }

    public SelectionFSMPanel(VisualElement utilityPanelRoot, Vector2 WinSize) : base(utilityPanelRoot)
    {
        _WinSize = WinSize;
        DirectoryInfo FSMMainDirectory = new DirectoryInfo(Application.dataPath + "/Scripts/Logic/FSM");
        if (!FSMMainDirectory.Exists)
        {
            FSMMainDirectory.Create();
            AssetDatabase.Refresh();
        }
        _FSMDirectories = FSMMainDirectory.GetDirectories("*FSM");
        
        Create();
    }

    protected override void Create()
    {
        // Create current FSM toolbar and toolbar menu.
        Toolbar toolbar = new Toolbar();
        toolbar.name = "currentFSMToolbar";
        toolbar.style.marginTop = 5;
        toolbar.style.backgroundColor = new Color(0.149f, 0.149f, 0.149f);
        toolbar.style.height = 30f;
        toolbar.style.width = _WinSize.x;
        _thisRoot.Add(toolbar);

        ToolbarMenu toolbarMenu = new ToolbarMenu();
        toolbarMenu.name = "currentFSMToolbarMenu";
        toolbarMenu.text = "Choose available FSM";
        toolbarMenu.style.width = _WinSize.x;
        toolbarMenu.style.marginBottom = 5;
        toolbarMenu.style.marginTop = 5;
        toolbarMenu.style.marginLeft = 5;
        toolbarMenu.style.marginRight = 15;
        toolbarMenu.style.fontSize = 14f;
        toolbarMenu.style.unityFontStyleAndWeight = FontStyle.Bold;
        toolbarMenu.style.color = Color.white;
        toolbar.Add(toolbarMenu);

        ListView listView = new ListView();
        listView.name = "currentFSMListView";
        listView.style.marginTop = 0f;
        listView.style.backgroundColor = new Color(0.102f, 0.102f, 0.102f);
        listView.style.minHeight = 250f;
        _thisRoot.Add(listView);

        // Append current FSM variants.
        DefineCurrentFSM(toolbarMenu, listView);
    }

    private void SelectCurrentFSM(string selectedName, ToolbarMenu toolbarMenu, ListView listView)
    {
        listView.hierarchy.Clear();
        toolbarMenu.text = selectedName;
        _selectedFSMName = selectedName;
        DirectoryInfo currentFSMDirectory = null;

        foreach (DirectoryInfo FSMDirectory in _FSMDirectories)
        {
            if (FSMDirectory.Name == selectedName)
            {
                currentFSMDirectory = FSMDirectory;
                break;
            }
        }

        FileInfo[] stateFiles = null;
        DirectoryInfo[] directories = currentFSMDirectory.GetDirectories();
        foreach (DirectoryInfo directory in directories)
        {
            if (directory.Name == "States")
            {
                stateFiles = directory.GetFiles();
                break;
            }
        }


        foreach (FileInfo stateFile in stateFiles)
        {
            if (stateFile.Name.Contains("meta")) continue;

            Button stateButton = new Button();
            stateButton.text = stateFile.Name;
            stateButton.style.fontSize = 13.5f;

            listView.hierarchy.Add(stateButton);
        }
    }

    private void DefineCurrentFSM(ToolbarMenu toolbarMenu, ListView listView)
    {
        foreach (DirectoryInfo FSMDirectory in _FSMDirectories)
            toolbarMenu.menu.AppendAction(FSMDirectory.Name, (DropdownMenuAction dropdownMenuAction) => SelectCurrentFSM(dropdownMenuAction.name, toolbarMenu, listView));
    }
}
