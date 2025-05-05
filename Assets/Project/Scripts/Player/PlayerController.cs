using System;
using System.Collections;
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

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckDistance = 0.1f; // Distance to check for ground
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;

    [Header("Universes Setting")]
    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject[] universes;
    private int universeIndex = 0;

    [Header("Speech Bubble")]
    [SerializeField] private GameObject speechBubble;

    //Events :
    public event Action OnPlayerAttack = delegate { };

    //Getters : 
    public float DirX { get { return dirX; } }
    public GameObject SpeechBubble { get { return speechBubble; } }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        if(blackout != null) {
            blackout.SetActive(false); // Ensure blackout is inactive at start
            for (int i = 0; i < universes.Length; i++) {
                if (i == universeIndex) {
                    universes[i].SetActive(true);
                }
                else {
                    universes[i].SetActive(false);
                }
            }
        }

        if(speechBubble != null) {
            speechBubble.SetActive(false); // Ensure speech bubble is inactive at start
        }
    }

    private void Update() {
        dirX = Input.GetAxisRaw("Horizontal");
        IsGrounded();

        //Jumping :
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount - 1) {
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }

        //Attack :
        if (Input.GetKeyDown(KeyCode.J)) {
            animator.SetInteger("state", (int)MovementState.attack);
            OnPlayerAttack.Invoke();
        }

        //Chane universe :
        if (Input.GetKeyDown(KeyCode.K)) {
            StartCoroutine(ChangeUniverse());
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);
        updateAnimationState();
    }

    private IEnumerator ChangeUniverse() {
        blackout.SetActive(true);
        universeIndex = (universeIndex == universes.Length - 1) ? 0 : universeIndex + 1;

        yield return new WaitForSeconds(0.25f); // Wait for the blackout to finish

        for (int i = 0; i < universes.Length; i++) {
            if (i == universeIndex) {
                universes[i].SetActive(true);
            }
            else {
                universes[i].SetActive(false);
            }
        }
    }

    private enum MovementState { idle, running, attack, jumping, falling }

    private void updateAnimationState() {
        MovementState state;

        //Running and idle animation : 
        if (dirX != 0f) {
            state = MovementState.running;

            if (dirX > 0f) {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation to default
            }

            else if (dirX < 0f) {
                transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate 180 degrees
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
        RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, groundCheckDistance, jumpableGround);
        RaycastHit2D hitRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, groundCheckDistance, jumpableGround);

        bool isGrounded = hitLeft.collider != null || hitRight.collider != null;

        if (isGrounded) {
            jumpCount = 0;
        }

        return isGrounded;
    }

    private void OnDrawGizmosSelected() {
        if (groundCheckLeft != null && groundCheckRight != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheckLeft.position, groundCheckLeft.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheckRight.position, groundCheckRight.position + Vector3.down * groundCheckDistance);
        }
    }
}
