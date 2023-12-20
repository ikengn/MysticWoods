using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1;
    public float knockBackPlayer = 125f;
    public float knockBackAllies = 50f;
    public float moveSpeed = 50f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate() {
        //should only find the player
        if (detectionZone.detecteds.Count > 0) {
            animator.SetBool("isMoving", true);
            //get diretion
            Vector2 direction = (detectionZone.detecteds[0].transform.position - transform.position).normalized;
            if (direction.x > 0) {
                spriteRenderer.flipX = false;
            } else if (direction.x < 0) {
                spriteRenderer.flipX = true;
            }
            //move towards the player
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        } else {
            animator.SetBool("isMoving", false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Damageable damageable = collision.collider.GetComponent<Damageable>();
        if (damageable != null) {
            // direction between slime and player
            UnityEngine.Vector2 direction = (collider.gameObject.transform.position - transform.position).normalized;
            if (collider.gameObject.tag == "Player") {
                UnityEngine.Vector2 knockBack = direction * knockBackPlayer;
                damageable.OnHit(damage, knockBack);
            } else if (collider.gameObject.tag == "Enemy") {
                UnityEngine.Vector2 knockBack = direction * knockBackAllies;
                damageable.OnHit(damage, knockBack);
            }
        }
    }
}
