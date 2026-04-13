using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
   
   void Start()
    {
        //Make the mouse invisible on the MainScene
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    //If you press the M key you get to the MainMenu - But if you press Play again it will restart the game FOR NOW 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
