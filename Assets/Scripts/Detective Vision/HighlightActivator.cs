using UnityEngine;

public class HighlightActivator : MonoBehaviour
{
    [SerializeField] private KeyCode activateKey = KeyCode.V;
    [SerializeField] private string highlightableTag = "Highlightable";
    [SerializeField] private float highlightDuration = 5f;
    [SerializeField] private float cooldownDuration = 20f;

    private OutlineHighlighter currentHighlighter;
    private float cooldownEndTime;

    void Update()
    {
        if (Time.time < cooldownEndTime) return;

        if (Input.GetKeyDown(activateKey))
        {
            TryHighlightObject();
        }
    }

    void TryHighlightObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            if (!string.IsNullOrEmpty(highlightableTag) &&
                !hit.collider.CompareTag(highlightableTag))
            {
                return;
            }

            OutlineHighlighter highlighter = hit.collider.GetComponent<OutlineHighlighter>();

            if (highlighter != null)
            {
                highlighter.SetHighlighted(true, highlightDuration);
                currentHighlighter = highlighter;
                cooldownEndTime = Time.time + cooldownDuration;
            }
        }
    }

    public void ClearCurrentHighlight()
    {
        if (currentHighlighter != null)
        {
            currentHighlighter.SetHighlighted(false, 0f);
            currentHighlighter = null;
        }
    }
}