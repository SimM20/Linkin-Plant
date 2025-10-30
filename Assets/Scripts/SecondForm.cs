public class SecondForm : BaseForm
{
    protected override void SendAnalytics(float a1, float a2, float a3, float a4, float a5) => GameAnalyticsHandler.TrackSecondQuestionary(a1, a2, a3, a4, a5);
}

