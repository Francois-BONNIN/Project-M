using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public SpriteRenderer graphics;
    public GameObject gameObject;

    private Transform target;
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
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi est quasiment arrivé à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    public void takeDamage(int damage)
    {
        health -=damage;

        if(health<=0)
        {
            Die();
        }
    }
    void Die()
    {
        //Instantiate(deathEffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
