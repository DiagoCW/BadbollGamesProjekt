using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        //if (!agent.pathPending)
        //{
        //    if (agent.remainingDistance <= agent.stoppingDistance)
        //    {
        //        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
        //        {
        //            // Done
        //            animator.ResetTrigger("Running");
        //        }
        //    }
        //}
        if (!isMoving) return;
        if (Vector3.Distance(agent.transform.position, targetDestinations[destIndex - 1].position) <= 0.5f)
        {
            //animator.ResetTrigger("Running");
            animator.SetTrigger($"Stop{animtrigger}");
            agent.tag = "NPC";
            isMoving = false;
            if (destIndex >= targetDestinations.Count)
            {
                destIndex = 0;
            }
        }
            


        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    //agent.isStopped = true;
        //    //animator.SetTrigger("StopRunning");
        //    animator.ResetTrigger("Running");
        //    //animator.SetBool("Idle", true);

            //    //Destroy(gameObject);
            //}

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
