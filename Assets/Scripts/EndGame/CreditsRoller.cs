using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsRoller : MonoBehaviour
{
    [Tooltip("Drag the TextMeshPro object here")]
    [SerializeField] private RectTransform creditsText;
    [SerializeField] private RectTransform creditsImage;

    // secondary text for additional text in other fonts
    [Tooltip("Aditional text")]
    [SerializeField] private RectTransform creditsAd;
    [SerializeField] private RectTransform creditsAd2;

    [Tooltip("How fast the text moves up")]
    [SerializeField] private float scrollSpeed = 50f;

    [Tooltip("Scene to load when credits are done or skipped")]
    [SerializeField]private string mainMenu = "MainMenu";

    [Tooltip("Time in seconds before automatically returning to main menu")]
    [SerializeField] private float autoReturnTime = 90f; // 1 minute 30 seconds

    private float elapsedTime = 0f;
    private float delay = 3f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Scroll credits after delay
        if (elapsedTime >= delay)
        {
            if (creditsText != null)
            {
                creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }
            if (creditsImage != null)
            {
                creditsImage.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }

            if (creditsAd2 != null)
            {
                creditsAd2.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }

            // secondary text for additional text in other fonts
            if (creditsAd != null)
            {
                creditsAd.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }
        }

        // Automatically return to main menu after specified time
        if (elapsedTime >= autoReturnTime)
        {
            ReturnToMainMenu();
        }
        
        // Skip credits with either escape or space
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToMainMenu();
        }
    }

    private void ReturnToMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenu))
        {
            SceneManager.LoadScene(mainMenu);
        }
    }
}
