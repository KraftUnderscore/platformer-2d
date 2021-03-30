using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [Range(0f, 1f)] [SerializeField] private float movementSmoothing;

    private float movementInput;

    private Vector2 current_velocity;
    private Rigidbody2D rigid;

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

    }

    public void OnInteract(InputAction.CallbackContext context)
    {

    }

    private void FixedUpdate()
    {
        Vector2 target = new Vector2(movementInput * movementSpeed, rigid.position.y);
        rigid.velocity = Vector2.SmoothDamp(rigid.velocity, target, ref current_velocity, movementSmoothing);
    }
}
