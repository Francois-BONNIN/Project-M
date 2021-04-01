using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxShield = 100;

    public int currentHealth;
    public int currentShield;

    public bool isInvincible = false;
    public float invicibilityFlashDelay = 0.35f;

    public SpriteRenderer graphics;

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
        /*        if (Input.GetKeyDown(KeyCode.H))
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
                }*/
    }
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
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
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }



    }
    public void TakeHealth(int health)
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
    
    public void TakeShield(int shield)
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

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 0.2f, 0.2f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(2f);
        isInvincible = false;
    }
}
