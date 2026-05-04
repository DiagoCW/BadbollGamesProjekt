using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SnoringController : MonoBehaviour
{
    private AudioSource snoringAudio;
    private bool playerInRoom = false;

    void Start()
    {
        snoringAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRoom = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRoom = false;
            snoringAudio.Pause();
        }
    }

    void Update()
    {
        if (!playerInRoom) return;

        bool isTalking = NewDialogueManager.Instance.dialogueIsPlaying;

        if (!isTalking && !snoringAudio.isPlaying)
        {
            snoringAudio.Play();
        }
        
        else if (isTalking && snoringAudio.isPlaying)
        {
            snoringAudio.Pause();
        }
    }
}