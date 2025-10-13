using System.Collections;
using UnityEngine;

public class PourDetector : CustomBehaviour
{
    [SerializeField] private float pourThreshold = 45f;
    [SerializeField] private Transform origin;
    [SerializeField] private GameObject streamPrefab;

    private bool isPouring = false;
    private GameObject currentStream;

    public override void CustomUpdate()
    {
        bool pour = CalculatePourAngle() > pourThreshold;
        if (isPouring != pour)
        {
            isPouring = pour;
            if (isPouring) StartPouring();
            else EndPouring();
        }
    }

    private void StartPouring() 
    {
        currentStream = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        currentStream?.GetComponent<Stream>().Begin();
    }
    private void EndPouring() 
    {
        currentStream?.GetComponent<Stream>().EndPour();
        currentStream = null;
    }
    private float CalculatePourAngle() { return Vector3.Angle(transform.up, Vector3.up); }
}
