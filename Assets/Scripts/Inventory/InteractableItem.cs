using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    // this script is used for items that can be interacted with in the world,
    // such as clues. It checks if the player is using detective vision and
    // gives different dialogue and items based on that. It replaced the
    // previus clues interact script -Hugo

    [Header("References")]
    private InventoryObject playerInventory;
    [SerializeField] ItemObject item, item2;
    [SerializeField] TextAsset inkJson, inkJson2;

    bool pickedUpClue = false;
    
    HighlightActivatorIAVersion highlighter;
    ParticleSystem particles;
    OutlineHighlighter outline;

    void Start()
    {
        playerInventory = PlayerController.Instance.inventory;
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
        outline = GetComponentInChildren<OutlineHighlighter>();

        particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Play();
            particles.Clear();
            particles.Stop();
        }
    }

    void HandleParticles()
    {
        if (particles == null) return;
        if (highlighter.IsHighlighting)
            particles.Play();
        else
            particles.Stop();
    }
    void Update()
    {
        HandleParticles();
    }
    public void Interact()
    {
        if (outline != null && outline.hasBeenHighlighted)
        {
            //Debug.Log("Detective vision enabled, and item is interacted with");
            if (inkJson2 != null)
                NewDialogueManager.Instance.EnterDialogue(inkJson2, null, null);
            if (!pickedUpClue && item != null)
            {
                if (item2 != null)
                {
                    playerInventory.AddItem(new Item(item2));
                    Debug.Log($"Added {item2.name} to player inventory");
                }
                    
                playerInventory.AddItem(new Item(item));
                pickedUpClue = true;
                gameObject.tag = "Untagged";
            }
        }
        else if (inkJson != null && !highlighter.IsHighlighting)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
        //else Debug.Log("Detective vision NOT enabled, and item is interacted with");
    }
}
