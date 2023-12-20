using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }
    bool IsMovingUp
    {
        set
        {
            isMovingUp = value;
            animator.SetBool("isMovingUp", isMovingUp);
        }
    }
    bool IsMovingDown
    {
        set
        {
            isMovingDown = value;
            animator.SetBool("isMovingDown", isMovingDown);
        }
    }
    public float moveSpeed = 500f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;
    Rigidbody2D rb;
    Animator animator;
    public GameObject attackHitBox;

    bool isMoving = false;
    bool isMovingUp = false;
    bool isMovingDown = false;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        if (canMove && moveInput != Vector2.zero)
        {

            // Accelerate with cap
            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);
            if (rb.velocity.magnitude > maxSpeed) {
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }

            if (moveInput.y > 0 && moveInput.x == 0)
            {
                IsMovingUp = true;
                IsMovingDown = false;
                IsMoving = false;
                gameObject.BroadcastMessage("IsFacing", 1);
            }
            else if (moveInput.y < 0 && moveInput.x == 0)
            {
                IsMovingUp = false;
                IsMovingDown = true;
                IsMoving = false;
                gameObject.BroadcastMessage("IsFacing", 2);
            }
            else
            {
                // look left or right
                if (moveInput.x > 0)
                {
                    spriteRenderer.flipX = false;
                    gameObject.BroadcastMessage("IsFacing", 3);
                }
                else if (moveInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                    gameObject.BroadcastMessage("IsFacing", 4);
                }
                IsMoving = true;
                IsMovingUp = false;
                IsMovingDown = false;
            }

        }
        else
        {
            // no movement
            IsMoving = false;
            IsMovingUp = false;
            IsMovingDown = false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnFire()
    {
        animator.SetTrigger("Attack");
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
