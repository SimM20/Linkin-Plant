using UnityEngine;

public class DoorConfig : CustomBehaviour
{
    [SerializeField] private float zMultiplier;
    public void MoveToOpen() => transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zMultiplier);
    public void MoveToClose() => transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - zMultiplier);
}
