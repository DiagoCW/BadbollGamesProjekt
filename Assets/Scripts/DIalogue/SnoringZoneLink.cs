using UnityEngine;

public class SnoringZoneLink : MonoBehaviour
{
    [Header("Drag the Armchair Guy's Audio Source here")]
    public AudioSource npcSnoring;

    public static SnoringZoneLink Instance;

    public bool playerInRoom = false;
    public bool isAsleep = true;

    void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRoom = true;

            // Only snore if he is actually asleep
            if (isAsleep && !npcSnoring.isPlaying)
            {
                npcSnoring.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRoom = false;
            
            npcSnoring.Pause();
        }
    }

    public void SetSnoringState(bool shouldSnore)
    {
        isAsleep = shouldSnore;

        if (isAsleep && playerInRoom)
        {
            npcSnoring.Play();
        }
        else
        {
            npcSnoring.Pause();
        }
    }
}