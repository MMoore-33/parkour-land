using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public float interactDistance = 10f;
    public GameObject interactPrompt;
    public TextMeshProUGUI interactText;

    public Terminal terminal;

    public PlayerController1 player;

    public AudioSource uiAudio;

    public AudioClip uiClickSound;

    void Start()
    {
        // Find the default speed object
        GameObject defaultObj = GameObject.FindGameObjectWithTag("SpeedDefault");

        if (defaultObj != null)
        {
            HighlightSelected(defaultObj);
        }
    }

    void Update()
    {
        Camera cam = Camera.main;

        if (cam == null)
        {
            return;
        }

        // Ray from center of screen
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Ignore player layer
        int layerMask = ~LayerMask.GetMask("Player");

        interactPrompt.SetActive(false);

        if (Physics.Raycast(ray, out hit, interactDistance, layerMask))
        {
            GameObject obj = hit.collider.gameObject;

            // ===== TERMINAL =====
            if (obj.CompareTag("Interactable"))
            {
                interactPrompt.SetActive(true);
                interactText.text = terminal.IsOn() ? "E to Turn Off" : "E to Turn On";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    terminal.Activate();
                }
            }

            // ===== SPEED OPTIONS =====
            else if (obj.CompareTag("SpeedSlow"))
            {
                interactPrompt.SetActive(true);
                interactText.text = "E: Set Speed (Slow)";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.runSpeed = 3f;
                    HighlightSelected(obj);

                    uiAudio.PlayOneShot(uiClickSound);
                }
            }

            else if (obj.CompareTag("SpeedDefault"))
            {
                interactPrompt.SetActive(true);
                interactText.text = "E: Set Speed (Default)";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.runSpeed = 6f;
                    HighlightSelected(obj);

                    uiAudio.PlayOneShot(uiClickSound);
                }
            }

            else if (obj.CompareTag("SpeedFast"))
            {
                interactPrompt.SetActive(true);
                interactText.text = "E: Set Speed (Fast)";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.runSpeed = 9f;
                    HighlightSelected(obj);

                    uiAudio.PlayOneShot(uiClickSound);
                }
            }

            else
            {
                interactPrompt.SetActive(false);
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }

    void HighlightSelected(GameObject selected)
    {
        // Get all speed objects
        GameObject[] slow = GameObject.FindGameObjectsWithTag("SpeedSlow");
        GameObject[] def = GameObject.FindGameObjectsWithTag("SpeedDefault");
        GameObject[] fast = GameObject.FindGameObjectsWithTag("SpeedFast");

        // Reset all text colors
        ResetTextColor(slow);
        ResetTextColor(def);
        ResetTextColor(fast);

        // Highlight selected
        SetTextColor(selected, Color.green);
    }

    void ResetTextColor(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.color = Color.white;
            }
        }
    }

    void SetTextColor(GameObject obj, Color color)
    {
        TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = color;
        }
    }
} 