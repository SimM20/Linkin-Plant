using UnityEngine;
using System;

public class SeedDetector : CustomBehaviour
{
    public event Action OnSeedPlanted;

    [ContextMenu("Add seed")]
    public void ManualAddSeed() 
    {
        GetComponentInParent<PotController>().HandleSeedPlanted();

        OnSeedPlanted?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var seed = other.GetComponent<ISeed>();
        if (seed != null)
        {
            //Podemos hacer otro checkeo para ver el tipo de semilla en caso de que sea necesario
            GetComponentInParent<PotController>().HandleSeedPlanted();

            OnSeedPlanted?.Invoke();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
