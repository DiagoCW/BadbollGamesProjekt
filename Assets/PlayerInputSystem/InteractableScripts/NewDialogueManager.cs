using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SearchService;
public class NewDialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI displayNameText;

    [Header("Load Globals JSON")]
    [SerializeField] TextAsset loadGlobalsJSON;

    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    [SerializeField] float typingSpeed = 0.02f;

    Story currentStory;

    Coroutine displayLineCoroutine;
    public bool dialogueIsPlaying { get; private set; }
    bool isTyping;

    DialogueVariables dialogueVariables;
    public static NewDialogueManager Instance { get; private set; }

    const string SPEAKER_TAG = "speaker";
    const string PORTRAIT_TAG = "portrait";
    const string LAYOUT_TAG = "layout";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of DialogueManager detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        else Instance = this;

        dialogueVariables = new(loadGlobalsJSON);
    }


    private void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        isTyping = false;

        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying ||
            currentStory.currentChoices.Count > 0) return;

        //if (!dialogueIsPlaying) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isTyping)
            {
                ContinueStory();
            }
            else
            {
                if (displayLineCoroutine != null)
                {
                    StopCoroutine(displayLineCoroutine);
                }
                dialogueText.text = currentStory.currentText;
                isTyping = false;
            }
        }
    }

    IEnumerator TypeText(string line)
    {
        dialogueText.text = string.Empty;
        isTyping = true;
        HideChoices();
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        DisplayChoices();
    }

    public void EnterDialogue(TextAsset inkJson)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;
        currentStory = new(inkJson.text);
        dialogueVariables.StartListening(currentStory);

        currentStory.BindExternalFunction("setAnimation", (string anim) =>
        {
            Debug.Log(anim);
        });


        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogue()
    {
        dialogueVariables.StopListening(currentStory);
        currentStory.UnbindExternalFunction("setAnimation");

        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = true;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(TypeText(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogue();
        }
    }

    void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogWarning($"Tag {tag} is not properly formatted. Expected format: 'key:value'");
                continue;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    // Hantera talare, t.ex. ändra namn eller färg på texten
                    Debug.Log($"Speaker: {tagValue}");
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // Hantera porträtt, t.ex. visa en bild av talaren
                    Debug.Log($"Portrait: {tagValue}");
                    break;
                case LAYOUT_TAG:
                    // Hantera layout, t.ex. ändra position eller stil på dialogrutan
                    Debug.Log($"Layout: {tagValue}");
                    break;
                default:
                    Debug.LogWarning($"Unknown tag key: {tagKey}");
                    break;
            }
        }
    }

    void HideChoices()
    {
        foreach (GameObject choice in choices)
            choice.SetActive(false);
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError($"More choices in the story than UI can support. " +
                $"Number of choices: {currentChoices.Count}");
        }

        // Beror på om vi vill se dialogtexten när spelaren har val inom dialogen,
        // Eller om dialogtexten ska försvinna när valen visas.
        //if (currentChoices.Count > 0)
        //    dialogueText.text = string.Empty;

        for (int i = 0; i < choices.Length; i++)
        {
            if (i < currentChoices.Count)
            {
                choices[i].SetActive(true);
                choicesText[i].text = currentChoices[i].text;
            }
            else
            {
                choices[i].SetActive(false);
            }
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}