using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FadeEffect : CustomBehaviour
{
    [SerializeField] private Volume fadeVolume;
    [SerializeField] private float fadeDuration = 2f;

    public override void CustomStart() => fadeVolume.weight = 0f;

    public void StartDayTransition() => StartCoroutine(TransitionRoutine());

    IEnumerator TransitionRoutine()
    {
        yield return StartCoroutine(Fade(0f, 1f));
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(Fade(1f, 0f));

        Debug.Log("Transición completada.");
    }

    IEnumerator Fade(float startWeight, float endWeight)
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            fadeVolume.weight = Mathf.SmoothStep(startWeight, endWeight, t);
            yield return null;
        }

        fadeVolume.weight = endWeight;
    }
}
