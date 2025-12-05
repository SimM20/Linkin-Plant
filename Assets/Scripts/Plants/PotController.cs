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
    [SerializeField] private AudioSource audioSource;

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

    public void ProcessNewDay(DailyTaskConfig newTask)
    {
        if (model.IsReadyToGrow())
        {
            model.AdvanceStage();
            view.UpdateStageVisuals(model.CurrentStage);
        }

        if (model.Soil != null)
            model.Soil.ResetMoisture();

        SetNewDayTask(newTask);
    }

    public void SetNewDayTask(DailyTaskConfig task)
    {
        model.SetNewTask(task);

        if (view != null)
        {
            view.ShowBugs(task.RequiresInsecticide);
            view.ShowWeeds(task.RequiresPruning);
        }
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
        if (model.HasSeed) return;
        model.PlantSeed();
        GameManager.Instance?.CheckPlantReadiness();
    }

    public void HandleWatered()
    {
        if (model.IsWateredToday) return;
        model.SetWatered();
        GameManager.Instance?.CheckPlantReadiness();
        audioSource.Play();
    }
    public void HandleInsecticideApplied()
    {
        if (model.HasInsecticideToday) return;
        model.SetInsecticide();
        view.ShowBugs(false);
        GameManager.Instance?.CheckPlantReadiness();
    }

    public void HandlePruning()
    {
        if (model.HasPruningToday) return;
        model.SetPruning();
        view.ShowWeeds(false);
        GameManager.Instance?.CheckPlantReadiness();
    }

    public void HandleMusic()
    {
        if (model.HasMusicToday) return;
        model.SetMusic();
        GameManager.Instance?.CheckPlantReadiness();
    }

    public void HandlePlayerInteraction(bool isEntering) => View.ShowCanvas(isEntering);
}
