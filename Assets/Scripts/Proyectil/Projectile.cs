using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : CustomBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 60f;
    [SerializeField] private float maxLife = 4f;

    [Header("Collision")]
    [SerializeField] private LayerMask hitMask;

    float life;
    Vector3 prevPos;
    PoolSimple myPool;

    protected override void OnEnable()
    {
        base.OnEnable();
        life = 0f;
        prevPos = transform.position;
    }

    public override void CustomUpdate()
    {
        float dt = Time.deltaTime;
        Vector3 newPos = transform.position + transform.forward * speed * dt;

        Vector3 delta = newPos - prevPos;
        float dist = delta.magnitude;
        if (dist > 0f && Physics.Raycast(prevPos, delta.normalized, out RaycastHit hit, dist, hitMask, QueryTriggerInteraction.Ignore))
        {
            ReturnToPool();
            return;
        }

        transform.position = newPos;
        prevPos = transform.position;

        life += dt;
        if (life >= maxLife)
            ReturnToPool();
    }

    public void SetPool(PoolSimple pool) => myPool = pool;

    void ReturnToPool()
    {
        if (myPool != null) myPool.Release(gameObject);
        else gameObject.SetActive(false);
    }
}


