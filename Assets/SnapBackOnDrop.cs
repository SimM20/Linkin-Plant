using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class SnapBackOnDrop : CustomBehaviour
{
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float returnRotationSpeed = 200f;
    [SerializeField] private bool kinematicOnReturn = true;

    private Vector3 homePosition;
    private Quaternion homeRotation;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private Coroutine returnCoroutine = null;

    public override void CustomStart()
    {
        homePosition = transform.position;
        homeRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (returnCoroutine == null)
            returnCoroutine = StartCoroutine(ReturnHomeCoroutine());
    }

    private IEnumerator ReturnHomeCoroutine()
    {
        rb.isKinematic = true;

        while (Vector3.Distance(transform.position, homePosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                homePosition,
                returnSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                homeRotation,
                returnRotationSpeed * Time.deltaTime
            );

            yield return null;
        }


        transform.position = homePosition;
        transform.rotation = homeRotation;

        rb.isKinematic = kinematicOnReturn;

        returnCoroutine = null;
    }
}
