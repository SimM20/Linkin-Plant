public class PotModel : CustomBehaviour
{
    public bool HasSeed; //{ get; private set; }
    public bool IsWateredToday; //{ get; private set; }
    public bool HasInsecticideToday; //{ get; private set; }
    public int CurrentStage; //{ get; private set; }
    public int MaxStages; //{ get; private set; }
    public SoilZone Soil; //{ get; private set; }

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

    public void AdvanceStage()
    {
        if (CurrentStage < MaxStages - 1) CurrentStage++;
    }

    public void ResetDay() => IsWateredToday = false;

    public void SetNewTask(DailyTaskConfig task)
    {
        currentTask = task;
        IsWateredToday = false;
        HasInsecticideToday = false;
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

        return true;
    }

    public bool CanReceiveSoil() { return HasSeed; }
}
