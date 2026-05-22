using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Author: Stefan & Isak
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        if (!isMoving) return;
        if (Vector3.Distance(agent.transform.position, targetDestinations[destIndex - 1].position) <= 0.5f)
        {
            animator.SetTrigger($"Stop{animtrigger}");
            agent.tag = "NPC";
            isMoving = false;
            if (destIndex >= targetDestinations.Count)
            {
                destIndex = 0;
            }
        }
            
    }
    public void StartPath(string trigger)
    {
        animtrigger = trigger;
        if (targetDestination != null)
        {
            animator.SetTrigger(animtrigger);
            agent.SetDestination(targetDestinations[destIndex++ % targetDestinations.Count].position);
            //agent.tag = "Untagged";
            isMoving = true;
        }
        else
        {
            Debug.LogError("Target destination not set for TestAIScript on " + gameObject.name);
        }
    }
}
