using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotView : CustomBehaviour
{
    [Header("Visual references")]
    [SerializeField] private GameObject[] plantStages;
    [SerializeField] private Canvas plantCanvas;

    private PotController controller;

    public override void CustomStart()
    {
        controller = GetComponent<PotController>();

        if (plantCanvas != null)
            plantCanvas.enabled = false;

        UpdateStageVisuals(0);
    }

    public void UpdateStageVisuals(int stage)
    {
        for (int i = 0; i < plantStages.Length; i++)
        {
            if (plantStages[i] != null)
                plantStages[i].SetActive(i == stage);
        }
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
        controller.HandlePlayerInteraction(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>() == null) return;
        controller.HandlePlayerInteraction(false);
    }
}
