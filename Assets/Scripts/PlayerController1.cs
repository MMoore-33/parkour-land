using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    Animator anim;
    bool isCursorLocked = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // -------- Cursor Logic --------
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isCursorLocked = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isCursorLocked = false;
        }

        // -------- Movement / Animation --------
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool isMoving = h != 0 || v != 0;

        anim.SetBool("iswalking", isMoving);
    }

}