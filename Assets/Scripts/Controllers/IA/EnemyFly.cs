
using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Utilities;

public class EnemyFly : EnemyBase
{
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true;
    private bool canUpdateMovement = true;
    private Animator animator ;
    // Movement and behavior variables
    public Vector3 nextPosition;
    public float visualRange = 10f;
    public float fleeDistance = 8f; // Distance to maintain from player
    public float movementUpdateInterval = 0.5f; // Time between movement updates

    // Projectile system
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = velocity;
        }
        animator = GetComponentInChildren<Animator>();
        if(animator != null )
        {
            Debug.LogError("No se encuentra");
        }
    }

    void Update()
    {
        if (player == null) return;

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
        nextPosition = CalculateFleeMovement();
        
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

    public Vector3 CalculateFleeMovement()
    {
        if (player == null || navMeshAgent == null)
        {
            return transform.position;
        }

        // Calculate direction away from player
        Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

        // Calculate desired flee position
        Vector3 fleeTarget = transform.position + directionAwayFromPlayer * fleeDistance;

        // Check if we're too close to the player - if so, flee
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < fleeDistance)
        {
            // Calculate the path away from the player using NavMesh
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, fleeTarget, NavMesh.AllAreas, path))
            {
                navMeshAgent.SetDestination(fleeTarget);

                // Return the next position to move to
                if (path.corners.Length > 1)
                {
                    return new Vector3(path.corners[1].x, transform.position.y, path.corners[1].z);
                }
            }
        }
        else
        {
            fleeTarget = transform.position - directionAwayFromPlayer * fleeDistance;
            // Calculate the path away from the player using NavMesh
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, fleeTarget, NavMesh.AllAreas, path))
            {
                navMeshAgent.SetDestination(fleeTarget);

                // Return the next position to move to
                if (path.corners.Length > 1)
                {
                    return new Vector3(path.corners[1].x, transform.position.y, path.corners[1].z);
                }
            }
        }
        // If at safe distance or no valid path, stay in position
        return transform.position;
    }

    internal void InAttackRange()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) <= attackRange && canAttack)
        {
            
            ShootProjectileWithCooldown();
        }
        if (Vector3.Distance(player.transform.position, this.transform.position) > visualRange)
        {
            player = null;
        }
    }

    public override void Die()
    {
        //TODO: Add die animation
        Debug.Log("EnemyFly died");
        //TODO: Add die sound
        //TODO: send die event
        //TODO: destroy enemy
        base.Die();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack)
        {
            ShootProjectileWithCooldown();
        }
    }

    private void ShootProjectileWithCooldown()
    {
        if (!canAttack) return;
        
        // Shoot projectile
        ShootProjectile();

        // Start cooldown
        canAttack = false;

        // Use DOTweenTimer utility to handle the cooldown timing
        DOTweenTimer.CreateTimer(attackCooldown, () =>
        {
            canAttack = true;
        });
    }

    private void ShootProjectile()
    {
        if (projectilePrefab == null || firePoint == null || player == null)
        {
            Debug.LogWarning("Missing projectile setup - check projectilePrefab, firePoint, or player reference");
            return;
        }

        // Create projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calculate direction to player
        Vector3 direction = (player.transform.position - firePoint.position).normalized;

        // Apply velocity to projectile if it has a Rigidbody
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.linearVelocity = direction * projectileSpeed;
        }

        Debug.Log("EnemyFly shot projectile at player");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(nextPosition, 0.1f);

        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw flee distance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);
    }





}
