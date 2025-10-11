using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance;
    public List<CustomBehaviour> behaviours = new List<CustomBehaviour>();

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (behaviours.Count > 0)
        {
            for (int i = 0; i < behaviours.Count; i++)
                behaviours[i].CustomStart();
        }
    }
    private void Update()
    {
        if (behaviours.Count > 0)
        {
            for (int i = 0; i < behaviours.Count; i++)
                behaviours[i].CustomUpdate();
        }
    }
    private void FixedUpdate()
    {
        if (behaviours.Count > 0)
        {
            for (int i = 0; i < behaviours.Count; i++)
                behaviours[i].CustomFixedUpdate();
        }
    }
    public void AddToMethodsList(CustomBehaviour customMethods)
    {
        if (!behaviours.Contains(customMethods))
            behaviours.Add(customMethods);
    }
    public void RemoveFromMethodsList(CustomBehaviour customMethods)
    {
        if (behaviours.Contains(customMethods))
            behaviours.Remove(customMethods);
    }
}
