using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : CustomBehaviour
{
    [Header("References")]
    [SerializeField] private PoolSimple projectilePool;
    [SerializeField] private Transform spawnPoint;

    public void Shoot(BaseInteractionEventArgs args)
    {
        if (!projectilePool || !spawnPoint) return;

        var go = projectilePool.Get(spawnPoint.position, spawnPoint.rotation);
        var proj = go.GetComponent<Projectile>();
        if (proj != null) proj.SetPool(projectilePool);
    }
}
