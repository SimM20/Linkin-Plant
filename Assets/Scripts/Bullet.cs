using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        //IDestructible destructible = other.gameObject.GetComponent<IDestructible>();
        //if (destructible != null) destructible.Destroy();
    }
}
