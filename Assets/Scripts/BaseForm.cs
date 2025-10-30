using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseForm : CustomBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> questions;
    [SerializeField] private List<Slider> sliderAnswers;

    public void OnSubmitForm() 
    {
        List<float> answers = new List<float>();
        foreach (Slider slider in sliderAnswers) { answers.Add(slider.value); }

        if (answers.Count == 5)
            SendAnalytics(answers[0], answers[1], answers[2], answers[3], answers[4]);

        else return;
    }

    protected virtual void SendAnalytics(float a1, float a2, float a3, float a4, float a5) { }
}
