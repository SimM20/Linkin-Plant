using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : CustomBehaviour
{
    [Header("Spawn")]
    [SerializeField] PoolSimple projectilePool;
    [SerializeField] Transform spawnPoint;
    [SerializeField, Tooltip("Disparos por segundo")] float fireRate = 6f;

    float cooldown;

    public override void CustomUpdate()
    {
        if (cooldown > 0f) cooldown -= Time.deltaTime;

        if (cooldown <= 0f) Fire();
    }

    public void TryFireRequested()
    {
        if (cooldown <= 0f) Fire();
    }

    void Fire()
    {
        if (!projectilePool || !spawnPoint) return;

        var go = projectilePool.Get(spawnPoint.position, spawnPoint.rotation);
        var proj = go.GetComponent<Projectile>();
        if (proj != null) proj.SetPool(projectilePool);

        cooldown = 1f / Mathf.Max(0.0001f, fireRate);
    }
}
