using UnityEngine;

public class SoilZone : CustomBehaviour
{
    [Header("Fill Settings")]
    [SerializeField] private float maxFillAmount = 10f;
    [SerializeField] private float soilBottomY = -0.5f;
    [SerializeField] private float soilTopY = 0f;

    [Header("Hydration Settings")]
    [Range(0, 1)][SerializeField] private float moisture = 0f;
    [SerializeField] private float absorptionSpeed = 0.4f;

    [Header("References")]
    [SerializeField] private Renderer soilRenderer;

    [Header("Colors")]
    [SerializeField] private Color dryColor = new Color(0.45f, 0.3f, 0.1f);   // dry
    [SerializeField] private Color wetColor = new Color(0.2f, 0.15f, 0.08f);  // wet

    private MaterialPropertyBlock propBlock;
    private Transform soilPlaneTransform;
    private float currentFillAmount = 0f;
    private bool isFilled = false;

    public float CurrentFill => currentFillAmount;
    public float CurrentMoisture => moisture;

    public override void CustomStart()
    {
        propBlock = new MaterialPropertyBlock();

        soilPlaneTransform = transform;

        soilPlaneTransform.localPosition = new Vector3(
            soilPlaneTransform.localPosition.x,
            soilBottomY,
            soilPlaneTransform.localPosition.z
        );

        UpdateVisual();
    }

    public void FillSoil(float amount)
    {
        if (currentFillAmount >= maxFillAmount)
        {
            isFilled = true;
            Debug.Log("Pot is filled with soilzone");
            return;
        }

        currentFillAmount = Mathf.Min(currentFillAmount + amount, maxFillAmount);
        float fillRatio = currentFillAmount / maxFillAmount;

        float newY = Mathf.Lerp(soilBottomY, soilTopY, fillRatio);
        soilPlaneTransform.localPosition = new Vector3(
            soilPlaneTransform.localPosition.x,
            newY,
            soilPlaneTransform.localPosition.z
        );
    }

    public void AddWater(Vector3 hitPoint, float amount)
    {
        if (!isFilled) return;
        Debug.Log("Adding water");
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
