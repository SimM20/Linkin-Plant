using System.Collections;
using TMPro;
using UnityEngine;

public class ThanksText : CustomBehaviour
{
    [SerializeField] private TextMeshProUGUI tittleText;
    [SerializeField] private float flickerSpeed = 2.0f;
    [SerializeField] private bool isActive = true;

    public override void CustomStart() => StartCoroutine(DoFlicker());

    IEnumerator DoFlicker()
    {
        while (isActive)
        {
            tittleText.color = new Color(
                tittleText.color.r,
                tittleText.color.g,
                tittleText.color.b,
                0f
            );

            yield return new WaitForSeconds(1f / flickerSpeed);

            tittleText.color = new Color(
                tittleText.color.r,
                tittleText.color.g,
                tittleText.color.b,
                1f
            );

            yield return new WaitForSeconds(1f / flickerSpeed);
        }
    }

    [ContextMenu("DoFlicker")]
    public void Flicker() => StartCoroutine(DoFlicker());
}
