using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); // Já começa olhando para direita
    }

    void Update()
    { 
        InputManagement();
        rb.linearVelocity = moveDir * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    void InputManagement()
    {
        // LastMovedVector apenas com movimento horizontal
        if (moveDir.x != 0)
        {
            lastMovedVector = new Vector2(moveDir.x > 0 ? 1 : -1, 0f);
        }
    }
}
