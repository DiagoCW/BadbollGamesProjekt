using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class InkExternalFunctions
{
    
    public void Bind(Story story, TestAIScript aiAgent)
    {
        story.BindExternalFunction("runAway", () =>
        {
            Debug.Log($"External function triggered!");
            //aiAgent?.StartPath();
            //aiAgent.tag = "Untagged";
        });
        
        
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("runAway");
    }


}
