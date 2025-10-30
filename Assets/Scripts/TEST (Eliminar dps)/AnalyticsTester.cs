using UnityEngine;

public class AnalyticsTester : CustomBehaviour
{
    [ContextMenu("Initialice Analytics")]
    public void InitialiceAnalytics()
    {
        GameAnalyticsHandler.Initialize();
        GameAnalyticsHandler.StartNewGameSession();
    }
}
