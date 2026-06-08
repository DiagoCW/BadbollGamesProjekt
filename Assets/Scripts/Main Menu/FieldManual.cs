using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the tutorial menu. Allows the palyer to click through multiple pages of instructions.
/// </summary>
public class FieldManual : MonoBehaviour
{
    [Header("Instruction pages")]
    [Tooltip("Drag the UI game objects for each page here in order p. 1, 2, 3, etc.")]
    [SerializeField] private GameObject[] pages;

    private int currentPageIndex = 0;

    private void OnEnable()
    {
        ShowPage(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.D)) NextPage(); // Left click makes the page go forward
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.A)) PreviousPage(); // Right click makes page go backward
    }

    /// <summary>
    /// Jumps to the next page
    /// </summary>
    public void NextPage() 
    {
        if(currentPageIndex < pages.Length - 1) 
        {
            ShowPage(currentPageIndex + 1);
        }
        else 
        {
            gameObject.SetActive(false);   
        }
    }

    public void PreviousPage() 
    {
        if (currentPageIndex > 0) 
        {
            ShowPage(currentPageIndex - 1);
        }
    }

    private void ShowPage(int index) 
    {
        currentPageIndex = index;

        for (int i = 0; i < pages.Length; i++) 
        {
            pages[i].SetActive(i == currentPageIndex);
        }
    }
}
