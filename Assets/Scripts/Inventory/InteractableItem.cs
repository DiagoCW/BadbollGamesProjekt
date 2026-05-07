using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    private InventoryObject playerInventory;
    [SerializeField] ItemObject item;
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
        if (outline.hasBeenHighlighted)
        {
            //Debug.Log("Detective vision enabled, and item is interacted with");

            if (item != null)
            {
                if (inkJson2 != null)
                    NewDialogueManager.Instance.EnterDialogue(inkJson2, null, null);
                if (!pickedUpClue)
                {
                    playerInventory.AddItem(new Item(item));
                    pickedUpClue = true;
                    Debug.Log($"Added {item.name} to player inventory");
                    gameObject.tag = "Untagged";
                }
                //Destroy(gameObject);
            }
            //gameObject.tag = "Untagged";
        }
        else if (inkJson != null && !highlighter.IsHighlighting)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
        //else Debug.Log("Detective vision NOT enabled, and item is interacted with");
    }
}
