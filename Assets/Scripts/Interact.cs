using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public float interactDistance = 6f;
    public GameObject interactPrompt;
    public TextMeshProUGUI interactText;

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
            if (hit.collider.CompareTag("Interactable"))
            {
                Terminal terminal = hit.collider.GetComponent<Terminal>();

                if (terminal != null)
                {
                    // show prompt
                    interactPrompt.SetActive(true);

                    // CHANGE TEXT BASED ON STATE
                    if (terminal.IsOn())
                    {
                        interactText.text = "E to turn off";
                    }
                    else
                    {
                        interactText.text = "E to turn on";
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        terminal.Activate();
                    }
                }
            }
        }
    }
} 