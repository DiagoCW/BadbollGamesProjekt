using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Author : Isabella, Stefan
    /// <summary>
    /// Pause Menu in game play

    [Header("References")]
    [SerializeField] GameObject pauseMenu;
    [Tooltip("The Are you sure you want to exit? panel")]
    [SerializeField] private GameObject confirmationPanel;
    [Tooltip("The field manual container")]
    [SerializeField] private GameObject fieldManualPanel;
    [Tooltip("Drag player here so it can't move or interact during scene transition")]
    [SerializeField] private PlayerController playerController;
    //[SerializeField] private PlayerInteract playerInteract;

    [Header("Settings")]
    [Tooltip("How long it takes to fade to black when quitting to the main menu")]
    [SerializeField] private float quitFadeDuration = 1.5f;

    private bool isPaused = false;
    private bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning) return;
        
        if((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))) // Toggle pause state when pressing p or ESC
        {
            
            if (!isPaused && FadeInOut.Instance != null && FadeInOut.Instance.IsScreenObscured())
                return; // Do not allow the pause the pause menu to be opened when the screen is black and/or fading

            if (isPaused)
            {
                Resume();
                Cursor.visible = false;
            }
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
        isTransitioning = true;
        isPaused = false;
        
        pauseMenu.SetActive(false);
        if (confirmationPanel != null) confirmationPanel.SetActive(false);

        // turn off player movement
        if (playerController != null) 
        {
            playerController.enabled = false;
        }
        //if (playerInteract != null) // turn off player actions
        //{
        //    playerInteract.enabled = false;
        //}

        // Hide mouse cursor so it doesn't float on the black screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Make game time run after pressing restart so coroutine can run
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

        ////Make game time run after pressing restart
        //Time.timeScale = 1;

        ////Go back to Main Menu
        //if (FadeInOut.Instance != null) 
        //{
        //    FadeInOut.Instance.FadeToScene("MainMenu", quitFadeDuration);
        //}
        //else 
        //{
        //    SceneManager.LoadScene("MainMenu");
        //}
    }
}
