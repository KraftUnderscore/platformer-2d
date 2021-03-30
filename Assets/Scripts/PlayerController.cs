using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSpeed;
    [Range(0f, 1f)] [SerializeField] private float movementSmoothing;

    private float movementInput;
    private bool interactInput;
    private bool jumpInput;

    private Vector2 current_velocity;
    private Rigidbody2D rigid;
    private bool playerDirection;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            jumpInput = true;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            interactInput = true;
    }

    private void FixedUpdate()
    {
        Interact();
        Move();
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

    }

    private void Jump()
    {
        if(jumpInput)
        {
            jumpInput = false;
            // need grounded check
            rigid.AddForce(Vector2.up * jumpForce);
        }
    }
}
