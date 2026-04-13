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
public class NewDialogueManager : MonoBehaviour
{
    // Alla komponenter för dialogpanelen
    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] UnityEngine.UI.Image dialogueCheck;
    [SerializeField] UnityEngine.UI.Image itemPortrait;
    //[SerializeField] TextMeshProUGUI displayNameText;
    [SerializeField] ItemDatabaseObject itemDatabase;

    [Header("Load Globals JSON")]
    [SerializeField] TextAsset loadGlobalsJSON;

    [Header("Choices UI")]
    [SerializeField] GameObject choicesContainer;
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    [Header("Speaker Panels")]
    [SerializeField] GameObject npcPanel;
    [SerializeField] GameObject playerPanel;
    [SerializeField] TextMeshProUGUI npcText;
    [SerializeField] TextMeshProUGUI playerText;

    // Behövs för att binda en extern funktion inuti Ink, kan lämnas som den är nu men kan behövas ändras i framtiden
    [Header("External Function References")]
    [SerializeField] TestAIScript aiAgent;

    Animator npcAnimator; // referens till animatorn för en npc. Parsar tags inuti Ink-filer som sätter animationtriggers
    

    [SerializeField] float typingSpeed = 0.03f;
    // Add with other private fields
    private float inputCooldownUntil;
    private const float INPUT_COOLDOWN = 0.1f;

    public Story currentStory { get; private set; } // Den dialog som spelas för tillfället: Nästan allting utgår från denna  
    public bool dialogueIsPlaying { get; private set; } // Referens som andra klasser kan hämta för att kontrollera att en dialog är aktiv eller inte 
    bool isTyping;

    private Coroutine displayLineCoroutine;

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
        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        isTyping = false;

        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
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

    private void Update()
    {
        if (!dialogueIsPlaying /*||
            currentStory.currentChoices.Count > 0*/ || choicesContainer.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitDialogue();

        //StartCoroutine(CanContinue());

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

    IEnumerator CanContinue()
    {
        yield return new WaitUntil(() => currentStory.canContinue || currentStory.currentChoices.Count > 0 || 
        !currentStory.canContinue);

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


    //IEnumerator TypeText(string line)
    //{
    //    HideChoices();
    //    // Denna gör att nuvarande val dyker upp så fort dialogen är klar; Enda nackdelen är att det inte går att
    //    // autocompletea dialogen. Försök fixa det
    //    if (currentStory.currentChoices.Count > 0)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        //isTyping = false;
    //        //dialogueText.text = string.Empty;
    //        DisplayChoices();
    //        //yield return new WaitForSeconds(0.01f);
    //    }
    //    //else
    //    {
    //        dialogueText.text = string.Empty;
    //        isTyping = true;
    //        foreach (char letter in line.ToCharArray())
    //        {
    //            dialogueText.text += letter;
    //            yield return new WaitForSeconds(typingSpeed);
    //        }
    //        isTyping = false;

    //        // Om man istället har den här så måste man klicka en extra gång för att få upp valen.
    //        // Inte lika snyggt men fungerar att autocompleta dialogen när det finns val.
    //        //if (currentStory.currentChoices.Count > 0)
    //        //{
    //        //    yield return new WaitForSeconds(0.1f);
    //        //    DisplayChoices();
    //        //}
    //    }
    //}

    IEnumerator TypeText(string line)
    {
        HideChoices();
        dialogueText.text = string.Empty;
        isTyping = true;
        dialogueCheck.enabled = false;

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
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



    public void EnterDialogue(TextAsset inkJson, Animator npc)
    {
        //PlayerController.Instance.enabled = false;
        npcAnimator = npc;
        currentStory = new(inkJson.text);
        dialogueVariables.StartListening(currentStory);
        functions.Bind(currentStory, aiAgent);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        itemPortrait.gameObject.SetActive(false);
        inputCooldownUntil = Time.time + INPUT_COOLDOWN;
        ContinueStory();
    }

    private void ExitDialogue()
    {

        dialogueVariables.StopListening(currentStory);
        //PlayerController.Instance.enabled = true;
        functions.Unbind(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
        itemPortrait.gameObject.SetActive(false);
    }

    void ContinueStory()
    {
        Debug.Log($"ContinueStory called. canContinue: {currentStory.canContinue}, " +
                  $"choices: {currentStory.currentChoices.Count}");

        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
                StopCoroutine(displayLineCoroutine);

            string line = currentStory.Continue();
            Debug.Log($"Continued. Line: '{line}', canContinue after: {currentStory.canContinue}, " +
                      $"choices after: {currentStory.currentChoices.Count}");

            if (string.IsNullOrWhiteSpace(line) && !currentStory.canContinue
                && currentStory.currentChoices.Count == 0)
            {
                Debug.Log("Empty line with nothing left — exiting.");
                ExitDialogue();
                return;
            }

            displayLineCoroutine = StartCoroutine(TypeText(line));
            HandleTags(currentStory.currentTags);
        }
        else if (currentStory.currentChoices.Count == 0)
        {
            Debug.Log("Nothing left — exiting.");
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
                    if (tagValue.Equals(null)) tagValue = "Test";
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
                    //displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // Hantera porträtt, t.ex. visa en bild av talaren
                    Debug.Log($"Portrait: {tagValue}");
                    //itemPortrait.enabled = true;
                    //itemPortrait.sprite = Resources.Load<Sprite>(tagValue);
                    //itemPortrait.sprite = ScriptableObject.FindFirstObjectByType<ItemDatabaseObject>()
                    //foreach (var clue in ScriptableObject.FindFirstObjectByType<ItemDatabaseObject>().items)
                    //{
                    //    if (clue.Id.Equals(Convert.ToInt32(tagValue)))
                    //    {
                    //        itemPortrait.sprite = clue.uiDisplay;
                    //        break;
                    //    }
                    //}

                    if (int.TryParse(tagValue, out int itemId) && itemDatabase.GetItem.TryGetValue(itemId, out ItemObject item))
                    {
                        itemPortrait.sprite = item.uiDisplay;
                        itemPortrait.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning($"Portrait tag value '{tagValue}' not found in item database.");
                        itemPortrait.gameObject.SetActive(false);
                    }

                    break;
                case LAYOUT_TAG:
                    // Hantera layout, t.ex. ändra position eller stil på dialogrutan
                    Debug.Log($"Layout: {tagValue}");
                    break;
                case ANIM_TAG:
                    if (npcAnimator == null) return;
                    Debug.Log($"Animation: {tagValue}, currentTag: {currentValue}");

                    npcAnimator.SetTrigger(tagValue);
                    break;
                default:
                    Debug.LogWarning($"Unknown tag key: {tagKey}");
                    break;
            }
        }
    }

    void HideChoices()
    {
        choicesContainer.SetActive(false);
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

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        inputCooldownUntil = Time.time + INPUT_COOLDOWN;
        ContinueStory();
        
    }
    
}