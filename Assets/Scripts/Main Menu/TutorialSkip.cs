using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSkip : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SkipTutorial();
        }
    }

    //While pressing the T button you move to the MainScene
    public void SkipTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
}
