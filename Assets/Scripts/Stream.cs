using System.Collections;
using UnityEngine;

public class Stream : CustomBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3 currentEndPoint;
    private Vector3 velocity;
    private Vector3 lastPos;

    public override void CustomStart()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public override void CustomUpdate()
    {
        velocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }

    public void Begin() => StartCoroutine(BeginPour());

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            Vector3 direction = (Vector3.down + velocity * 0.05f).normalized;

            RaycastHit hit;
            Vector3 endPoint;
            if (Physics.Raycast(transform.position, direction, out hit, 2f))
            {
                endPoint = hit.point;
                HandleImpact(hit);
            }
            else endPoint = transform.position + direction * 2f;

            currentEndPoint = Vector3.Lerp(currentEndPoint, endPoint, Time.deltaTime * 15f);
            MoveToPosition(0, transform.position);
            MoveToPosition(1, currentEndPoint);

            yield return null;
        }
    }

    protected virtual void HandleImpact(RaycastHit hit) { }

    public void EndPour() => StartCoroutine(EndPouring());

    private IEnumerator EndPouring()
    {
        Vector3 start = currentEndPoint;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            MoveToPosition(1, Vector3.Lerp(start, transform.position, t));
            yield return null;
        }

        Destroy(gameObject);
    }

    private void MoveToPosition(int index, Vector3 targetPosition) => lineRenderer?.SetPosition(index, targetPosition);
}
