using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Video;

public class InteractableTV : MonoBehaviour, IInteractable
{
    [Header("Dialogue Reference")]
    [SerializeField] TextAsset tvInkJson;

    [Header("Tv Components")]
    [Tooltip("Drag the video player here")]
    public VideoPlayer tvVideoPlayer;

    public static InteractableTV Instance;

    void Awake() 
    {
        Instance = this;
    }

    public void Interact() 
    {
        if (tvInkJson != null) 
        {
            NewDialogueManager.Instance.EnterDialogue(tvInkJson, null, null);
        }
    }

    public void PlayVideo() 
    {
        if (tvVideoPlayer != null && !tvVideoPlayer.isPlaying)
        {
            tvVideoPlayer.Play();
        }
    }
}
