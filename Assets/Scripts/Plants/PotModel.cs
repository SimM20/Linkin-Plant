public class PotModel : CustomBehaviour
{
    public bool HasSeed { get; private set; } = false;
    public bool IsWateredToday { get; private set; } = false;
    public bool HasInsecticideToday { get; private set; } = false;
    public bool HasPruningToday { get; private set; } = false;
    public bool HasMusicToday { get; private set; } = false;
    public int CurrentStage { get; private set; }
    public int MaxStages { get; private set; }
    public SoilZone Soil { get; private set; }

    public DailyTaskConfig currentTask;

    public void Initialize(int maxStages, SoilZone soilZone)
    {
        MaxStages = maxStages;
        Soil = soilZone;
        HasSeed = false;
        CurrentStage = 0;
    }

    public void PlantSeed() => HasSeed = true;

    public void SetWatered() => IsWateredToday = true;

    public void SetInsecticide() => HasInsecticideToday = true;

    public void SetPruning() => HasPruningToday = true;

    public void SetMusic() => HasMusicToday = true;

    public void AdvanceStage()
    {
        if (CurrentStage < MaxStages - 1) CurrentStage++;
    }

    public void SetNewTask(DailyTaskConfig task)
    {
        currentTask = task;
        IsWateredToday = false;
        HasInsecticideToday = false;
        HasPruningToday = false;
        HasMusicToday = false;
    }

    public bool IsReadyToGrow()
    {
        if (currentTask == null) return false;

        if (currentTask.RequiresSeed && !HasSeed)
            return false;

        if (currentTask.RequiresWater && !IsWateredToday)
            return false;

        if (currentTask.RequiresInsecticide && !HasInsecticideToday)
            return false;

        if (currentTask.RequiresMusic && !HasMusicToday)
            return false;

        if (currentTask.RequiresPruning && !HasPruningToday)
            return false;

        return true;
    }

    public bool CanReceiveSoil() { return HasSeed; }
}
