using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyCQC : EnemyBase
{
    private NavMeshAgent navMeshAgent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float deltaMovimiento = 0.1f;
    public Vector3 nextPosition;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) return;

        if(Vector3.Distance(transform.position, nextPosition) > deltaMovimiento)
        {
            transform.DOKill();
            transform.DOMove(nextPosition, deltaMovimiento).SetEase(Ease.Linear);
            nextPosition = MoveToPlayer();
        }
        
        
    }
    
    public Vector3 MoveToPlayer()
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
                return path.corners[1]; // Next waypoint
            }
        }
        
        // If no valid path, return current position
        return transform.position;
    }
    
    
    public override void Die()
    {
        //TODO: Add die animation
        Debug.Log("EnemyCQC died");
        //TODO: Add die sound
        //TODO: send die event
        //TODO: destroy enemy
    }
    
}
