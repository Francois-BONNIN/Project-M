using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public Transform[] waypoints;
    public int damageOnCollision = 20;
    public SpriteRenderer graphics;
    public GameObject gameObject;

    private bool changeDirection = true;
    
    private Transform target;
    public Transform player;
    private int destPoint = 0;

    public int health = 100;

    //public GameObject deathEffect;

    void Start()
    {
        target = waypoints[0];
        graphics = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector3 distPlayerEnnemi = player.position - transform.position;
        
        Vector3 dir = target.position - transform.position;

        //Si l'ennemi voit le joueur
        Debug.Log(Vector3.Distance(transform.position, player.position));
        if (Vector3.Distance(transform.position, player.position) < 8f)
        {
            if (target == waypoints[0] && changeDirection==true)
            {
                graphics.flipX = !graphics.flipX;
                changeDirection = false;
            }
            animator.SetBool("See_Player",true);

            if (Vector3.Distance(transform.position, player.position) < 6f)
            {
                animator.SetTrigger("Shoot");
            }
        }
        else
        {
            if (target == waypoints[0] && changeDirection==false)
            {
                graphics.flipX = !graphics.flipX;
                changeDirection = true;
            }
            
            animator.SetBool("See_Player",false);
            
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            // Si l'ennemi est quasiment arrivé à sa destination
            if(Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
                graphics.flipX = !graphics.flipX;
            }
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //Instantiate(deathEffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Touch");
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
            Debug.Log("Shield : " + playerHealth.currentShield + "  Health : " + playerHealth.currentHealth);
        }
    }
}
