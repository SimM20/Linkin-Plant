using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance;
    public override void CustomStart()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 90;

        GameAnalyticsHandler.Initialize();
        GameAnalyticsHandler.StartNewGameSession();

        UIManager.Instance.ShowFirstForm();
    }

    public void HandleFirstFormCompleted() => Debug.Log("Se completo el primer form"); //Que vamos a hacer cuando se complete el primer form?
}
