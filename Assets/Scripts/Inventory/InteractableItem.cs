using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    private InventoryObject playerInventory;
    [SerializeField] ItemObject item;
    [SerializeField] TextAsset inkJson, inkJson2;
    HighlightActivatorIAVersion highlighter;
    ParticleSystem particles;

    void Start()
    {
        playerInventory = PlayerController.Instance.inventory;
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();

        particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Play();
            particles.Clear();
            particles.Stop();
        }
    }

    void Update()
    {
        if (particles == null) return;
        if (highlighter.IsHighlighting)
            particles.Play();
        else
            particles.Stop();
    }
    public void Interact()
    {
        if (highlighter.IsHighlighting)
        {
            Debug.Log("Detective vision enabled, and item is interacted with");

            if (item != null)
            {
                if (inkJson2 != null)
                    NewDialogueManager.Instance.EnterDialogue(inkJson2, null);
                playerInventory.AddItem(new Item(item));
                //Destroy(gameObject);
            }
            //gameObject.tag = "Untagged";
        }
        else if (inkJson != null && !highlighter.IsHighlighting)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);
        else Debug.Log("Detective vision NOT enabled, and item is interacted with");
    }
}
