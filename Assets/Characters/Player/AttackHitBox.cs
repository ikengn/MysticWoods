using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public Collider2D swordCollider;
    public float attackDamage = 1f;
    public float knockBackValue = 100f;
    public UnityEngine.Vector3 faceRight = new UnityEngine.Vector3(0.1f, -0.09f, 0);
    public UnityEngine.Vector3 faceLeft = new UnityEngine.Vector3(-0.1f, -0.09f, 0);
    public UnityEngine.Vector3 faceUp = new UnityEngine.Vector3(0, 0.1f, 0);
    public UnityEngine.Vector3 faceDown = new UnityEngine.Vector3(0, -0.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if(swordCollider == null) {
            Debug.LogWarning("Sword Collider not found");
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {

        Damageable damageableObject = collider.GetComponent<Damageable>();

        if (damageableObject != null) {
            // direction between player and slime
            UnityEngine.Vector3 parentPosition = transform.parent.position;
            UnityEngine.Vector2 direction = (collider.gameObject.transform.position - parentPosition).normalized;
            UnityEngine.Vector2 knockBack = direction * knockBackValue;

            damageableObject.OnHit(attackDamage, knockBack);
        } 
    }

    public void IsFacing(int isFacing) {
        if (isFacing == 1) {
            gameObject.transform.localPosition= faceUp;
        } else if (isFacing == 2) {
            gameObject.transform.localPosition= faceDown;
        } else if (isFacing == 3) {
            gameObject.transform.localPosition= faceRight;
        } else {
            gameObject.transform.localPosition = faceLeft;
        }
    }

}
