using UnityEngine;

public class SoilBagController : CustomBehaviour
{
    [SerializeField] private GameObject pullTab;
    [SerializeField] private Transform soilSpawnPoint;

    public bool IsOpen { get; private set; } = false;

    public void OpenBag()
    {
        Debug.Log("No sabemos si esta abierta o no pero se esta intentando abrir");

        IsOpen = true;

        Debug.Log("Bag opened");

        Destroy(pullTab);
    }
}
