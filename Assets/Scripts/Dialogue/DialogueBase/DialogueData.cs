using UnityEngine;

// Dialogue choice

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueData nextDialogue;
}

// Dialogue data

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] lines;

    public float textSpeed = 0.05f;

    public DialogueChoice[] choices;
}