using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI; // El panel del men� de pausa
    private bool isPaused = false;
    
    public CharacterControllerNew characterController;
    public CharacterCombatNew characterCombat;
    public CharacterRotationController characterRotationController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        characterController.enabled = false;
        characterCombat.enabled = false;
        characterRotationController.enabled = false;
        Time.timeScale = 0f; // congela el tiempo del juego
        isPaused = true;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        characterController.enabled = true;
        characterCombat.enabled = true;
        characterRotationController.enabled = true;
        Time.timeScale = 1f; // reanuda el tiempo
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
