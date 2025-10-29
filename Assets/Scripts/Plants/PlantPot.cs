using UnityEngine;

public class PlantPot : MonoBehaviour
{
    [Header("Etapas de la planta")]
    [SerializeField] private GameObject[] plantStages; // 0 = brote, 1 = media, 2 = madura
    [SerializeField] private Canvas plantCanvas;

    private int currentStage = 0;

    private void Start()
    {
        if (PlantManager.Instance != null)
            PlantManager.Instance.RegisterPlant(this);

        UpdateStage();

        if (plantCanvas != null)
            plantCanvas.enabled = false;
    }

    private void OnDestroy()
    {
        if (PlantManager.Instance != null)
            PlantManager.Instance.UnregisterPlant(this);
    }

    private void UpdateStage()
    {
        for (int i = 0; i < plantStages.Length; i++)
            if (plantStages[i] != null)
                plantStages[i].SetActive(i == currentStage);
    }

    public void NextStage()
    {
        currentStage++;
        if (currentStage >= plantStages.Length)
            currentStage = 0;

        UpdateStage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>() == null) return;
        if (plantCanvas != null)
            plantCanvas.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>() == null) return;
        if (plantCanvas != null)
            plantCanvas.enabled = false;
    }
}
