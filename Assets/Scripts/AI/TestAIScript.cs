using UnityEngine;
using UnityEngine.AI;

public class TestAIScript : MonoBehaviour
{
    public Transform targetDestination;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
