using Ink.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [Header("References")]
    public InventoryObject playerInventory; 
    public ItemObject garlicBreathClue;
    [SerializeField] TextAsset inkJson;
    

    private bool hasDiscoveredBreath = false;
    HighlightActivatorIAVersion highlighter;
    ParticleSystem particles;

    void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Play();
        particles.Clear();
        particles.Stop();
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
    }

    void Update()
    {
        if (particles.Equals(null)) return;
        if (highlighter.IsHighlighting)
            particles.Play();
        else
            particles.Stop();
        if (hasDiscoveredBreath)
            Destroy(particles);
    }
    public void Interact()
    {
        //if (!highlighter.IsHighlighting) return;
        if (!hasDiscoveredBreath)
        {
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);
            Debug.Log("Wow, this guy needs a mint. Added Garlic Breath Clue!");

            // Add the item to the inventory
            playerInventory.AddItem(new Item(garlicBreathClue));

            hasDiscoveredBreath = true;
        }

    }
}
