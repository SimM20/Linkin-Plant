using UnityEngine;
using System.Collections;

public class SnapBackOnDrop : CustomBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float returnRotationSpeed = 200f;

    [SerializeField] private Vector3 homePosition;
    [SerializeField] private Quaternion homeRotation;
    private Coroutine returnCoroutine = null;


    public override void CustomStart()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();


    }

    public void OnGrabbed()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }

    public void OnReleased() => returnCoroutine = StartCoroutine(ReturnHomeCoroutine());

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

        returnCoroutine = null;
    }
}
