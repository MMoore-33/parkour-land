using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpSlider : MonoBehaviour
{
    public Slider slider;
    public PlayerController1 player;
    public TextMeshProUGUI valueText;

    public AudioSource uiAudio;
    public AudioClip uiClickSound;

    private float lastValue;

    void Start()
    {
        // Set default
        slider.value = 5;
        player.jumpForce = 5;

        UpdateText(5);

        // Listen for changes
        slider.onValueChanged.AddListener(OnSliderChange);
    }

    void OnSliderChange(float value)
    {
        player.jumpForce = value;
        UpdateText(value);

        if (Mathf.Round(value) != Mathf.Round(lastValue))
        {
            uiAudio.PlayOneShot(uiClickSound);
            lastValue = value;
        }
    }

    void UpdateText(float value)
    {
        valueText.text = "Jump: " + value.ToString("0");
    }
}