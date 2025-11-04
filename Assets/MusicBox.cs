using System.Collections;
using UnityEngine;

public class MusicBox : CustomBehaviour
{
    [SerializeField] private CrankHandle crank;
    [SerializeField] private float happinessRadius = 5f;
    [SerializeField] private LayerMask plantLayer;
    [SerializeField] private float happinessTickRate = 1.0f;

    private Coroutine happinessCoroutine;
    private AudioSource musicAudioSource;
    private bool isMusicPlaying = false;

    public override void CustomStart()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.loop = true;

        crank.OnCrankTurned += StartMusic;
        crank.OnCrankStopped += StopMusic;
    }

    private void OnDestroy()
    {
        crank.OnCrankTurned -= StartMusic;
        crank.OnCrankStopped -= StopMusic;
    }

    private void StartMusic()
    {
        if (isMusicPlaying) return;
        isMusicPlaying = true;

        musicAudioSource.Play();

        if (happinessCoroutine != null)
            StopCoroutine(happinessCoroutine);
        happinessCoroutine = StartCoroutine(SpreadHappiness());
    }

    private void StopMusic()
    {
        if (!isMusicPlaying) return;
        isMusicPlaying = false;

        musicAudioSource.Stop();

        if (happinessCoroutine != null)
            StopCoroutine(happinessCoroutine);
    }

    private IEnumerator SpreadHappiness()
    {
        while (isMusicPlaying)
        {
            Collider[] plantsHit = Physics.OverlapSphere(transform.position, happinessRadius, plantLayer);

            foreach (var plantCollider in plantsHit)
            {
                PotController pot = plantCollider.GetComponentInParent<PotController>();
                if (pot != null) pot.HandleMusic();
            }

            yield return new WaitForSeconds(happinessTickRate);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, happinessRadius);
    }
}
