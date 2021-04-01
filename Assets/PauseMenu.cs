using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        //activer le menu pause / l'afficher
        pauseMenuUI.SetActive(true);
        //arreter le temps
        Time.timeScale = 0;
        //changer le status du jeu
        gameIsPaused = true;
    }

    public void Resume()
    {
        //activer le menu pause / l'afficher
        pauseMenuUI.SetActive(false);
        //arreter le temps
        Time.timeScale = 1;
        //changer le status du jeu
        gameIsPaused = false;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
