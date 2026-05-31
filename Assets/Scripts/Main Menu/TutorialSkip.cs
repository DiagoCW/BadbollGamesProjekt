using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSkip : MonoBehaviour
{
    //Author : Isabella
    //Tutorial skip button in the tutorial scene

    [Header("Settings")]
    [Tooltip("How long the screen takes to fade to black before loading main scene")]
    [SerializeField] public float fadeDuration { get; private set; } = 1.5f;

    private bool isSkipping = false; // Failsafe to prevent the player from spamming T button while fading

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && !isSkipping)
        {
            SkipTutorial();
        }
    }

    //While pressing the T button you move to the MainScene
    public void SkipTutorial()
    {
        isSkipping = true;
        Time.timeScale = 1;
        
        if (FadeInOut.Instance != null) 
        {
            FadeInOut.Instance.FadeToScene("MainScene", fadeDuration);
        }
        else 
        {
            Debug.LogWarning("FadeInOut missing. Loading main scene without fade");
            SceneManager.LoadScene("MainScene");
        }
    }
}
