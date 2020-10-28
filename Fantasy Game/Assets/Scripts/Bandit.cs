using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;
    private bool                m_isDead = false;

    public int attackDamage = 99;

    public int health;
    public int maxHealth = 1000;
    public HealthBar healthBar;

    public Transform player;
    public bool isFlipped = true;
    public LayerMask playerMask;
    public float attackRange = 4f;
    public Vector3 attackOffset;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(8,10);
        health = maxHealth;
        healthBar.MaxHealth(maxHealth);
    }

    public void LookAtPlayer() {
        Vector3 flip = transform.localScale;
        flip.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped) {
            transform.localScale = flip;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        } else if (transform.position.x > player.position.x && !isFlipped) {
            transform.localScale = flip;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Attack() {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        FindObjectOfType<AudioManager>().Play("enemy-attack");

        Collider2D hitPlayer = Physics2D.OverlapCircle(pos,attackRange, playerMask);
        if (hitPlayer != null) {
            if (hitPlayer.GetComponent<HeroKnight>().TakeDamage(attackDamage)) {
                m_animator.SetBool("playerDead", true);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        
        Gizmos.DrawWireSphere(pos,attackRange);
    }

    public void TakeDamage(int damage){
        if(m_isDead == false && health > 0) {
            health -= damage;
            healthBar.SetHealth(health);
            m_animator.SetTrigger("hurt");
            if(health <= 0) {
                FindObjectOfType<AudioManager>().Play("death");
                FindObjectOfType<AudioManager>().Play("body-thud");
                m_animator.SetBool("dead",true);
                m_isDead = true;
            }
        }
    }
}
