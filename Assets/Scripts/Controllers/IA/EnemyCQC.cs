using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Utilities;
public class EnemyCQC : EnemyBase
{
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true;
    private bool canUpdateMovement = true;
    private Animator animator;
    // Movement and behavior variables
    public Vector3 nextPosition;
    public float visualRange = 10f;
    public float movementUpdateInterval = 0.5f; // Time between movement updates
    public GameObject attackVFX;
    private Sequence _attackSeq;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = velocity;
        }
        animator = GetComponentInChildren<Animator>();
        if (animator != null ) 
        {
            Debug.LogError("no se encuentra");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) return;

        var rotVector = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(rotVector);

        // Update movement based on timer instead of distance
        if (canUpdateMovement)
        {
            UpdateMovementWithTimer();
        }

        InAttackRange();
    }
    
    private void UpdateMovementWithTimer()
    {
        // Calculate new movement position
        nextPosition = CalculateNextMovement();
        
        // Stop current movement and start new one
        transform.DOKill();
        
        // Calculate movement duration based on distance and velocity
        float movementDuration = Vector3.Distance(transform.position, nextPosition) / velocity;
        
        // Move to new position
        transform.DOMove(nextPosition, movementDuration).SetEase(Ease.Linear);
        
        // Start cooldown timer for next movement update
        canUpdateMovement = false;
        
        // Use DOTweenTimer utility to handle the movement update timing
        DOTweenTimer.CreateTimer(movementUpdateInterval, () =>
        {
            canUpdateMovement = true;
        });
    }
    
    public Vector3 CalculateNextMovement()
    {
        if (player == null || navMeshAgent == null)
        {
            return transform.position;
        }
        
        
        Vector3 targetPosition = player.transform.position;
        
        // Calculate the path to the player using NavMesh
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path))
        {
            // If a valid path exists, set the destination for the NavMeshAgent
            navMeshAgent.SetDestination(targetPosition);
            
            // Return the next position to move to (first corner of the path)
            if (path.corners.Length > 1)
            {
                return new Vector3(path.corners[1].x, transform.position.y, path.corners[1].z); // Next waypoint
            }
        }
        
        // If no valid path, return current position
        return transform.position;
    }
    
    internal void InAttackRange()
    {
        if(Vector3.Distance(player.transform.position, this.transform.position) <= attackRange && canAttack)
        {
            AttackPlayerWithCooldown();
            animator.SetTrigger("Attack");
        }
        if(Vector3.Distance(player.transform.position, this.transform.position) > visualRange)
        {
            player = null;
        }
    }
    
    public override void Die()
    {
        animator.SetTrigger("Muerte");
        Debug.Log("EnemyCQC died");
        //TODO: Add die sound
        //TODO: send die event
        //TODO: destroy enemy
        base.Die();
        
    }
    private void AttackPlayerWithCooldown()
    {
        if (!canAttack) return;
        
        // Perform the attack
        AttackPlayer();
        if (attackVFX != null)
        {
            animator.SetTrigger("Attack");
            attackVFX.SetActive(true);
            AnimateAttackVFX();
           
        }
        // Start cooldown
        canAttack = false;
        
        // Use DOTweenTimer utility to handle the cooldown timing
        DOTweenTimer.CreateTimer(attackCooldown, () =>
        {
            canAttack = true;
        });
    }

    /// <summary>
    /// Anima el VFX de ataque con rotaci�n de 360 grados y escala descendente
    /// La duraci�n coincide con el cooldown del ataque
    /// </summary>
    private void AnimateAttackVFX(float startDelay = 10f)
    {
        
        if (attackVFX == null) return;

        // Si hay una animación anterior, la matamos para reiniciar limpio
        _attackSeq?.Kill();

        // Asegurar estado inicial
        attackVFX.SetActive(true);
        attackVFX.transform.localScale = Vector3.one;
        attackVFX.transform.localRotation = Quaternion.identity;

        // Duración en segundos (cuidamos división por cero)
        float animationDuration = 1f / Mathf.Max(attackCooldown, 0.0001f);

        // Crear secuencia con delay al inicio
        _attackSeq = DOTween.Sequence()
            .SetDelay(startDelay)            // ⬅️ delay antes de todo
            .SetAutoKill(true)               // se destruye al terminar
            .SetLink(attackVFX);             // si el GO se destruye, mata el tween

        // Rotación Y 360° (local) en la misma duración
        _attackSeq.Join(
            attackVFX.transform.DOLocalRotate(
                new Vector3(0f, 360f, 0f),
                animationDuration,
                RotateMode.FastBeyond360
            )
        );

        // Escala a 0 con ease
        _attackSeq.Join(
            attackVFX.transform.DOScale(Vector3.zero, animationDuration)
                .SetEase(Ease.InCubic)
        );

        // Al completar, desactivar el VFX
        _attackSeq.OnComplete(() => attackVFX.SetActive(false));

        // Opcional: reproducir explícitamente
        _attackSeq.Play();
    }


private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nextPosition, 0.1f);
    }
    
    
}
