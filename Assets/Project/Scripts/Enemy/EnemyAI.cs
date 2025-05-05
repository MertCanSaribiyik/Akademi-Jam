using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float attackSpeed = 4f;
    [SerializeField] private float borderDistance = 8f;
    [SerializeField] private float attackDistance = 2f;

    [Header("Boundary Points")]
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private BoxCollider2D borderCollider;

    [Header("Death Effect Settings")]
    [SerializeField] private GameObject deathEffectParticle;
    [SerializeField] private Color deathEffectColor;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    private bool movingRight = true;
    private bool playerDetected = false;
    private bool isPlayerDead = false;

    // Public Getters
    public Animator Animator => animator;
    public bool IsPlayerDead => isPlayerDead;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        if (borderCollider != null) {
            Vector2 newSize = borderCollider.size;
            newSize.x = borderDistance;
            borderCollider.size = newSize;
        }

        HealthSystem.OnPlayerDeath += OnPlayerDeath;
        HealthSystem.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnDestroy() {
        HealthSystem.OnPlayerDeath -= OnPlayerDeath;
        HealthSystem.OnEnemyDeath -= OnEnemyDeath;
    }

    private void Update() {
        if (isPlayerDead) return;

        if (playerDetected)
            Attack();
        else
            Patrol();
    }

    private void Patrol() {
        float direction = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * patrolSpeed, rb.velocity.y);

        // Flip enemy sprite based on direction
        rb.transform.rotation = Quaternion.Euler(0f, movingRight ? 0f : 180f, 0f);

        if (movingRight && rb.position.x >= rightLimit.position.x)
            movingRight = false;
        else if (!movingRight && rb.position.x <= leftLimit.position.x)
            movingRight = true;
    }

    private void Attack() {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(player.position, rb.position);

        if (distanceToPlayer <= attackDistance) {
            animator.SetTrigger("attack");
            animator.speed = 1f;
            //rb.velocity = new Vector2(0f, rb.velocity.y); // stop moving during attack
        }
        else {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            rb.velocity = new Vector2(direction.x * attackSpeed, rb.velocity.y);

            // Face the player
            if (direction.x > 0f) {
                rb.transform.rotation = Quaternion.Euler(0, 0f, 0);
            }
            else if (direction.x < 0f) {
                rb.transform.rotation = Quaternion.Euler(0, 0180f, 0);
            }

            animator.SetTrigger("move");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerDetected = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerDetected = false;
            animator.speed = 1f;
        }
    }

    private void OnPlayerDeath() {
        isPlayerDead = true;
        rb.velocity = Vector2.zero;
    }

    private void OnEnemyDeath() {
        GameObject deathEffect = Instantiate(deathEffectParticle, transform.position, Quaternion.identity);
        deathEffect.GetComponent<SpriteRenderer>().color = deathEffectColor;
        Destroy(deathEffect, 2f);
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }
}
