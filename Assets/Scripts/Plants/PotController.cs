using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : CustomBehaviour
{
    [SerializeField] private PotModel model;
    [SerializeField] private PotView view;

    public PotModel Model => model;
    public PotView View => view;

    [Header("External references")]
    [SerializeField] private SoilZone soilZone;
    [SerializeField] private SeedDetector seedDetector;

    public override void CustomStart()
    {
        model.Initialize(view.GetMaxStages(), soilZone);

        soilZone.OnWatered += HandleWatered;

        seedDetector.OnSeedPlanted += HandleSeedPlanted;

        GameManager.Instance?.CheckPlantReadiness();
    }

    private void OnDestroy()
    {
        soilZone.OnWatered -= HandleWatered;
        seedDetector.OnSeedPlanted -= HandleSeedPlanted;
    }

    public void ProcessNewDay()
    {
        model.AdvanceStage();
        view.UpdateStageVisuals(model.CurrentStage);
        if (model.Soil != null)
            model.Soil.ResetMoisture();
    }

    public void SetNewDayTask(DailyTaskConfig task)
    {
        model.SetNewTask(task);
        Debug.Log("Setteo de tareas listo");
    }

    public void ReciveSoil(Vector3 hitPoint, float amount)
    {
        if (model.CanReceiveSoil())
        {
            if (model.Soil == null) return;

            if (!model.Soil.gameObject.activeSelf)
                model.Soil.gameObject.SetActive(true);

            model.Soil.FillSoil(amount);
        }
    }

    public void HandleSeedPlanted()
    {
        model.PlantSeed();
        GameManager.Instance?.CheckPlantReadiness();
    }

    public void HandleWatered()
    {
        model.SetWatered();
        GameManager.Instance?.CheckPlantReadiness();
    }
    public void HandleInsecticideApplied()
    {
        model.SetInsecticide();
        GameManager.Instance?.CheckPlantReadiness();
    }

    public void HandlePlayerInteraction(bool isEntering) => View.ShowCanvas(isEntering);
}
