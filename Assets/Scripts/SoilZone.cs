using UnityEngine;

public class SoilZone : CustomBehaviour
{
    [Header("Water Settings")]
    [SerializeField] private float absorptionSpeed = 0.4f;
    [SerializeField] private float wetRadius = 0.05f;

    [Header("Visual Settings")]
    [SerializeField] private Renderer soilRenderer;
    [SerializeField] private int textureResolution = 128;

    private Texture2D wetMask;
    private MaterialPropertyBlock propBlock;
    private Color[] pixels;
    private int wetPixelCount = 0;

    public bool IsFullyWatered => wetPixelCount >= pixels.Length * 0.95f;

    public override void CustomStart()
    {
        propBlock = new MaterialPropertyBlock();
        wetMask = new Texture2D(textureResolution, textureResolution, TextureFormat.R8, false);
        pixels = new Color[textureResolution * textureResolution];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.black;
        wetMask.SetPixels(pixels);
        wetMask.Apply();

        soilRenderer.GetPropertyBlock(propBlock);
        propBlock.SetTexture("_WetMask", wetMask);
        soilRenderer.SetPropertyBlock(propBlock);
    }

    public void AddWater(Vector3 hitPoint, float amount)
    {
        Vector3 local = soilRenderer.transform.InverseTransformPoint(hitPoint);
        Vector3 size = soilRenderer.bounds.size;
        Vector2 uv = new Vector2(local.x / size.x + 0.5f, local.z / size.z + 0.5f);

        int centerX = Mathf.RoundToInt(uv.x * textureResolution);
        int centerY = Mathf.RoundToInt(uv.y * textureResolution);
        int radiusPixels = Mathf.RoundToInt(wetRadius * textureResolution);

        for (int y = -radiusPixels; y <= radiusPixels; y++)
        {
            for (int x = -radiusPixels; x <= radiusPixels; x++)
            {
                int px = centerX + x;
                int py = centerY + y;
                if (px < 0 || px >= textureResolution || py < 0 || py >= textureResolution)
                    continue;

                float dist = Mathf.Sqrt(x * x + y * y);
                if (dist > radiusPixels)
                    continue;

                int index = py * textureResolution + px;
                float current = pixels[index].r;
                float target = Mathf.Clamp01(current + amount * absorptionSpeed);
                if (target > current)
                {
                    pixels[index].r = target;
                }
            }
        }

        wetMask.SetPixels(pixels);
        wetMask.Apply();

        propBlock.SetTexture("_WetMask", wetMask);
        soilRenderer.SetPropertyBlock(propBlock);

        wetPixelCount = 0;
        foreach (var c in pixels)
            if (c.r > 0.8f)
                wetPixelCount++;
    }
}
