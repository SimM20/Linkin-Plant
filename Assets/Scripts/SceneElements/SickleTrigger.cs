using UnityEngine;

public class SickleTrigger : CustomBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        Weed weed = other.GetComponent<Weed>();
        if (weed != null)
        {
            weed.Cut();
            audioSource?.Play();
        }
    }
}
