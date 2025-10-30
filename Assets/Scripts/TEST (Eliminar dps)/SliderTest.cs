using UnityEngine;
using TMPro;

public class SliderTest : CustomBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText;

    public void ChangeSliderText(float value) => sliderText.text = value.ToString();
}
