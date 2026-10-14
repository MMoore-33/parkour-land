using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 3, -3);
    public float followSpeed = 10f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        // Smooth follow (this is the upgrade)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}