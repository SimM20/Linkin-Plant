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
        foreach (var pot in pots)
        {
            if (pot != null)
                pot.ProcessNewDay();
        }

        if (DayManager.CurrentDay < dailyTasks.Length)
            SetNewDayTask(DayManager.CurrentDay);

        else
            UIManager.Instance?.ShowSecondForm();

        CheckPlantReadiness();
    }

    private void SetNewDayTask(int dayIndex)
    {
        if (dayIndex >= dailyTasks.Length) return;

        DailyTaskConfig newTask = dailyTasks[dayIndex];

        foreach (var pot in pots) { pot.SetNewDayTask(newTask); }
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
        Debug.LogWarning("Day completed");
        bed.SetReadyForSleep(true);
    }
}
