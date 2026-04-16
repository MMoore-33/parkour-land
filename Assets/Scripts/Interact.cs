using UnityEngine;

public class Interact : MonoBehaviour
{
    public float interactDistance = 40f;

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        // BIG forgiving cast (like a thick ray)
        if (Physics.SphereCast(ray, 0.5f, out hit, interactDistance))
        {
            Debug.Log("HIT SOMETHING: " + hit.collider.name +
                      " | Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        }
        else
        {
            Debug.Log("HIT NOTHING");
        }

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
    }
}
