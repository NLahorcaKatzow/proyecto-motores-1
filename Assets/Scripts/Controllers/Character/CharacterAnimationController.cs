using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator;

    [Header("Animation Parameters")]
    // Los nombres de los parámetros en tu Animator (¡deben ser EXACTOS!)
    private const string SPEED_PARAMETER = "Blend";

    [Header("Movement Values")]
    // Valores que coinciden con los Thresholds de tu Blend Tree
    private const float IDLE_VALUE = 0.0f;
    private const float WALK_VALUE = 0.25f; // Para Caminar
    private const float RUN_VALUE = 0.6f;  // Para Correr

    [Header("Transition Settings")]
    // Para una transición suave entre animaciones
    public float blendTransitionSpeed = 5.0f;

    void Update()
    {
        // 1. Detección de Entrada - Todas las direcciones
        float horizontal = Input.GetAxis("Horizontal"); // A/D o Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S o Up/Down
        
        // Calcular la magnitud del movimiento (0 = parado, 1 = movimiento máximo)
        Vector2 movement = new Vector2(horizontal, vertical);
        float movementMagnitude = movement.magnitude;
        
        // Limitar la magnitud a 1 para movimiento diagonal
        movementMagnitude = Mathf.Clamp01(movementMagnitude);
        
        // Detección de correr
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed;

        // 2. Determinar la velocidad objetivo basada en la magnitud del movimiento
        if (movementMagnitude > 0.1f) // Umbral pequeño para evitar jitter
        {
            if (isRunning)
            {
                // Movimiento + Shift: Correr
                targetSpeed = RUN_VALUE;
            }
            else
            {
                // Solo movimiento: Caminar
                targetSpeed = WALK_VALUE;
            }
        }
        else
        {
            // Sin movimiento: Inactivo
            targetSpeed = IDLE_VALUE;
        }

        // 3. Suavizar la transición (Interpolación Lineal - Lerp)
        float currentSpeed = animator.GetFloat(SPEED_PARAMETER);
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * blendTransitionSpeed);

        // 4. Aplicar al Animator
        animator.SetFloat(SPEED_PARAMETER, newSpeed);
    }
}
