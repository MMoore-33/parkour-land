using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public float smoothSpeed = 10f;

    public Transform pivot;

    float xRotation = 0f;
    float yRotation = 0f;

    float currentX;
    float currentY;

    bool canLook = false;
    float lookDelay = 0.2f;

    void Start()
    {
        // Get current rotation from the camera
        xRotation = transform.localEulerAngles.x;
        yRotation = pivot.localEulerAngles.y;

        if (yRotation > 180f)
            yRotation -= 360f;

        // Fix Unity angle wrap (VERY important)
        if (xRotation > 180f)
            xRotation -= 360f;

        // Sync smoothing variables
        currentX = xRotation;
        currentY = yRotation;

        Invoke(nameof(EnableLook), lookDelay);
    }

    void Update()
    {
        HandleCursorToggle();
        HandleMouseLook();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnlockCursor();

        if (Input.GetMouseButtonDown(1))
            LockCursor();
    }

    void HandleMouseLook()
    {
        if (!canLook) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 60f);

        yRotation += mouseX;

        // Smooth rotation (this is the upgrade)
        currentX = Mathf.Lerp(currentX, xRotation, smoothSpeed * Time.deltaTime);
        currentY = Mathf.Lerp(currentY, yRotation, smoothSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(currentX, 0f, 0f);
        pivot.localRotation = Quaternion.Euler(0f, currentY, 0f);
    }

    void EnableLook()
    {
        canLook = true;
    }
}