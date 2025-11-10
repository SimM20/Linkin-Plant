using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using System;

public class GameAnalyticsHandler
{
    private static bool initialized = false;
    private static string currentGameSessionID;

    public static async void Initialize()
    {
        if (initialized) return;

        try
        {
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
            initialized = true;
            Debug.Log("[Analytics] Unity Analytics initialized.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[Analytics] Failed to initialize: {e.Message}");
        }
    }

    public static void StartNewGameSession()
    {
        currentGameSessionID = Guid.NewGuid().ToString();
        Debug.Log($"[Analytics] NEW Game Session ID generated: {currentGameSessionID}");
    }

    public static void TrackFirstQuestionary(float answ1, float answ2, float answ3, float answ4, float answ5)
    {
        if (!EnsureSessionIsReady()) return;

        AnalyticsService.Instance.RecordEvent(new CustomEvent("FirstQuestionary")
        {
            { "gameSessionID", currentGameSessionID },
            { "f1_question_1", answ1 },
            { "f1_question_2", answ2 },
            { "f1_question_3", answ3 },
            { "f1_question_4", answ4 },
            { "f1_question_5", answ5 }
        });
    }

    public static void TrackSecondQuestionary(float answ1, float answ2, float answ3, float answ4, float answ5)
    {
        if (!EnsureSessionIsReady()) return;

        AnalyticsService.Instance.RecordEvent(new CustomEvent("SecondQuestionary")
        {
            { "gameSessionID", currentGameSessionID },
            { "f2_question_1", answ1 },
            { "f2_question_2", answ2 },
            { "f2_question_3", answ3 },
            { "f2_question_4", answ4 },
            { "f2_question_5", answ5 }
        });
        currentGameSessionID = null;
    }

    private static bool EnsureSessionIsReady()
    {
        if (!initialized)
        {
            Debug.LogError("[Analytics] Analytics Service is not initialized yet. Call Initialize() first.");
            return false;
        }

        if (string.IsNullOrEmpty(currentGameSessionID))
        {
            Debug.LogError("[Analytics] ERROR: Trying to track an event, but 'currentGameSessionID' is missing. " +
                           "You MUST call 'GameAnalyticsHandler.StartNewGameSession()' *before* tracking any events.");
            return false;
        }
        return true;
    }
}
