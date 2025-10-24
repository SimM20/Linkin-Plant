using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FixedXRGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Transform leftHandAttachment;
    [SerializeField] private Transform rightHandAttachment;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("RightHand")) { attachTransform = rightHandAttachment; }
        else if (args.interactorObject.transform.CompareTag("LeftHand")) { attachTransform = leftHandAttachment; }
        base.OnSelectEntered(args);
    }
}