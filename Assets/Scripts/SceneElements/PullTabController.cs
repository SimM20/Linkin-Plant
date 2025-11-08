using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullTabController : CustomBehaviour
{
    [SerializeField] private SoilBagController bagController;
    [SerializeField] private float pullDistance = 0.1f;

    private XRGrabInteractable grab;
    private Vector3 initialPosition;

    public override void OnEnable()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    public override void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        initialPosition = transform.position;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Si querés que se abra apenas se agarre, podés mover esto a OnGrab
    }

    public override void CustomUpdate()
    {
        Debug.Log("Todavia no se esta intentando abrir");
        if (bagController.IsOpen) return;

        if (Vector3.Distance(initialPosition, transform.position) >= pullDistance)
        {
            Debug.Log("Se esta intentando abrir");
            bagController.OpenBag();

        }
    }

}
