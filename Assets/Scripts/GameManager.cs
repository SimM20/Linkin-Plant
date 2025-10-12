using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance;
    private void OnEnable() => GameLoop.Instance?.AddToMethodsList(this);

    public override void CustomStart()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Application.targetFrameRate = 90;
    }
}
