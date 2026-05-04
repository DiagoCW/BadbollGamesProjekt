using UnityEngine;
using UnityEngine.AI;

public class TestAIScript : MonoBehaviour
{
    public Transform targetDestination;

    private NavMeshAgent agent;
    private Animator animator;

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

        float dist = agent.remainingDistance;
        if (Vector3.Distance(agent.transform.position, targetDestination.position) <= 1)
        {
            animator.ResetTrigger("Running");
            animator.SetTrigger("StopRunning");
            //Destroy(gameObject);
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

    public void StartPath()
    {
        if (targetDestination != null)
        {
            agent.SetDestination(targetDestination.position);
        }
        else
        {
            Debug.LogError("Target destination not set for TestAIScript on " + gameObject.name);
        }
        
    }
}
