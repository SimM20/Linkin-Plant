using UnityEngine;

public class SickleTrigger : CustomBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Weed weed = other.GetComponent<Weed>();
        if (weed != null) weed.Cut();
    }
}
