using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    /// <summary>
    /// Pause Menu in game play

    [Header("References")]
    [SerializeField] GameObject pauseMenu;

    [Header("Settings")]
    [Tooltip("How long it takes to fade to black when quitting to the main menu")]
    [SerializeField] private float quitFadeDuration = 1.5f;

    private bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
       
        //Make mouse visible
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Make game time freeze during menu
        Time.timeScale = 0; 
    }

    public void Resume()
    {
        isPaused = false;
        //Make mouse invisible
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Make game time run after pressing resume
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        //Make game time run after pressing restart
        Time.timeScale = 1;

        //Go back to Main Menu
        if (FadeInOut.Instance != null) 
        {
            FadeInOut.Instance.FadeToScene("MainMenu", quitFadeDuration);
        }
        else 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
