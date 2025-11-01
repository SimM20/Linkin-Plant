using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomBehaviour
{
    public static UIManager Instance;

    [Header("Prefab References")]
    [SerializeField] private GameObject firstForm;
    [SerializeField] private GameObject secondForm;

    [Header("UI References")]
    [SerializeField] private GameObject mainPanel;

    private GameObject firstFormInstance;
    private GameObject secondFormInstance;

    public override void CustomStart()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowFirstForm() 
    {
        if (firstFormInstance == null)
            firstFormInstance = Instantiate(firstForm, mainPanel.transform);

        else firstFormInstance.SetActive(true);
    }

    public void HideFirstForm()
    {
        if (firstFormInstance != null) 
            firstFormInstance.SetActive(false);
    }

    public void ShowSecondForm()
    {
        if (secondFormInstance == null)
            secondFormInstance = Instantiate(secondForm, mainPanel.transform);

        else secondFormInstance.SetActive(true);
    }

    public void HideSecondForm()
    {
        if (secondFormInstance != null)
            secondFormInstance.SetActive(false);
    }
}
