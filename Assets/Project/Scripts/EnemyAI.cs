using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float attackSpeed = 4f;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private BoxCollider2D borderColl;
    [SerializeField] private float borderDistance = 8f;
    [SerializeField] private float attackDistance = 2f;


    private bool movingRight = true;
    private bool playerDetected = false;
    private Transform player;

    private Rigidbody2D enemyRb;
    private Animator animator;

    private bool isPlayerDead = false;

    //Getters
    public bool IsPlayerDead { get => isPlayerDead; }

    private void Awake() {
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Vector2 newSize = borderColl.size;
        newSize.x = borderDistance;
        borderColl.size = newSize;

        HealthSystem.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy() {
        HealthSystem.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath() {
        isPlayerDead = true;
        animator.SetTrigger("idle");
    }

    void Update() {
        if(!isPlayerDead) {
            if (!playerDetected)
                Patrol();
            else
                Attack();
        }
    }

    void Patrol() {
        if (movingRight) {
            enemyRb.transform.rotation = Quaternion.Euler(0, 180f, 0);
            enemyRb.velocity = new Vector2(patrolSpeed * 1, enemyRb.velocity.y);
            if (enemyRb.transform.position.x >= rightLimit.position.x)
                movingRight = false;
        }
        else {
            enemyRb.transform.rotation = Quaternion.Euler(0, 0f, 0);
            enemyRb.velocity = new Vector2(patrolSpeed * -1, enemyRb.velocity.y);
            if (enemyRb.transform.position.x <= leftLimit.position.x)
                movingRight = true;
        }
    }

    void Attack() {
        if (player != null) {
            Vector2 direction = (player.position - enemyRb.transform.position).normalized;

            if (Vector2.Distance(player.position, enemyRb.transform.position) <= attackDistance) {
                animator.SetTrigger("attack");
                animator.speed = 1f;
            }

            else {
                enemyRb.velocity = new Vector2(direction.x * attackSpeed, enemyRb.velocity.y);

                if (direction.x > 0f) {
                    enemyRb.transform.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else if (direction.x < 0f) {
                    enemyRb.transform.rotation = Quaternion.Euler(0, 0f, 0);
                }

                animator.SetTrigger("move");
                animator.speed = attackSpeed / patrolSpeed;
            }
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
}
