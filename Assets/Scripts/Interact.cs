using UnityEngine;

public class Interact : MonoBehaviour
{
    public float interactDistance = 40f;

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

        if (Physics.Raycast(ray, out hit, interactDistance, layerMask))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Terminal terminal = hit.collider.GetComponent<Terminal>();

                    if (terminal != null)
                    {
                        terminal.Activate();
                    }
                }
            }
        }
    }
}