using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator animator;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private float dirX;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private int maxJumpCount = 2;
    private int jumpCount = 0;

    [SerializeField] private GameObject blackout;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        blackout.SetActive(false); // Ensure blackout is inactive at start
    }

    private void Update() {
        dirX = Input.GetAxisRaw("Horizontal");
        IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount - 1) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            animator.SetInteger("state", (int)MovementState.attack);
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            blackout.SetActive(true);
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);
        updateAnimationState();
    }

    private enum MovementState { idle, running, attack, jumping, falling }

    private void updateAnimationState() {
        MovementState state;

        //Running and idle animation : 
        if (dirX != 0f) {
            state = MovementState.running;

            if (dirX > 0f) {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            else if (dirX < 0f) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        else {
            state = MovementState.idle;
        }

        //Jumping and Falling animation :
        if (rb.velocity.y > 0.1f) {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -0.1f) {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded() {
        bool isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, -transform.up, 0.1f, jumpableGround);

        if (isGrounded) {
            jumpCount = 0; // Reset jump count when grounded
        }

        return isGrounded;
    }
}
