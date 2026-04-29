using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    void Start()
    {
        //Make the mouse visible on the MainMenu scene
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Change scene by pressing the play button
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("TutorialScene");
    }

    //Exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
