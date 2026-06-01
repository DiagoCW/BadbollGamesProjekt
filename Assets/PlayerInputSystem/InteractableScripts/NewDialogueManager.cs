using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SearchService;
using System;
using UnityEngine.UIElements;
using UnityEngine.UI;
//using Ink.Parsed;
/// <summary>
/// Singleton script for the DialogueManager. This handles all dialogue interactions from a parsed JSON file,
/// and gets / sets variables within INK; including checking bool triggers, if lists contains certain items, etc.
/// It also handles animations and navmeshagents, and binds methods that can be called from within INK
/// to set a certain animation for the currently talking NPC, or have them move between points.
/// Huge thanks to @ShapedByRainStudios on Youtube for much of the framework that this script was built on.
/// 
/// </summary>
public class NewDialogueManager : MonoBehaviour
{
    // Alla komponenter för dialogpanelen
    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] UnityEngine.UI.Image dialogueCheck;
    [SerializeField] UnityEngine.UI.Image itemPortrait;
   // [SerializeField]
    public ItemDatabaseObject itemDatabase;

    // This is a globals file that contains variables that all dialogue files need access to, in order to 
    // persist variables between characters and objects. 
    [Header("Load Globals JSON")]
    [SerializeField] TextAsset loadGlobalsJSON;

    // The container that holds all assets for the dialogue box. 
    [Header("Choices UI")]
    [SerializeField] GameObject choicesContainer;
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    [Header("Speaker Panels")]
    [SerializeField] GameObject npcPanel;
    [SerializeField] GameObject playerPanel;
    [SerializeField] TextMeshProUGUI npcText;
    [SerializeField] TextMeshProUGUI playerText;

    // References for animating characters and controlling them as navmeshagents.
    TestAIScript aiAgent;
    Animator npcAnimator;

    public float typingSpeed = 0.03f;
    // Add with other private fields
    private float inputCooldownUntil;
    private const float INPUT_COOLDOWN = 0.1f;

    public Story currentStory { get; private set; } // Den dialog som spelas för tillfället: Nästan allting utgår från denna  
    public bool dialogueIsPlaying { get; private set; } // Referens som andra klasser kan hämta för att kontrollera att en dialog är aktiv eller inte 
    bool isTyping;

    private Coroutine displayLineCoroutine; // Should contain the Coroutine for typing out text one char at a time

    public DialogueVariables dialogueVariables { get; private set; } // Lagrar alla globala variabler som finns delade mellan olika Ink-filer 
    public InkExternalFunctions functions { get; private set; } // Denna kallas för att binda / unbinda externa funktioner inuti en Ink-fil 
    public static NewDialogueManager Instance { get; private set; } // Instans för att hämta relevanta metoder och properties från andra klasser 

    // Tags som sätts inuti Ink-filen och parsas för att ändra bl.a speaker-panel, animationer, eller porträtt för t.ex npc, spelare, items, osv 
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
        functions = new();
    }


    private void Start()
    {
        if (dialoguePanel != null) 
        {
            dialoguePanel.SetActive(false);
            dialogueIsPlaying = false;
            isTyping = false;

            choicesText = new TextMeshProUGUI[choices.Length];
            for (int i = 0; i < choices.Length; i++)
            {
                choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
            }
            Debug.Log("DialogueManager active");
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object value = null;
        dialogueVariables.variables.TryGetValue(variableName, out value);
        //if (value == null)
        //    Debug.LogWarning($"Variable {variableName} not found in DialogueVariables.");
        return value;
        
    }

    /// <summary>
    /// Check if a list in INK contains the item
    /// </summary>
    /// <param name="itemName">The item to look for</param>
    /// <param name="listName">The INK list to check in</param>
    /// <returns></returns>
    public bool InkListContainsItem(string itemName, string listName)
    {
        var list = currentStory.variablesState[listName] as InkList;
        if (list!= null)
        {
            return list.ContainsItemNamed(itemName);
            
        }
        return false;
    }

    /// <summary>
    /// For spawning Suspects to clueboard. Checks the Suspects INK list too see if it contains the suspect you want to unlock
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckSuspectsList(string itemName)
    {
        var list = currentStory.variablesState["Suspects"] as InkList;
        return list.ContainsItemNamed(itemName);
    }

    private void Update()
    {
        if (!dialogueIsPlaying /*||
            currentStory.currentChoices.Count > 0 || choicesContainer.activeSelf*/) return;

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (FadeInOut.Instance != null) FadeInOut.Instance.FadeScreenOnly(0f, 1f); // failsafe if the screen is still black when dialogue is skipped
            ExitDialogue();
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time >= inputCooldownUntil)
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
                dialogueCheck.enabled = true;
                if (currentStory.currentChoices.Count > 0)
                    DisplayChoices();
            }
        }
    }

    /// <summary>
    /// Types out the dialogue text one character at a time.
    /// </summary>
    /// <param name="line">The next line of dialogue to type out.</param>
    /// <returns></returns>
    IEnumerator TypeText(string line)
    {
        HideChoices();
        dialogueText.text = string.Empty;
        isTyping = true;
        dialogueCheck.enabled = false;

        bool addingItalicsToText = false;

        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<') addingItalicsToText = true;
            
            dialogueText.text += letter;

            if (letter == '>') addingItalicsToText = false;

            if (!addingItalicsToText)
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        dialogueCheck.enabled = true;
        if (currentStory.currentChoices.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            DisplayChoices();
        }
    }

    public void EnterDialogue(TextAsset inkJson, Animator npc, TestAIScript agent)
    {
        npcAnimator = npc;
        aiAgent = agent;
        if (aiAgent != null && aiAgent.isMoving) return; // Do not initiate dialogue if a character is moving
        currentStory = new(inkJson.text);
        dialogueVariables.StartListening(currentStory);
        //if (agent != null)
            functions.Bind(currentStory, agent);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        itemPortrait.gameObject.SetActive(false);
        inputCooldownUntil = Time.time + INPUT_COOLDOWN;
        ContinueStory();
    }

    // Disables UI, unbinds external functions called from INK, and dialoguevariables stop subscribing to any variable events
    private void ExitDialogue()
    {
        dialogueVariables.StopListening(currentStory);
        functions.Unbind(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
        itemPortrait.gameObject.SetActive(false);
    }

    void ContinueStory()
    {
        //Debug.Log($"ContinueStory called. canContinue: {currentStory.canContinue}, " +
        //          $"choices: {currentStory.currentChoices.Count}");

        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
                StopCoroutine(displayLineCoroutine);

            string line = currentStory.Continue();
            //Debug.Log($"Continued. Line: '{line}', canContinue after: {currentStory.canContinue}, " +
            //          $"choices after: {currentStory.currentChoices.Count}");

            if (string.IsNullOrWhiteSpace(line) && !currentStory.canContinue
                && currentStory.currentChoices.Count == 0)
            {
                //Debug.Log("Empty line with nothing left — exiting.");
                ExitDialogue();
                return;
            }

            displayLineCoroutine = StartCoroutine(TypeText(line));
            HandleTags(currentStory.currentTags);
        }
        else if (currentStory.currentChoices.Count == 0)
        {
            //Debug.Log("Nothing left — exiting.");
            ExitDialogue();
        }
    }

    /// <summary>
    /// In INK, you can set global #tags for the entire file or after individual lines. This method parses
    /// them, and sets their values to the corresponding thing. Example:
    /// "#anim: Walking" parses the 'anim' tag and enters the corresponding switch statement, 
    /// gets the descriptor string 'Walking', and sets the string as the animation trigger for the current character or object.
    /// </summary>
    /// <param name="currentTags">All tags collected from the latest Continue() call of the story.</param>
    public void HandleTags(List<string> currentTags)
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
                    //if (tagValue.Equals(null)) tagValue = "Test";
                    if (tagValue.Equals("Player"))
                    {
                        playerText.text = "Justin Time";
                        npcPanel.SetActive(false);
                        playerPanel.SetActive(true);
                    }
                    else
                    {
                        npcText.text = tagValue;
                        npcPanel.SetActive(true);
                        playerPanel.SetActive(false);
                    }
                    //displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // Hantera porträtt, t.ex. visa en bild av talaren
                    Debug.Log($"Portrait: {tagValue}");

                    if (int.TryParse(tagValue, out int itemId) && itemDatabase.GetItem.TryGetValue(itemId, out ItemObject item))
                    {
                        itemPortrait.sprite = item.uiDisplay;
                        itemPortrait.gameObject.SetActive(true);
                    }
                    else if (string.IsNullOrEmpty(tagValue))
                        itemPortrait.gameObject.SetActive(false);
                    else
                    {
                        Debug.LogWarning($"Portrait tag value '{tagValue}' not found in item database.");
                        itemPortrait.gameObject.SetActive(false);
                    }

                    break;
                //case LAYOUT_TAG:
                //    // Hantera layout, t.ex. ändra position eller stil på dialogrutan
                //    Debug.Log($"Layout: {tagValue}");
                //    break;
                case ANIM_TAG:
                    if (npcAnimator == null) return;
                    Debug.Log($"Animation: {tagValue}, currentTag: {currentValue}");

                    npcAnimator.SetTrigger(tagValue);
                    break;
                case "snore":
                    if (tagValue == "stop")
                    {
                        if (SnoringZoneLink.Instance != null)
                            SnoringZoneLink.Instance.SetSnoringState(false);
                    }
                    else if (tagValue == "start")
                    {
                        if (SnoringZoneLink.Instance != null)
                            SnoringZoneLink.Instance.SetSnoringState(true);
                    }
                    break;
                case "video":
                    if (tagValue == "play_karaoke") 
                    {
                        if (InteractableTV.Instance != null) 
                        {
                            InteractableTV.Instance.PlayVideo();
                        }
                    }
                    break;
                default:
                    Debug.LogWarning($"Unknown tag key: {tagKey}");
                    break;
            }
        }
    }

    // Hides the Dialogue Choices UI-component after making a choice.
    void HideChoices()
    {
        choicesContainer.SetActive(false);
        foreach (GameObject choice in choices)
            choice.SetActive(false);
    }

    /// <summary>
    /// If the latest Continue() call for the current story contains dialogue choices, this method displays
    /// them at the end of the dialogue line. 
    /// </summary>
    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.Log($"More choices in the story than UI can support. " +
                $"Number of choices: {currentChoices.Count}", this);
        }

        choicesContainer.SetActive(currentChoices.Count > 0);

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

    // A workaround method for registering button presses for the Event System. I have no idea how or why it works
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

    /// <summary>
    /// This method is bound to their respective button game components in the Dialogue UI. When a button with
    /// index n is pressed, it calls this method and sends that as the parameter, and then continues the story.
    /// </summary>
    /// <param name="choiceIndex"></param>
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        inputCooldownUntil = Time.time + INPUT_COOLDOWN;
        ContinueStory();
        
    }
    
}