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

    private float elapsedTime = 0f;
    private float delay = 3f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= delay)
        {
            if (creditsText != null)
            {
                creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }
            if (creditsText != null)
            {
                creditsImage.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }

            if (creditsText != null)
            {
                creditsAd2.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }

            // secondary text for additional text in other fonts
            if (creditsAd != null)
            {
                creditsAd.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) // skip credits with either escape or space
        {
            if (!string.IsNullOrEmpty(mainMenu))
            {
                SceneManager.LoadScene(mainMenu);
            }
        }
    }
}
