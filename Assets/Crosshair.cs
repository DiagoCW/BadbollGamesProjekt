using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(PlayerController.Instance.IsInventoryOpen || NewDialogueManager.Instance.dialogueIsPlaying);
        
    }
}
