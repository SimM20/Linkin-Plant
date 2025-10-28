using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStream : Stream
{
    protected override void HandleImpact(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out SoilZone soil))
            soil.AddWater(hit.point, Time.deltaTime * 0.5f);
    }
}
