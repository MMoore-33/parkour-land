using UnityEngine;

public class Terminal : MonoBehaviour
{
    private bool isOn = false;
    private Renderer rend;

    public static bool isUIOpen = false;

    public GameObject terminalUI;

    public Color offColor = Color.white;
    public Color onColor = Color.green;

    // ADD THIS
    public SkyboxController skyboxController;

    public AudioSource terminalAudio;
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;

    public AudioSource uiAudio;
    public AudioClip uiClickSound;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = offColor;

        terminalUI.SetActive(false);
    }

    public void Activate()
    {
        isOn = !isOn;
        isUIOpen = isOn;

        if (isOn)
        {
            rend.material.color = onColor;
            terminalUI.SetActive(true);
            terminalAudio.PlayOneShot(turnOnSound);
        }
        else
        {
            rend.material.color = offColor;
            terminalUI.SetActive(false);
            terminalAudio.PlayOneShot(turnOffSound);
        }
    }

    public bool IsOn()
    {
        return isOn;
    }

    // ADD THESE FUNCTIONS
    public void SetNight()
    {
        skyboxController.SetNight();
        uiAudio.PlayOneShot(uiClickSound);
    }

    public void SetSunset()
    {
        skyboxController.SetSunset();
        uiAudio.PlayOneShot(uiClickSound);
    }

    public void SetDay()
    {
        skyboxController.SetDay();
        uiAudio.PlayOneShot(uiClickSound);
    }
}