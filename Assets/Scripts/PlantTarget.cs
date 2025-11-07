using UnityEngine;

public class PlantTarget : CustomBehaviour
{
    [SerializeField] private PotController potController;
    public PotController Pot => potController;
    public override void CustomStart()
    {
        if (potController == null)
            potController = GetComponentInParent<PotController>();
    }
}
