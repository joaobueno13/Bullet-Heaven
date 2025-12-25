using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bool isMoving = playerMovement.moveDir != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        SpriteDirectionChecker();
    }

    void SpriteDirectionChecker()
    {
        if (playerMovement.moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerMovement.moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
