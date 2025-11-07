using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerInteractable : CustomBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Canvas dayCanvas;
    [SerializeField] private TextMeshProUGUI dayText;

    [Header("Configuración")]
    [SerializeField] private float displayTime = 3f; 
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private bool oneTimeUse = false;

    private bool isRunning = false;
    private bool alreadyTriggered = false;

    private bool isReadyForSleep = false;

    public override void CustomStart()
    {
        if (dayCanvas != null)
            dayCanvas.enabled = false;

        if (dayText != null)
            dayText.alpha = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRunning || (oneTimeUse && alreadyTriggered)) return;

        if (other.GetComponent<PlayerInteraction>() == null) return;

        if (isReadyForSleep) StartCoroutine(ShowDaySequence());
    }

    private IEnumerator ShowDaySequence()
    {
        isRunning = true;
        alreadyTriggered = true;

        if (dayCanvas != null)
            dayCanvas.enabled = true;

        if (dayText != null)
        {
            dayText.text = $"End of day {DayManager.CurrentDay}";
            yield return StartCoroutine(FadeText(dayText, 0, 1, fadeTime));
        }

        DayManager.AdvanceDay();

        yield return new WaitForSeconds(displayTime);

        GameManager.Instance?.AdvanceDay();

        if (dayText != null)
            yield return StartCoroutine(FadeText(dayText, 1, 0, fadeTime));

        if (dayCanvas != null)
            dayCanvas.enabled = false;

        isRunning = false;
    }

    private IEnumerator FadeText(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            text.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        text.alpha = endAlpha;
    }

    public void SetReadyForSleep(bool isReady) => isReadyForSleep = isReady;
}
