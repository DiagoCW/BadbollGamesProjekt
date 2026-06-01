using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Author: Stefan & Isak
/// Contains references to the objects navmeshagent and targets that it can move to.
/// 
/// </summary>
public class TestAIScript : MonoBehaviour
{
    public Transform targetDestination;
    [SerializeField] List<Transform> targetDestinations;
    private int destIndex = 0;

    private NavMeshAgent agent;
    private Animator animator;

    private string animtrigger;
    public bool isMoving = false;
    public bool isBlockingDialogue = false;

    private float moveStartTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        if (!isMoving) return;
        if (Time.time < moveStartTime + 0.2f) return;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            animator.SetTrigger($"Stop{animtrigger}");
            agent.tag = "NPC";
            isMoving = false;
            isBlockingDialogue = false;
        }
            
    }
    public void StartPathBlocking(string trigger)
    {
        isBlockingDialogue = true; // Lock the dialogue
        StartPath(trigger); // Start the normal movement
    }

    /// <summary>
    /// This method is bound to an external function defined within INK, that can start their path when called in dialogue
    /// </summary>
    /// <param name="trigger"></param>
    public void StartPath(string trigger)
    {
        animtrigger = trigger;

        if (targetDestination != null && targetDestinations.Count > 0)
        {
            animator.SetTrigger(animtrigger);

            agent.SetDestination(targetDestinations[destIndex].position); // set destination to current index

            destIndex++;
            if (destIndex >= targetDestinations.Count)
            {
                destIndex = 0; // Wrap around to the start
            }

            //agent.tag = "Untagged";
            isMoving = true;
            moveStartTime = Time.time;
        }
        else
        {
            Debug.LogError("Target destination not set for TestAIScript on " + gameObject.name);
        }
    }
}
