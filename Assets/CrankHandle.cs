using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CrankHandle : CustomBehaviour
{
    public event Action OnCrankTurned;
    public event Action OnCrankStopped;

    [Header("Config")]
    [SerializeField] private float turnThreshold = 0.5f;

    private Rigidbody rb;
    private bool isTurning = false;
    private XRGrabInteractable grabInteractable;

    public override void CustomStart()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered?.AddListener(OnGrab);
        grabInteractable.selectExited?.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered?.RemoveListener(OnGrab);
        grabInteractable.selectExited?.RemoveListener(OnRelease);
    }


    public override void CustomFixedUpdate()
    {
        if (!grabInteractable.isSelected)
        {
            if (isTurning)
            {
                isTurning = false;
                OnCrankStopped?.Invoke();
            }
            return;
        }

        float turnSpeed = rb.angularVelocity.magnitude;

        if (turnSpeed > turnThreshold && !isTurning)
        {
            isTurning = true;
            OnCrankTurned?.Invoke();
        }
        else if (turnSpeed <= turnThreshold && isTurning)
        {
            isTurning = false;
            OnCrankStopped?.Invoke();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Rigidbody parentRb = GetComponentInParent<HingeJoint>().connectedBody;
        if (parentRb != null)
            Physics.IgnoreCollision(GetComponent<Collider>(), parentRb.GetComponent<Collider>(), true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Rigidbody parentRb = GetComponentInParent<HingeJoint>().connectedBody;
        if (parentRb != null)
            Physics.IgnoreCollision(GetComponent<Collider>(), parentRb.GetComponent<Collider>(), false);
    }
}
