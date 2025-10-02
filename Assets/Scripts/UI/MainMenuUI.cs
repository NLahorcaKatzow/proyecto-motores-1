using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.Instance.LoadNextLevel();
    }
    
    public void Credits()
    {
        SceneManager.Instance.LoadLevel(3);
    }
    
    public void GoToMainMenu()
    {
        SceneManager.Instance.LoadMainMenu();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
