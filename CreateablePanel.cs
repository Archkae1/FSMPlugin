using UnityEngine.UIElements;

public abstract class CreateablePanel
{
    protected VisualElement _thisRoot, _utilityPanelRoot;

    public VisualElement root { get { return _thisRoot; } }

    public CreateablePanel(VisualElement utilityPanelRoot)
    {
        _thisRoot = new VisualElement();
        _utilityPanelRoot = utilityPanelRoot;
    }

    protected abstract void Create();
}
