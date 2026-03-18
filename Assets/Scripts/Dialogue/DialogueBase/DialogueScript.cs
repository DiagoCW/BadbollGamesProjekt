using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI[] choiceTexts;
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) Choose(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Choose(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Choose(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Choose(3);
    }

    void ShowChoices()
    {
        if (dialogueData.choices == null || dialogueData.choices.Length == 0)
        {
            EndDialogue();
            return;
        }

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            if (i < dialogueData.choices.Length)
            {
                choiceTexts[i].text = (i + 1) + ". " + dialogueData.choices[i].choiceText;
                choiceTexts[i].gameObject.SetActive(true);
            }
            else
            {
                choiceTexts[i].gameObject.SetActive(false);
            }
        }
    }

    void Choose(int choiceIndex)
    {
        if (isTyping) return;

        if (dialogueData.choices == null) return;
        if (choiceIndex >= dialogueData.choices.Length) return;

        DialogueData next = dialogueData.choices[choiceIndex].nextDialogue;

        if (next != null)
        {
            StartDialogue(next);
        }
        else
        {
            EndDialogue();
        }
    }

    public void StartDialogue(DialogueData newDialogue)
    {
        //if (newDialogue == null || newDialogue.lines == null || newDialogue.lines.Length == 0)
        //{
        //    Debug.LogWarning("Dialogue data is empty!");
        //    return;
        //}

        dialogueData = newDialogue;
        index = 0;
        textComponent.text = "";

        foreach (var choice in choiceTexts)
        {
            choice.gameObject.SetActive(false);
        }

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
            ShowChoices();
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
        DialogueManager.Instance.EndDialogue();
    }
}
