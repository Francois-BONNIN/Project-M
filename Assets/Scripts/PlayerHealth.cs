using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxShield = 100;

    public int currentHealth;
    public int currentShield;

    public HealthBar healthBar;
    public ShieldBar shieldBar;
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        healthBar.SetMaxHealth(maxHealth);// pour que la barre de vie suive les points de vie
        shieldBar.SetMaxShield(maxShield);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeHealth(20);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeShield(20);
        }
    }
    void TakeDamage(int damage)
    {
        if (currentShield > 0)
        {
            if (currentShield - damage < 0)
            {
                currentShield = 0;
            }
            else
            {
                currentShield -= damage;
            }
            shieldBar.SetShield(currentShield);
        } 
        else
        {
            if (currentHealth - damage < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= damage;
            }
            healthBar.SetHealth(currentHealth);
        }
    }
    void TakeHealth(int health)
    {
        if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
        healthBar.SetHealth(currentHealth);
    }
    void TakeShield(int shield)
    {
        if (currentShield + shield > maxShield)
        {
            currentShield = maxShield;
        }
        else
        {
            currentShield += shield;
        }
        shieldBar.SetShield(currentShield);
    }
}
