using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : CustomBehaviour
{
    [SerializeField] private SoilZone soilPlaneZone;
    public void ReciveSoil(Vector3 hitPoint, float amount)
    {
        if (soilPlaneZone == null) return;

        if (!soilPlaneZone.gameObject.activeSelf)
            soilPlaneZone.gameObject.SetActive(true);

        soilPlaneZone.FillSoil(amount);
    }
}
