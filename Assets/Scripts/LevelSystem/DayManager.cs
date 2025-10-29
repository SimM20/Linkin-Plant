using UnityEngine;

public static class DayManager
{
    private static int currentDay = 1;

    public static int CurrentDay => currentDay;

    public static void AdvanceDay()
    {
        currentDay++;
        Debug.Log($"Nuevo día: {currentDay}");
    }

    public static void ResetDays()
    {
        currentDay = 1;
        Debug.Log("Días reiniciados a 1.");
    }
}
