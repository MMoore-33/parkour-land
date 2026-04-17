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
        }
        else
        {
            rend.material.color = offColor;
            terminalUI.SetActive(false);
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
    }

    public void SetSunset()
    {
        skyboxController.SetSunset();
    }

    public void SetDay()
    {
        skyboxController.SetDay();
    }
}