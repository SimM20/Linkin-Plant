using UnityEngine;

public class DoorConfig : CustomBehaviour
{
    [SerializeField] private Vector3 newPlace;

    private Vector3 originalPlace;

    public override void CustomStart() => originalPlace = transform.position;

    public void MoveToOpen() => transform.position = newPlace;

    public void MoveToClose() => transform.position = originalPlace;
}
