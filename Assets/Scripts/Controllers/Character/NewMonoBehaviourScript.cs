using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Asegúrate de que esta variable esté asignada en el Inspector
    public Animator animator;

    // El nombre del parámetro en tu Animator (¡debe ser EXACTO!)
    private const string SPEED_PARAMETER = "Blend";

    // Valores que coinciden con los Thresholds de tu Blend Tree
    private const float IDLE_VALUE = 0.0f;
    private const float WALK_VALUE = 0.25f; // Asumo 1.0 para Caminar
    private const float RUN_VALUE = 0.6f;  // Asumo 2.0 para Correr

    // Para una transición suave entre animaciones
    public float blendTransitionSpeed = 5.0f;

    void Update()
    {
        // 1. Detección de Entrada
        bool isMoving = Input.GetKey(KeyCode.W);
        // Usa LeftShift o RightShift para el correr
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed;

        // 2. Determinar la velocidad objetivo
        if (isMoving)
        {
            if (isRunning)
            {
                // W + Shift: Correr
                targetSpeed = RUN_VALUE;
            }
            else
            {
                // Solo W: Caminar
                targetSpeed = WALK_VALUE;
            }
        }
        else
        {
            // Sin teclas de movimiento: Inactivo
            targetSpeed = IDLE_VALUE;
        }

        // 3. Suavizar la transición (Interpolación Lineal - Lerp)
        float currentSpeed = animator.GetFloat(SPEED_PARAMETER);
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * blendTransitionSpeed);

        // 4. Aplicar al Animator
        animator.SetFloat(SPEED_PARAMETER, newSpeed);
    }
}