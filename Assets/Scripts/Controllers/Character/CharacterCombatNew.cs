using UnityEngine;
using System.Linq;
public class CharacterCombatNew : MonoBehaviour
{
    public float attackRange = 1.5f;      // Radio de ataque
    public int attackDamage = 20;         // Da�o por golpe
    public float attackRate = 2f;         // Ataques por segundo
    public LayerMask enemyLayers;         // Capa de enemigos

    private float nextAttackTime = 0f;
    private Animator animator;

   
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // Click izquierdo = atacar
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Activar animaci�n 
        if (animator != null)
            animator.SetTrigger("Attack"); // TODO: IMPLEMETNACION DE ANIMACIONES DE ATTACK

        // Detectar enemigos en el �rea de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayers);


        foreach (Collider enemy in hitEnemies)
        {
            EnemyBase enemyHealth = enemy.GetComponent<EnemyBase>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Para visualizar el �rea de ataque en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackRange);
    }
}
