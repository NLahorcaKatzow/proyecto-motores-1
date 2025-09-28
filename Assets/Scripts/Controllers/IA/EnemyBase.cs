using DG.Tweening;
using UnityEngine;
public class EnemyBase : MonoBehaviour
{
    public float health;
    public float velocity;
    public int attackDamage;
    public int attackRange;
    public float attackCooldown;
    public GameObject player;

    public void TakeDamage(float damage, Vector3 fromPosition)
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
        
        health -= damage;
        if (health <= 0)
        {
            Die();
            return;
        }
        Nockback(fromPosition);
    }

    private void Nockback(Vector3 fromPosition)
    {
        var directionNockback = (transform.position - fromPosition).normalized;
        directionNockback.y = 0;
        transform.localScale = Vector3.one;
        
        // Create unified animation sequence
        Sequence knockbackSequence = DOTween.Sequence();
        
        // Add both animations to run simultaneously
        knockbackSequence.Join(transform.DOMove(transform.position + directionNockback * 3f, 0.4f).SetEase(Ease.OutQuad));
        knockbackSequence.Join(transform.DOPunchScale(Vector3.one * 0.5f, 0.4f).SetEase(Ease.OutQuad));
        
        // Ensure scale is set to Vector3.one when sequence completes
        knockbackSequence.OnComplete(() => {
            transform.localScale = Vector3.one;
        });
    }

    public virtual void Die()
    {
        //TODO: Add die animation

        Debug.Log("Enemy died");
        //TODO: Add die sound
        //TODO: send die event
        //TODO: destroy enemy
        transform.DOKill();
        //transform.DOScale(0, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => Destroy(gameObject));
        Destroy(gameObject);
    }

    public void AttackPlayer()
    {
        Debug.Log("Enemy attacked player");
        if(HealthController.Instance == null) {
            Debug.LogError("HealthController not found");
            return;
        }
        HealthController.Instance.TakeDamage(attackDamage);
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