using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] TextAsset inkJson;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
