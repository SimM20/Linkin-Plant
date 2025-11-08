using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weeds : CustomBehaviour
{
    [SerializeField] private PotController potController;
    [SerializeField] private List<Weed> _weeds = new List<Weed>();

    public override void CustomStart()
    {
        if (potController == null) potController = GetComponentInParent<PotController>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        foreach (var weed in _weeds) { weed.InitializeWeed(this); }
    }

    public void OnWeedCut(Weed weed)
    {
        if (_weeds.Contains(weed)) _weeds.Remove(weed);
        CheckWeedStatus();
    }

    public void CheckWeedStatus()
    {
        if (_weeds.Count <= 0) potController.HandlePruning();
    }
}
