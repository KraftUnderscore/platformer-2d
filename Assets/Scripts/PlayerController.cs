using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius;

    [SerializeField] private float movementSpeed;
    [Range(0f, 1f)] [SerializeField] private float movementSmoothing;

    [SerializeField] private AudioClip[] audioClips;

    private float movementInput;
    private bool interactInput;
    private bool jumpInput;

    private Vector2 current_velocity;
    private Rigidbody2D rigid;
    private Animator anim;
    private AudioSource sound;
    private bool playerDirection;
    private bool isGrounded;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        isGrounded = true;
    }

    public void FootstepEvent()
    {
        sound.clip = audioClips[Random.Range(0, audioClips.Length)];
        sound.Play();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
        anim.SetBool("isWalking", !(movementInput == 0));
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            anim.SetTrigger("jump");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interactInput = true;
            anim.SetTrigger("interact");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("MovingPlatform"))
            transform.SetParent(collision.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("MovingPlatform"))
            transform.parent = null;
    }

    private void Update()
    {
        anim.SetFloat("fallingSpeed", rigid.velocity.y);
    }

    private void FixedUpdate()
    {
        Interact();
        Move();
        GroundedCheck();
        Jump();
    }

    private void Move()
    {
        if (movementInput == 0) return;

        Vector2 target = new Vector2(movementInput * movementSpeed, rigid.velocity.y);
        rigid.velocity = Vector2.SmoothDamp(rigid.velocity, target, ref current_velocity, movementSmoothing);
        Flip();
    }

    private void Flip()
    {
        if (movementInput > 0 && !playerDirection) return;
        if (movementInput < 0 && playerDirection) return;

        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        playerDirection = !playerDirection;
    }

    private void Interact()
    {
        if (!interactInput) return;
        interactInput = false;

        var colliders = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRadius, interactableMask);
        foreach (var col in colliders)
            col.GetComponent<Interactable>().Interact();
    }

    private void GroundedCheck()
    {
        if (isGrounded) return;

        var colliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, groundMask);
        if (colliders.Length > 0) isGrounded = true;
    }

    private void Jump()
    {
        if(jumpInput && isGrounded)
        {
            jumpInput = false;
            isGrounded = false;
            rigid.AddForce(Vector2.up * jumpForce);
        }
    }
}
