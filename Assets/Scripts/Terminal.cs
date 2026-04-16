using UnityEngine;

public class Terminal : MonoBehaviour
{
    private bool isOn = false;
    private Renderer rend;

    public Color offColor = Color.white;
    public Color onColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = offColor;
    }

    public void Activate()
    {
        isOn = !isOn;

        if (isOn)
        {
            rend.material.color = onColor;
        }
        else
        {
            rend.material.color = offColor;
        }
    }

    public bool IsOn()
    {
        return isOn;
    }
}
