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
            //HealthController.instance.TakeDamage(3) // daño al jugador
        }
    }
}
