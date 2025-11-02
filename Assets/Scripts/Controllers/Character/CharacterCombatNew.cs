using UnityEngine;
using System.Linq;
using Utilities;
using DG.Tweening;
public class CharacterCombatNew : MonoBehaviour
{
    public float attackRange = 1.5f;      // Radio de ataque
    public int attackDamage = 20;         // Da�o por golpe
    public float attackRate = 2f;         // Ataques por segundo
    public LayerMask enemyLayers;         // Capa de enemigos
    public GameObject attackVFX;
    private float nextAttackTime = 0f;
    private Animator animator;
    public MagnaController magnaController;


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
        if (attackVFX != null)
        {
            attackVFX.SetActive(true);
            AnimateAttackVFX();
        }


        // Detectar enemigos en el �rea de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayers);



        foreach (Collider enemy in hitEnemies)
        {
            EnemyBase enemyHealth = enemy.GetComponent<EnemyBase>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage, this.transform.position);
cha                magnaController.AddMana(33);
            }
        }
    }

    // Para visualizar el �rea de ataque en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackRange);
    }
    

    
    
    /// <summary>
    /// Anima el VFX de ataque con rotaci�n de 360 grados y escala descendente
    /// La duraci�n coincide con el cooldown del ataque
    /// </summary>
    private void AnimateAttackVFX()
    {
        if (attackVFX == null) return;
        
        // Calcular duraci�n basada en el attack rate (cooldown)
        float animationDuration = 1f / attackRate;
        
        // Resetear escala y rotaci�n inicial
        attackVFX.transform.localScale = Vector3.one;
        attackVFX.transform.localRotation = Quaternion.Euler(0,0,0);
        
        // Crear secuencia de animaciones
        Sequence vfxSequence = DOTween.Sequence();
        
        // Rotaci�n de 360 grados alrededor del eje Y (padre)
        vfxSequence.Join(attackVFX.transform.DOLocalRotate(new Vector3(0, 360, 0), animationDuration, RotateMode.FastBeyond360));
        
        // Escala descendente hasta 0
        vfxSequence.Join(attackVFX.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InCubic));
        
        // Al completar, desactivar el VFX
        vfxSequence.OnComplete(() =>
        {
            attackVFX.SetActive(false);
        });
    }
}
