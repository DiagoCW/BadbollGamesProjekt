using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    bool dVisionTutorial = false;
    bool tutorialComplete = false;
    [SerializeField] TextAsset inkJson;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NewDialogueManager.Instance == null)
            return;
        dVisionTutorial = (Ink.Runtime.BoolValue)
        NewDialogueManager.Instance.GetVariableState("dvisionTutorialTrigger");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && dVisionTutorial 
            && !NewDialogueManager.Instance.dialogueIsPlaying)
        {
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);
            Debug.Log("Triggered D-Vision Tutorial");
            tutorialComplete = true;
        }
        if (tutorialComplete)
            Destroy(gameObject);
    }
}
