using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Attack Settings")]
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackMask;

    private bool isFlipped;

    public void LookAtPlayer() {
        //Vector3 flipped = transform.localScale;
        //flipped.z *= -1f;

        //if(transform.position.x > player.position.x && isFlipped) {
        //    transform.localScale = flipped;
        //    transform.Rotate(0f, 180f, 0f);
        //    isFlipped = false;
        //}

        //else if(transform.position.x > player.position.x && !isFlipped) {
        //    transform.localScale = flipped;
        //    transform.Rotate(0f, 180f, 0f);
        //    isFlipped = true;
        //}

        Vector2 direction = (player.position - transform.position).normalized;

        // Face the player
        if (direction.x > 0f) {
            transform.rotation = Quaternion.Euler(0, 0f, 0);
        }
        else if (direction.x < 0f) {
            transform.rotation = Quaternion.Euler(0, 0180f, 0);
        }

    }

    public void Attack() {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if(colInfo != null) {
            colInfo.GetComponent<HealthSystem>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected() {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);
    }

}
