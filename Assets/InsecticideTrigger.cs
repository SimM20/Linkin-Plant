using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsecticideTrigger : CustomBehaviour
{
    private Insecticide insecticide;

    public override void CustomStart() => insecticide = GetComponentInParent<Insecticide>();

    private void OnTriggerEnter(Collider other)
    {
        PlantTarget plant = other.GetComponent<PlantTarget>();

        if (plant != null)
            insecticide.SetTargetPot(plant.Pot);
    }

    private void OnTriggerExit(Collider other)
    {
        PlantTarget plant = other.GetComponent<PlantTarget>();

        if (plant != null)
            insecticide.ClearTargetPot(plant.Pot);
    }
}
