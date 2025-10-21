using UnityEngine;

public class JumpingPlatform : CustomBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.Translate(new Vector3(0, 2f, 0));
    }
}
