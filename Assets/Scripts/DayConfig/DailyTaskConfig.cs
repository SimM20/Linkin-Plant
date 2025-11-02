using UnityEngine;

[CreateAssetMenu(fileName = "DayConfig", menuName = "Game/DailyTaskConfig", order = 1)]
public class DailyTaskConfig : ScriptableObject
{
    [Header("Day requeriments")]
    [SerializeField] private bool requiresSeed;
    [SerializeField] private bool requiresSoil;
    [SerializeField] private bool requiresWater;
    [SerializeField] private bool requiresInsecticide;

    public bool RequiresSeed => requiresSeed;
    public bool RequiresSoil => requiresSoil;
    public bool RequiresWater => requiresWater;
    public bool RequiresInsecticide => requiresInsecticide;
}
