using UnityEngine;

public class SoilZone : CustomBehaviour
{
    [Header("Hydration Settings")]
    [Range(0, 1)][SerializeField] private float moisture = 0f;
    [SerializeField] private float absorptionSpeed = 0.4f;

    [Header("References")]
    [SerializeField] private Renderer soilRenderer;
    private MaterialPropertyBlock propBlock;

    [Header("Colors")]
    [SerializeField] private Color dryColor = new Color(0.45f, 0.3f, 0.1f);   // dry
    [SerializeField] private Color wetColor = new Color(0.2f, 0.15f, 0.08f);  // wet

    public override void CustomStart()
    {
        propBlock = new MaterialPropertyBlock();
        UpdateVisual();
    }

    public void AddWater(Vector3 hitPoint, float amount)
    {
        moisture = Mathf.Clamp01(moisture + amount * absorptionSpeed);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (!soilRenderer) return;

        soilRenderer.GetPropertyBlock(propBlock);

        Color baseColor = Color.Lerp(dryColor, wetColor, moisture);

        propBlock.SetColor("_BaseColor", baseColor);
        soilRenderer.SetPropertyBlock(propBlock);
    }
}
