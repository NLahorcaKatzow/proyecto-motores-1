using UnityEngine;
public class EnemyBase : MonoBehaviour
{
    public float health;
    public float speed;
    public float attackDamage;
    public int attackRange;
    public float attackCooldown;
    public GameObject player;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //TODO: Add die animation
        Debug.Log("Enemy died");
        //TODO: Add die sound
        //TODO: send die event
        //TODO: destroy enemy
    }

    public void AttackPlayer()
    {
        Debug.Log("Enemy attacked player");
        //TODO: Add attack animation
        //TODO: Add attack sound
        //TODO: send attack damage to player event
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void RemovePlayer()
    {
        this.player = null;
    }

}