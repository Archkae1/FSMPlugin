using UnityEngine;
using UnityEngine.UIElements;

public class CurrentFSMPanel : CreatablePanel
{
    private SelectionFSMPanel _selectionFSMPanel;
    private CurrentStatePanel _currentStatePanel;
    private NewStatePanel _newStatePanel;

    public CurrentFSMPanel(VisualElement utilityPanelRoot, Vector2 WinSize) : base(utilityPanelRoot)
    {
        Create();

        _selectionFSMPanel = new SelectionFSMPanel(utilityPanelRoot, WinSize);
        _currentStatePanel = new CurrentStatePanel(utilityPanelRoot);
        _newStatePanel = new NewStatePanel(utilityPanelRoot);

        _thisRoot.Add(_selectionFSMPanel.root);
        _thisRoot.Add(_currentStatePanel.root);
        _thisRoot.Add(_newStatePanel.root);
    }

    protected override void Create()
    {
        // Create current FSM head text element.
        TextElement headTextElement = new TextElement();
        headTextElement.name = "currentFSMHeadTextElement";
        headTextElement.text = "Current Final State Machines";
        headTextElement.style.alignSelf = Align.Center;
        headTextElement.style.fontSize = 16f;
        headTextElement.style.unityFontStyleAndWeight = FontStyle.Bold;
        _thisRoot.Add(headTextElement);
    }
}
