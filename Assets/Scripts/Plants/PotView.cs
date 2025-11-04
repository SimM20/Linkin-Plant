using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotView : CustomBehaviour
{
    [Header("Visual references")]
    [SerializeField] private GameObject[] plantStages;
    [SerializeField] private Canvas plantCanvas;

    [Header("Task Visuals")]
    [SerializeField] private ParticleSystem bugParticles;
    [SerializeField] private GameObject weedsToPrune;

    private PotController controller;

    public override void CustomStart()
    {
        controller = GetComponent<PotController>();

        if (plantCanvas != null)
            plantCanvas.enabled = false;

        UpdateStageVisuals(0);

        ShowBugs(false);
        ShowWeeds(false);
    }

    public void UpdateStageVisuals(int stage)
    {
        for (int i = 0; i < plantStages.Length; i++)
        {
            if (plantStages[i] != null)
                plantStages[i].SetActive(i == stage);
        }
    }

    public void ShowBugs(bool show)
    {
        if (bugParticles == null) return;
        if (show && !bugParticles.isPlaying)
            bugParticles.Play();
        else if (!show && bugParticles.isPlaying)
            bugParticles.Stop();
    }

    public void ShowWeeds(bool show)
    {
        if (weedsToPrune != null)
            weedsToPrune.SetActive(show);
    }

    public void ShowCanvas(bool show)
    {
        if (plantCanvas != null)
            plantCanvas.enabled = show;
    }

    public int GetMaxStages() { return plantStages.Length; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>() == null) return;
        controller?.HandlePlayerInteraction(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>() == null) return;
        controller?.HandlePlayerInteraction(false);
    }
}
