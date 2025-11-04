using UnityEngine;

public class Insecticide : CustomBehaviour
{
    [SerializeField] private ParticleSystem particuleSpray;
    [SerializeField] private GameObject sprayTriggerObject;

    private bool isSpraying = false;
    private PotController currentPot = null;

    public override void CustomUpdate()
    {
        if (isSpraying && currentPot != null)
            ApplyInsecticide();
    }

    public void Shoot()
    {
        isSpraying = true;
        particuleSpray.Play();
        sprayTriggerObject?.SetActive(true);
    }

    public void Stop()
    {
        isSpraying = false;
        particuleSpray.Stop();
        sprayTriggerObject?.SetActive(false);
    }

    public void SetTargetPot(PotController pot)
    {
        currentPot = pot;
        if (isSpraying)
            ApplyInsecticide();
    }

    public void ClearTargetPot(PotController pot)
    {
        if (currentPot == pot)
            currentPot = null;
    }

    private void ApplyInsecticide()
    {
        if (currentPot == null) return;
        currentPot.HandleInsecticideApplied();
    }
}
