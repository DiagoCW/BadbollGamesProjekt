using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
     
    //Author : Isabella

    /// <summary>
    /// While pressing M key you will be returned to MainMenu - OLD CODE


    void Start()
    {
        Time.timeScale = 1f;

        //Make the mouse invisible on the MainScene
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    //If you press the M key you get to the MainMenu - But if you press Play again it will restart the game FOR NOW 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
