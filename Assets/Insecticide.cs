using UnityEngine;

public class Insecticide : CustomBehaviour
{
    [SerializeField] private ParticleSystem particuleSpray;

    public void Shoot() => particuleSpray.Play();

    public void Stop() => particuleSpray.Stop();
}
