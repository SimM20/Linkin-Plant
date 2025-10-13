using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance;
    public override void CustomStart()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Application.targetFrameRate = 90;
    }
}
