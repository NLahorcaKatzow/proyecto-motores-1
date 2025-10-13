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
    
    // Movement and behavior variables
    public Vector3 nextPosition;
    public float visualRange = 10f;
    public float movementUpdateInterval = 0.5f; // Time between movement updates
    public GameObject attackVFX;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = velocity;
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
        }
        if(Vector3.Distance(player.transform.position, this.transform.position) > visualRange)
        {
            player = null;
        }
    }
    
    public override void Die()
    {
        //TODO: Add die animation
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
    private void AnimateAttackVFX()
    {
        if (attackVFX == null) return;
        
        // Calcular duraci�n basada en el attack rate (cooldown)
        float animationDuration = 1/attackCooldown;
        
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nextPosition, 0.1f);
    }
    
    
}
