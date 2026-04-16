using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180f, 0); // flip so it faces correctly
        }
    }
}
