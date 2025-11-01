using UnityEngine;

public class Pot : CustomBehaviour
{
    [SerializeField] private SoilZone soilPlaneZone;

    private bool hasSeed = false;
    public void ReciveSoil(Vector3 hitPoint, float amount)
    {
        if (!hasSeed) return;
        if (soilPlaneZone == null) return;

        if (!soilPlaneZone.gameObject.activeSelf)
            soilPlaneZone.gameObject.SetActive(true);

        soilPlaneZone.FillSoil(amount);
    }

    public void HandleSeedPlanted() => hasSeed = true;
}
