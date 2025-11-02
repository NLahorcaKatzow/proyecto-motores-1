using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    [Header("Camera Settings")]
    public float rotationSpeed = 5f;

    public GameObject thirdPersonCam;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
    }

    [Header("Cursor Control")]
    [Tooltip("Si está activo, el cursor se bloqueará y ocultará al iniciar el juego.")]
    public bool lockCursorOnStart = true;
    private bool isCursorLocked = true;

    private void Start()
    {
        // Bloquea y oculta el cursor al inicio
        if (lockCursorOnStart)
            SetCursorLock(true);
    }

    private void Update()
    {
        // Permite alternar el estado del cursor con Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            SetCursorLock(isCursorLocked);
        }

        // Cambiar estilo de cámara (si tuvieras varios)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchCameraStyle(CameraStyle.Basic);

        // Rotar orientación hacia el jugador
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotar al jugador según input
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        // Apagar todas las cámaras
        thirdPersonCam.SetActive(false);

        // Activar la seleccionada
        if (newStyle == CameraStyle.Basic)
            thirdPersonCam.SetActive(true);

        currentStyle = newStyle;
    }

    /// <summary>
    /// Bloquea o libera el cursor.
    /// </summary>
    /// <param name="locked">true = bloqueado/oculto | false = libre/visible</param>
    public void SetCursorLock(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}