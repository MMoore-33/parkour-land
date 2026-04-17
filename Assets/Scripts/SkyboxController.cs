using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material daySky;
    public Material nightSky;
    public Material sunsetSky;

    public Light sunLight;

    void Start()
    {
        RenderSettings.skybox = daySky;
        DynamicGI.UpdateEnvironment();
        RenderSettings.ambientIntensity = 1.2f;

        sunLight.color = Color.white;
        sunLight.intensity = 1.2f;

        sunLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        RenderSettings.fog = false;
    }

    public void SetDay()
    {
        RenderSettings.skybox = daySky;
        DynamicGI.UpdateEnvironment();
        RenderSettings.ambientIntensity = 1.2f;

        sunLight.color = Color.white;
        sunLight.intensity = 1.2f;

        sunLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        RenderSettings.fog = false;
    }

    public void SetNight()
    {
        RenderSettings.skybox = nightSky;
        DynamicGI.UpdateEnvironment();
        RenderSettings.ambientIntensity = 0.7f;

        sunLight.color = new Color(0.4f, 0.4f, 0.6f); // bluish moonlight
        sunLight.intensity = 0.3f;
        sunLight.transform.rotation = Quaternion.Euler(120f, -30f, 40f);

        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.05f, 0.05f, 0.1f);
        RenderSettings.fogDensity = 0.02f;
    }

    public void SetSunset()
    {
        RenderSettings.skybox = sunsetSky;
        DynamicGI.UpdateEnvironment();
        RenderSettings.ambientIntensity = 0.3f;

        sunLight.transform.rotation = Quaternion.Euler(10f, 120f, 0f); // low angle = sunset
        sunLight.color = new Color(1f, 0.5f, 0.3f); // orange
        sunLight.intensity = 0.8f;

        RenderSettings.fog = false;
    }
}