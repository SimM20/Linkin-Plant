using UnityEngine;

public class PlantTarget : CustomBehaviour
{
    private PotController potController;
    public PotController Pot => potController;
    public override void CustomStart()
    {
        potController = GetComponentInParent<PotController>();
        if (potController == null)
            Debug.LogError("¡Este 'blanco' de planta no sabe a qué maceta pertenece!", gameObject);
    }
}
