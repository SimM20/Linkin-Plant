using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilStream : Stream
{
    protected override void HandleImpact(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out PotController pot))
            pot.ReciveSoil(hit.point, Time.deltaTime * 0.5f);
    }
}
