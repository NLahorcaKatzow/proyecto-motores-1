using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class KillWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            HealthController.Instance.TakeDamage(3); // da�o al jugador
        }
    }
}
