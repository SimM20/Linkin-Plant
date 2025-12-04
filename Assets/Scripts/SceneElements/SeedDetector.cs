using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class SeedDetector : CustomBehaviour
{
    public event Action OnSeedPlanted;

    [SerializeField] private Transform plantPoint;

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
            var pot = GetComponentInParent<PotController>();
            pot.HandleSeedPlanted();

            OnSeedPlanted?.Invoke();

            Transform target = plantPoint != null ? plantPoint : transform;

            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                grab.enabled = false;
            }

            other.transform.position = target.position;
            other.transform.rotation = target.rotation;
            other.transform.SetParent(pot.transform);

            Destroy(gameObject);
        }
    }
}
