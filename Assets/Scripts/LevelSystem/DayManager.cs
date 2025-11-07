using UnityEngine;

public static class DayManager
{
    private static int currentDay = 0;

    public static int CurrentDay => currentDay;

    public static void AdvanceDay() => currentDay++;

    public static void ResetDays() => currentDay = 1;
}
