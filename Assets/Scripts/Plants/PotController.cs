using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : CustomBehaviour
{
    public PotModel Model { private set; get; }
    public PotView View { private set; get; }

    [Header("External references")]
    [SerializeField] private SoilZone soilZone;

    public override void CustomStart()
    {
        Model = GetComponent<PotModel>();
        View = GetComponent<PotView>();

        Model.Initialize(View.GetMaxStages(), soilZone);

        GameManager.Instance?.RegisterPlant(this);
    }

    private void OnDestroy() => GameManager.Instance?.UnregisterPlant(this);

    public void ProcessNewDay()
    {
        if (Model.IsReadyToGrow())
        {
            Model.AdvanceStage();

            View.UpdateStageVisuals(Model.CurrentStage);

            if (Model.Soil != null)
                Model.Soil.ResetMoisture();
        }
        Model.ResetDay();
    }

    public void ReciveSoil(Vector3 hitPoint, float amount)
    {
        if (Model.CanReceiveSoil())
        {
            if (Model.Soil == null) return;

            if (!Model.Soil.gameObject.activeSelf)
                Model.Soil.gameObject.SetActive(true);

            Model.Soil.FillSoil(amount);
        }
    }

    public void HandleSeedPlanted() => Model.PlantSeed();

    public void HandleWatered() => Model.SetWatered();

    public void HandlePlayerInteraction(bool isEntering) => View.ShowCanvas(isEntering);
}
