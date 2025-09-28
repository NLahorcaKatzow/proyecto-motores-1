using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static HealthController Instance;
    public int currentHealth = 3;
    public int maxHealth = 3;


    void Awake()
    {
        Instance = this;
    }

    public void TakeDamage(int damage = 1)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int amount = 1)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    
    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void Die()
    {
        //TODO: Implementar la muerte del personaje
        //TODO: Implementar escene manager, ui de muerte
    }

}
