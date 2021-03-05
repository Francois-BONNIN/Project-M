using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //demande si le joueur touche l'objet
        {
            Inventory.instance.AddCoins(5);
            Destroy(gameObject);
        }
    }
}
