using UnityEngine;

public class SeedDetector : CustomBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var seed = other.GetComponent<ISeed>();
        if (seed != null)
        {
            //Podemos hacer otro checkeo para ver el tipo de semilla en caso de que sea necesario
            GetComponentInParent<PotController>().HandleSeedPlanted();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
