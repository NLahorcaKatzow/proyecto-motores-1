using UnityEngine;
using System;
public class HealthController : MonoBehaviour
{
    public static HealthController Instance { get; private set; }
    public event Action<int> onHealthChanged;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private int maxHealth = 3;

    public int CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = Mathf.Max(1, value); } // Evitamos que la vida m�xima sea menor a 1
    }

    private void Awake()
    {
        // Si ya existe una instancia, destruimos este objeto para evitar duplicados
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantiene el objeto al cambiar de escena
        CurrentHealth = maxHealth;     // Al iniciar, vida llena
    }

    public void TakeDamage(int damage = 1)
    {
        CurrentHealth -= damage;
        onHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount = 1)
    {
        CurrentHealth += amount;
        onHealthChanged?.Invoke(CurrentHealth);
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        onHealthChanged?.Invoke(CurrentHealth);
    }

    public void Die()
    {
        //TODO: Implementar la muerte del personaje
        //TODO: Implementar SceneManager, UI de muerte
        SceneManager.Instance.OnPlayerDeath();
    }
}
