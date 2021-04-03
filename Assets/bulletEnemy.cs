using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo);
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if(player!=null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
