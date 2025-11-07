using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject[] doors;

    [SerializeField] private List<PotController> pots = new List<PotController>();

    [SerializeField] private TriggerInteractable bed;
    [SerializeField] private DailyTaskConfig[] dailyTasks;

    public override void CustomStart()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Application.targetFrameRate = 90;

        GameAnalyticsHandler.Initialize();
        GameAnalyticsHandler.StartNewGameSession();

        UIManager.Instance?.ShowFirstForm();

        SetNewDayTask(DayManager.CurrentDay);

        CheckPlantReadiness();
    }

    public void Initialize() 
    {
        foreach (GameObject door in doors) { Destroy(door); }
    }

    public void RegisterPlant(PotController pot)
    {
        if (!pots.Contains(pot))
            pots.Add(pot);
    }

    public void UnregisterPlant(PotController pot)
    {
        if (pots.Contains(pot))
            pots.Remove(pot);
    }

    public void AdvanceDay()
    {
        int newDayIndex = DayManager.CurrentDay;

        Debug.LogWarning($"Current day: {newDayIndex}");

        if (newDayIndex >= dailyTasks.Length)
        {
            UIManager.Instance?.ShowSecondForm();
            CheckPlantReadiness();
            return;
        }

        DailyTaskConfig newTask = dailyTasks[newDayIndex];

        foreach (var pot in pots)
        {
            if (pot != null)
                pot.ProcessNewDay(newTask);
        }

        CheckPlantReadiness();
    }

    private void SetNewDayTask(int dayIndex)
    {
        if (dayIndex >= dailyTasks.Length) return;

        DailyTaskConfig newTask = dailyTasks[dayIndex];
        foreach (var pot in pots)
        {
            pot.SetNewDayTask(newTask);
            Debug.LogWarning($"New task name: {newTask.name}.\n Tasks: {newTask.RequiresSoil}" +
                $" - {newTask.RequiresPruning} - {newTask.RequiresMusic} - {newTask.RequiresInsecticide}" +
                $" - {newTask.RequiresWater}");
        }
    }

    public void CheckPlantReadiness()
    {
        if (bed == null) return;

        if (pots.Count <= 0)
        {
            bed.SetReadyForSleep(false);
            return;
        }

        foreach (var pot in pots)
        {
            if (pot == null || !pot.Model.IsReadyToGrow())
            {
                bed.SetReadyForSleep(false);
                return;
            }
        }
        Debug.LogWarning($"Day {DayManager.CurrentDay} completed");
        bed.SetReadyForSleep(true);
    }
}
