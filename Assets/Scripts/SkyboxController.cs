using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material daySky;
    public Material nightSky;
    public Material sunsetSky;

    public void SetDay()
    {
        RenderSettings.skybox = daySky;
    }

    public void SetNight()
    {
        RenderSettings.skybox = nightSky;
    }

    public void SetSunset()
    {
        RenderSettings.skybox = sunsetSky;
    }
}