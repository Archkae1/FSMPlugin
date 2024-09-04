using UnityEngine;
using UnityEngine.UIElements;

public class CurrentStatePanel : CreatablePanel
{
    public CurrentStatePanel(VisualElement utilityPanelRoot) : base(utilityPanelRoot)
    {
        Create();
    }

    public void OnStateButtonClicked(string stateButtonText) 
    {
        Debug.Log(stateButtonText);
    }

    protected override void Create()
    {


        SelectionFSMPanel.stateButtonClicked += OnStateButtonClicked;
    }
}
