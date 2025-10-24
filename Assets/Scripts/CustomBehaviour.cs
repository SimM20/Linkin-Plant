using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    protected virtual void OnEnable() => GameLoop.Instance?.AddToMethodsList(this);
    protected virtual void OnDisable() => GameLoop.Instance?.RemoveFromMethodsList(this);
    public virtual void CustomStart() { }
    public virtual void CustomUpdate() { }
    public virtual void CustomFixedUpdate() { }
    public virtual void CustomSetActive(bool active) => gameObject.SetActive(active);
    public virtual bool CustomIsActive() { return gameObject.activeInHierarchy; }
    public virtual void CustomSetParent(Transform parent) => gameObject.transform.SetParent(parent);
    public virtual GameObject CustomGetGameObject() { return gameObject; }
}
