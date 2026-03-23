using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DraggableOutline : MonoBehaviour
{
    private Image image;
    private Color normalColor;
    private Outline outline;

    void Awake()
    {
        image = GetComponent<Image>();
        normalColor = image.color;

        // Add Outline component
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        outline.effectColor = new Color(1f, 1f, 0f, 0.8f); // bright yellow
        outline.effectDistance = new Vector2(4, -4);
        outline.enabled = false; // off by default
    }

    void OnEnable()
    {
        
#if UNITY_EDITOR
        outline.enabled = true;
#endif
    }

    
    public void OnPointerEnter()
    {
        outline.enabled = true;
    }

    public void OnPointerExit()
    {
        outline.enabled = false;
    }
}