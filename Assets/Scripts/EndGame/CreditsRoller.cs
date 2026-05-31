using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsRoller : MonoBehaviour
{
    [Tooltip("Drag the TextMeshPro object here")]
    [SerializeField] private RectTransform creditsText;

    [Tooltip("How fast the text moves up")]
    [SerializeField] private float scrollSpeed = 50f;

    [Tooltip("Scene to load when credits are done or skipped")]
    [SerializeField]private string mainMenu = "MainMenu";

    private void Update()
    {
        if (creditsText != null) 
        {
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) // skip credits with either escape or space
        {
            SceneManager.LoadScene(mainMenu);
        }
    }
}
