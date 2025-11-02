using UnityEngine;

public class SoilBagController : CustomBehaviour
{
    [SerializeField] private GameObject pullTab;
    private PourDetector pourDetector;

    public bool IsOpen { get; private set; } = true;

    public override void CustomStart()
    {
        pourDetector = GetComponent<PourDetector>();
        pourDetector.TogglePour(); //Default = true, Toggle to false
    }

    [ContextMenu("Open bag")]
    public void OpenBag()
    {
        IsOpen = true;
        pourDetector.TogglePour();
        Destroy(pullTab);
    }
}
