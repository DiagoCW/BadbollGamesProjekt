using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   

    /// <summary>
    /// Pause Menu in game play


    [SerializeField] GameObject pauseMenu;
    //bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        //Make mouse visible
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Make game time freeze during menu
        Time.timeScale = 0; 

        //isPaused = true;
    }

    public void Resume()
    {
        //Make mouse invisible
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Make game time run after pressing resume
        Time.timeScale = 1;

        //isPaused = false; 
    }

    public void RestartGame()
    {
        //Make game time run after pressing restart
        Time.timeScale = 1;

        //Go back to Main Menu
        SceneManager.LoadScene("MainMenu");
    }
}
