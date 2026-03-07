using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    private DialogueData dialogueData;
    private int index;
    private bool isTyping;

    void Start()
    {
        textComponent.text = "";
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = dialogueData.lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(DialogueData newDialogue)
    {
        if (newDialogue == null || newDialogue.lines == null || newDialogue.lines.Length == 0)
        {
            Debug.LogWarning("Dialogue data is empty!");
            return;
        }

        dialogueData = newDialogue;
        index = 0;
        textComponent.text = "";
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";

        foreach (char c in dialogueData.lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(dialogueData.textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < dialogueData.lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
        DialogueManager.Instance.EndDialogue();
    }
}
