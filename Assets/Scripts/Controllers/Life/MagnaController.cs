using UnityEngine;

public class MagnaController : MonoBehaviour
{
   
    public float mana = 100f;                 // Valor actual del mana
    public float maxMana = 100f;              // Valor máximo del mana
    public float manaCost = 33f;              // Costo para curar (33%)

    void Update()
    {
        // Si apretás F y tenés mana suficiente
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

        }
        }

    // Método para recuperar mana (Al matar enemigos)
    public void AddMana(float amount)
    {
        mana += amount;
        if (mana > maxMana) mana = maxMana;
    }

    // Obtener el porcentaje de mana (para la barra en UI)
    public float GetManaPercent()
    {
        return mana / maxMana;
    }
}
