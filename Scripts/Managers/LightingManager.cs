using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;
    [SerializeField]
    private LightingData lightData;
    [SerializeField, Range(0, 24)]
    private float timeOfDay;
    [SerializeField]
    private float timeScale = 0.5f;

    private void Update()
    {
        if (lightData == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * timeScale;
            timeOfDay %= 24;
            float timePercent = timeOfDay / 24f;
            UpdatLighting(timePercent);
        }
        else
        {
            UpdatLighting(timeOfDay / 24f);
        }
    }
    private void UpdatLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightData.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightData.fogColor.Evaluate(timePercent);
        if (directionalLight != null)
        {
            directionalLight.color = lightData.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }
    private void OnValidate()
    {
        if (directionalLight != null)
        {
            return;
        }
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
