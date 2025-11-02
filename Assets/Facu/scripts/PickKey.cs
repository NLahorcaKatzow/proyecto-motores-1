using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PickKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // destruye el objeto que tiene este script
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}