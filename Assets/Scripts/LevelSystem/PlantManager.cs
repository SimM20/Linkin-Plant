using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }

    private List<PlantPot> plants = new List<PlantPot>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterPlant(PlantPot plant)
    {
        if (!plants.Contains(plant))
            plants.Add(plant);
    }

    public void UnregisterPlant(PlantPot plant)
    {
        if (plants.Contains(plant))
            plants.Remove(plant);
    }

    public void AdvanceAllPlants()
    {
        foreach (var plant in plants)
        {
            if (plant != null)
                plant.NextStage();
        }
    }
}
