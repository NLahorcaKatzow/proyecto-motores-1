using UnityEngine;
using System;
public class MagnaController : MonoBehaviour
{
    public float mana = 100f;                 // Valor actual del mana
    public float maxMana = 100f;              // Valor m�ximo del mana
    public float manaCost = 33f;              // Costo para curar (33%)
    public event Action<float> onMagnaChanged;
    
    
    
    void Update()
    {
        // Si apret�s F y ten�s mana suficiente
        if (Input.GetKeyDown(KeyCode.F) && mana >= manaCost)
        {
            HealPlayer();
        }
    }

    void HealPlayer()
    {
        if (mana >= manaCost)
        {
            // Curamos 1 punto de vida
            HealthController.Instance.Heal(1);

            // Consumimos mana
            mana -= manaCost;

            // Aseguramos que no baje de 0
            if (mana < 0) mana = 0;

            onMagnaChanged?.Invoke(mana);

        }
        }

    // M�todo para recuperar mana (Al matar enemigos)
    public void AddMana(float amount)
    {
        Debug.Log("Adding mana: " + amount);
        mana += amount;
        if (mana > maxMana) mana = maxMana;
        onMagnaChanged?.Invoke(mana);
    }

    // Obtener el porcentaje de mana (para la barra en UI)
    public float GetManaPercent()
    {
        return mana / maxMana;
    }
}
