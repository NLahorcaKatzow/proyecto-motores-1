using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    [SerializeField] bool pausedGame = false;

       public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pausedGame = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pausedGame = true;
    }

}
