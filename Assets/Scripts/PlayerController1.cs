using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public AudioClip footstepSound;
    public AudioClip jumpLandSound;
    public AudioClip respawnSound;

    private AudioSource audioSource;

    private bool wasGrounded;

    private float footstepTimer;
    public float footstepDelay = 0.5f; // time between steps

    public Transform spawnPoint;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Animator anim;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
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

        // Camera-relative movement (IMPORTANT FIX)
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * v + right * h;

        bool isMoving = move.magnitude > 0.1f;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isMoving;

        float speed = isSprinting ? runSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        // ===== FOOTSTEPS =====
        if (isGrounded && isMoving)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                audioSource.PlayOneShot(footstepSound);
                float delay = isSprinting ? 0.3f : 0.5f;
                footstepTimer = delay;
            }
        }
        else
        {
            footstepTimer = 0f;
        }

        // ===== ROTATE PLAYER TO FACE MOVEMENT =====
        if (isMoving)
        {
            transform.forward = move;
        }

        // ===== JUMP =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            anim.SetTrigger("jump");
        }

        // ===== GRAVITY =====
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ===== LANDING DETECTION =====
        if (!wasGrounded && isGrounded && velocity.y < -2f)
        {
            audioSource.PlayOneShot(jumpLandSound);
        }

        wasGrounded = isGrounded;

        // ===== FALLING CHECK =====
        bool isFalling = velocity.y < -1f && !isGrounded;

        // ===== ANIMATIONS =====
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isSprinting", isSprinting);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);

        // ===== RESPAWN =====
        if (transform.position.y < -10f)
        {
            Respawn();
            audioSource.PlayOneShot(respawnSound);
        }
    }

    void Respawn()
    {
        controller.enabled = false;
        transform.position = spawnPoint.position;
        velocity = Vector3.zero;
        controller.enabled = true;
    }
}