using System;
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

    private void Awake()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        shieldBar.SetMaxShield(maxShield);
    }

    private void Update()
    {
        currentHealth = PlayerPrefs.GetInt("Health",100);
        currentShield = PlayerPrefs.GetInt("Shield",100);
        
        healthBar.SetHealth(currentHealth);
        shieldBar.SetShield(currentShield);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(PlayerPrefs.GetInt("Health"));
        Debug.Log(PlayerPrefs.GetInt("Shield"));
        
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
