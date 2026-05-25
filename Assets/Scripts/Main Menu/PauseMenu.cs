using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Pause Menu in game play

    [Header("References")]
    [SerializeField] GameObject pauseMenu;
    [Tooltip("The Are you sure you want to exit? panel")]
    [SerializeField] private GameObject confirmationPanel;
    [Tooltip("The field manual container")]
    [SerializeField] private GameObject fieldManualPanel;

    [Header("Settings")]
    [Tooltip("How long it takes to fade to black when quitting to the main menu")]
    [SerializeField] private float quitFadeDuration = 1.5f;

    private bool isPaused = false;

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) /*&& !isPaused*/) // Toggle pause state when pressing p or ESC
        {
            //if(FadeInOut.Instance != null && FadeInOut.Instance.IsScreenObscured())
            //{
            //    return;
            //}
            // Updated Version //Bella - now the pausemenu wont cloe by pressing same button twice
            // Do not allow the pause the pause menu to be opened when the screen is black and/or fading

            //Pause();

            
            if (!isPaused && FadeInOut.Instance != null && FadeInOut.Instance.IsScreenObscured())
                return; // Do not allow the pause the pause menu to be opened when the screen is black and/or fading

            if (isPaused) Resume();
            else Pause();
            
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);

        // Force close sub menus when pausing
        if (confirmationPanel != null) confirmationPanel.SetActive(false);
        if (fieldManualPanel != null) fieldManualPanel.SetActive(false);

        // just a fix for a small visual bug for the red check marks in the pause menu
        // Forces unity to forget whatever button you were hovering over last time the pause menu was open
        EventSystem.current.SetSelectedGameObject(null); 

        Cursor.lockState = CursorLockMode.None; //Make mouse visible
        Cursor.visible = true;

        //Make game time freeze during menu
        Time.timeScale = 0; 
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);

        if (confirmationPanel != null) confirmationPanel.SetActive(false);
        if (fieldManualPanel != null) fieldManualPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;  //Make mouse invisible
        Cursor.visible = false;

        //Make game time run after pressing resume
        Time.timeScale = 1;
    }

    /// <summary>
    /// Called by the Restart Case button. Shows the confirmation pop up
    /// </summary>
    public void ShowQuitConfirmation() 
    {
        if (confirmationPanel != null) confirmationPanel.SetActive(true);
    }

    /// <summary>
    /// Hides the confirmation pop up if the player presses no
    /// </summary>
    public void HideQuitConfirmation() 
    {
        if (confirmationPanel != null) confirmationPanel.SetActive(false);
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
