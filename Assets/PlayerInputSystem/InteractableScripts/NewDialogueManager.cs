using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SearchService;
using System;
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

    [Header("Speaker Panels")]
    [SerializeField] GameObject npcPanel;
    [SerializeField] GameObject playerPanel;
    [SerializeField] TextMeshProUGUI npcText;
    [SerializeField] TextMeshProUGUI playerText;

    [Header("NPC Animation Controller")]
    //[SerializeField] RuntimeAnimatorController npcAnimController;
    /*[SerializeField]*/ Animator npcAnimator;
    

    [SerializeField] float typingSpeed = 0.03f;

    public Story currentStory { get; private set; } // dialogens textfil 
    public bool dialogueIsPlaying { get; private set; }
    bool isTyping;

    private Coroutine displayLineCoroutine;

    public DialogueVariables dialogueVariables { get; private set; }
    public static NewDialogueManager Instance { get; set; }

    const string SPEAKER_TAG = "speaker";
    const string PORTRAIT_TAG = "portrait";
    const string LAYOUT_TAG = "layout";
    const string ANIM_TAG = "anim";
    string currentValue = "Idle";
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
        if (!dialogueIsPlaying /*||
            currentStory.currentChoices.Count > 0*/) return;

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitDialogue();

        if (Input.GetKeyDown(KeyCode.E) && currentStory.currentChoices.Count == 0)
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
        
        if (currentStory.currentChoices.Count > 0)
        {
            //isTyping = false;
            dialogueText.text = string.Empty;
            DisplayChoices();
            //yield return new WaitForSeconds(0.01f);
        }
        else
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
        }
        
        
    }

    public void EnterDialogue(TextAsset inkJson, GameObject npc)
    {
        //GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;
        
        PlayerController.Instance.enabled = false;
        npcAnimator = npc.GetComponent<Animator>();
        currentStory = new(inkJson.text);
        dialogueVariables.StartListening(currentStory);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogue()
    {
        dialogueVariables.StopListening(currentStory);
        //GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = true;
        //GameObject.FindWithTag("Player").GetComponent<GameInput>().enabled = true;
        PlayerController.Instance.enabled = true;
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
                    if (tagValue.Equals("Player"))
                    {
                        playerText.text = tagValue;
                        npcPanel.SetActive(false);
                        playerPanel.SetActive(true);
                    }
                        
                    else
                    {
                        npcText.text = tagValue;
                        npcPanel.SetActive(true);
                        playerPanel.SetActive(false);
                    }
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
                case ANIM_TAG:
                    Debug.Log($"Animation: {tagValue}, currentTag: {currentValue}");

                    //DialogueTrigger.npcAnimator.SetTrigger(tagValue);
                    npcAnimator.SetTrigger(tagValue);

                    //if (!currentValue.Equals(tagValue))
                    //{
                    //    //DialogueTrigger.npcAnimator.ResetTrigger(currentValue);
                    //    npcAnimator.ResetTrigger(currentValue);
                    //    currentValue = tagValue;
                    //    //continue;
                    //}

                    

                    
                    
                    //string currentValue = tagValue;
                    

                    //if (npcAnimator.GetBool(tagValue))
                    //    npcAnimator.SetBool(tagValue, false);
                    //else npcAnimator.SetBool(tagValue, true);

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
        StartCoroutine(ChoiceDelay());
        //ContinueStory();
    }

    private IEnumerator ChoiceDelay()
    {
        yield return new WaitForSeconds(5f);
    }
}