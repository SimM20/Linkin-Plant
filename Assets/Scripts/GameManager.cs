using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<PotController> pots = new List<PotController>();

    [SerializeField] private TriggerInteractable bed;

    public override void CustomStart()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Application.targetFrameRate = 90;

        GameAnalyticsHandler.Initialize();
        GameAnalyticsHandler.StartNewGameSession();

        UIManager.Instance?.ShowFirstForm();

        CheckPlantReadiness();
    }

    public void HandleFirstFormCompleted() => Debug.Log("Se completo el primer form"); //Que vamos a hacer cuando se complete el primer form?

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

        CheckPlantReadiness();
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
        bed.SetReadyForSleep(true);
    }
}
