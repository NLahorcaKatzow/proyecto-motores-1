using System.Collections.Generic;
using UnityEngine;
public class EnemyCollider : MonoBehaviour
{
    public EnemyBase enemy;
    HashSet<Collider> colliders = new HashSet<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (colliders.Contains(other)) return;
        if (other.gameObject.CompareTag("Player"))
        {
            colliders.Add(other);
            enemy.SetPlayer(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliders.Remove(other);
            enemy.RemovePlayer();
        }
    }
}