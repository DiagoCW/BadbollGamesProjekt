using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// draws the red thread on the UI. 
/// stretches and rotates an Image to connect a suspect to a clue slot
/// </summary>
[RequireComponent(typeof(Image))]
public class ThreadLine : MonoBehaviour
{
    [Tooltip("How wide the thread should be")]
    public float thickness = 2f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Set pivot to the left edge (0, 0.5) so the line grows outward from the start point instead of growing from the middle.
        rectTransform.pivot = new Vector2(0f, 0.5f);

        // center the anchors so the positioning math aligns with the canvas
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
    }

    /// <summary>
    /// Stretches and rotates the line to connect the start and end points
    /// </summary>
    /// <param name="startLocal">The starting UI coordinate.</param>
    /// <param name="endLocal">The ending UI coordinate.</param>
    public void UpdateLine(Vector2 startLocal, Vector2 endLocal)
    {
        // move the start of the line to the first point
        rectTransform.localPosition = startLocal;

        // find the distance and direction between the two points
        Vector2 direction = endLocal - startLocal;

        // calculate the angle to make the line point directly at the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.localRotation = Quaternion.Euler(0, 0, angle);

        // stretch the line width to match the distance
        float distance = direction.magnitude;
        rectTransform.sizeDelta = new Vector2(distance, thickness);
    }
}