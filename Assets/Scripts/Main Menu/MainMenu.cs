using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //Author : Isabella


    [Header("References")]
    [Tooltip("Drag MainMenuMusic object here")]
    public MainMenuMusic musicManager;

    void Start()
    {
        //Make the mouse visible on the MainMenu scene
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Change scene by pressing the play button
    public void PlayGame()
    {
        if (musicManager != null)
        {
            musicManager.StartGameWithFade();
        }
        else
        {
            Debug.LogWarning("Music Manager not assigned. Loading scene without music fade");
            SceneManager.LoadSceneAsync("TutorialScene");
        }
    }

    //Exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
