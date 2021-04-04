using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public Transform[] waypoints;
    public int damageOnCollision = 20;
    public SpriteRenderer graphics;
    public GameObject gameObject;

    private bool playerSaw = false;
    private bool changeDirection = true;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private bool isShooting = true;
    
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
        if (Vector3.Distance(transform.position, player.position) < 8f || playerSaw ==true)
        {
            if (target == waypoints[0] && changeDirection==true)
            {
                transform.Rotate(0f,180f,0f);
                changeDirection = false;
            }
            animator.SetBool("See_Player",true);
            playerSaw = true;

            if (Vector3.Distance(transform.position, player.position) > 15f)
            {
                playerSaw = false;
            }

            //L'ennemi tire
            if (Vector3.Distance(transform.position, player.position) < 6f && isShooting == true)
            {
                animator.SetTrigger("Shoot");
                Shoot();
                isShooting = false;
                StartCoroutine(shoot_delayed(0.8f));
            }
        }
        else
        {
            if (target == waypoints[0] && changeDirection==false)
            {
                transform.Rotate(0f,180f,0f);
                changeDirection = true;
            }
            
            animator.SetBool("See_Player",false);
            
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            // Si l'ennemi est quasiment arrivé à sa destination
            if(Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
                transform.Rotate(0f,180f,0f);
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator shoot_delayed(float temps)
    {
        yield return new WaitForSeconds(temps);
        isShooting = true;
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
