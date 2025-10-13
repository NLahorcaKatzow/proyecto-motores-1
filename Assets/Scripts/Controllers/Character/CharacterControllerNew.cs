using UnityEngine;

public class CharacterControllerNew : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    
    [Header("Camera")]
    public Camera playerCamera; // Reference to the camera for relative movement

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public LayerMask floorMask;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                playerCamera = FindFirstObjectByType<Camera>();
            }
        }
    }

    void Update()
    {
        // Verifica si est� tocando el suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantiene al jugador en el suelo
        }

        // Inputs del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Determina si corre o camina
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Calcula direcci�n de movimiento relativa a la c�mara
        Vector3 move = GetCameraRelativeMovement(horizontal, vertical);
        controller.Move(move * speed * Time.deltaTime);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    /// <summary>
    /// Calculates movement direction relative to camera orientation
    /// </summary>
    /// <param name="horizontal">Horizontal input (-1 to 1)</param>
    /// <param name="vertical">Vertical input (-1 to 1)</param>
    /// <returns>World space movement direction</returns>
    private Vector3 GetCameraRelativeMovement(float horizontal, float vertical)
    {
        if (playerCamera == null)
        {
            return transform.right * horizontal + transform.forward * vertical;
        }
        
        
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        cameraForward.Normalize();
        cameraRight.Normalize();
        
        Vector3 move = cameraRight * horizontal + cameraForward * vertical;
        
        return move;
    }
}
