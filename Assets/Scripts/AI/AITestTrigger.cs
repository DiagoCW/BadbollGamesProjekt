using UnityEngine;

public class AITestTrigger : MonoBehaviour
{
    public TestAIScript npc;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            npc.StartPath("Walk");
        }
    }
}