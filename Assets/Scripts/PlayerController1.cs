using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Animator anim;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ===== GROUND CHECK =====
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // ===== INPUT =====
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Camera-relative movement
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * v + camRight * h;
        move = Vector3.ClampMagnitude(move, 1f);

        // ===== ROTATION =====
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        // ===== MOVEMENT STATE =====
        bool isMoving = move.magnitude > 0.1f;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isMoving;

        float speed = isSprinting ? runSpeed : walkSpeed;

        // ===== JUMP =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            anim.SetTrigger("jump");
        }

        // ===== GRAVITY =====
        velocity.y += gravity * Time.deltaTime;

        // ===== FINAL MOVE =====
        Vector3 finalMove = move * speed;
        finalMove.y = velocity.y;

        controller.Move(finalMove * Time.deltaTime);

        // ===== FALLING CHECK =====
        bool isFalling = velocity.y < -0.1f && !isGrounded;

        // ===== ANIMATIONS =====
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isSprinting", isSprinting);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);

        // ===== RESPAWN =====
        if (transform.position.y < -10f)
        {
            Respawn();
        }

        Debug.Log(isGrounded);
    }

    void Respawn()
    {
        controller.enabled = false;
        transform.position = new Vector3(0, 2, 0);
        velocity = Vector3.zero;
        controller.enabled = true;
    }
}