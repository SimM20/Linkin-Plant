public class PotModel : CustomBehaviour
{
    public bool HasSeed { get; private set; }
    public bool IsWateredToday { get; private set; }
    public int CurrentStage { get; private set; }
    public int MaxStages { get; private set; }
    public SoilZone Soil { get; private set; }

    public void Initialize(int maxStages, SoilZone soilZone)
    {
        MaxStages = maxStages;
        Soil = soilZone;

        HasSeed = false;
        IsWateredToday = false;
        CurrentStage = 0;
    }

    public void PlantSeed() => HasSeed = true;

    public void SetWatered() => IsWateredToday = true;

    public void AdvanceStage()
    {
        if (CurrentStage < MaxStages - 1) CurrentStage++;
    }

    public void ResetDay() => IsWateredToday = false;

    public bool IsReadyToGrow() { return HasSeed && IsWateredToday; }

    public bool CanReceiveSoil() { return HasSeed; }
}
