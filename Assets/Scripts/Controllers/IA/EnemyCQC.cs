using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyCQC : EnemyBase
{
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float deltaMovimiento = 0.1f;
    public Vector3 nextPosition;
    public float visualRange = 10f;
    
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
        
        
        if(Vector3.Distance(player.transform.position, nextPosition) > deltaMovimiento)
        {
            transform.DOKill();
            transform.DOMove(nextPosition, Vector3.Distance(player.transform.position, this.transform.position) / velocity).SetEase(Ease.Linear);
            nextPosition = CalculateNextMovement();
        }
        
        InAttackRange();
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
    }
    private void AttackPlayerWithCooldown()
    {
        if (!canAttack) return;
        
        // Perform the attack
        AttackPlayer();
        
        // Start cooldown
        canAttack = false;
        
        // Use DOTween to handle the cooldown timing
        DOTween.To(() => 0f, x => { }, 1f, attackCooldown)
            .OnComplete(() => {
                canAttack = true;
            });
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nextPosition, 0.1f);
    }
    
    
}
