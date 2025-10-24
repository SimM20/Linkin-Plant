using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Data/BulletData", order = 0)]
public class BulletData : ScriptableObject
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeToDestroy;
    public float BulletSpeed => bulletSpeed;
    public float TimeToDestroy => timeToDestroy;
}
