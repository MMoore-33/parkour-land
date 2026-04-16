using UnityEngine;

public class Terminal : MonoBehaviour
{
    private bool isOn = false;
    private Renderer rend;

    public GameObject terminalUI;

    public Color offColor = Color.white;
    public Color onColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = offColor;

        terminalUI.SetActive(false);
    }

    public void Activate()
    {
        isOn = !isOn;

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
}
