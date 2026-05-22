using Ink.Runtime;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
/// <summary>
/// Authored by Isak. 
/// Takes any EXTERNAL FUNCTIONS declared within in an INK file, and binds them to Unity methods. Whenever the function
/// is called from within INK, this script calls the method within Unity that it is bound to. 
/// Even if the function is not called from within the current INK story, it must still be bound (as well as unbound).
/// </summary>
public class InkExternalFunctions
{
    /// <summary>
    /// EXAMPLE: Within INK, an "EXTERNAL FUNCTION startMovement(x) is declared. It is called from within INK by using 
    /// '~ startMovement(x), with 'x' being the argument to pass along (in this case the animation trigger to set).
    /// This method then passes it along to the method within the aiAgent-instance, and there it starts the current
    /// navmeshagent and also sets the animation parameter. 
    /// The method is called everytime EnterDialogue() in DialogueManager is called.
    /// </summary>
    /// <param name="story">The current INK story that is being executed</param>
    /// <param name="aiAgent">Must be passed along to activate navmeshagents from an INK file</param>
    public void Bind(Story story, TestAIScript aiAgent)
    {
        story.BindExternalFunction("startMovement", (string name) =>
        {
            //Debug.Log($"External function triggered!");
            aiAgent?.StartPath(name);
        });
        story.BindExternalFunction("FadeIn", () =>
        {
            if (FadeInOut.Instance != null) 
            {
                FadeInOut.Instance.FadeToScene("MainScene", 5f); // Wait 5 seconds then load
            }
        });
        story.BindExternalFunction("FadeOut", () =>
        {
            if (FadeInOut.Instance != null) 
            {
                FadeInOut.Instance.FadeScreenOnly(0f, 2f); // Fades from black to clear in over 2 seconds
            }
        });
        story.BindExternalFunction("unlockSuspect", (int id) =>
        {
            SuspectManager.Instance.UnlockSuspect(id);
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startMovement");
        story.UnbindExternalFunction("FadeIn");
        story.UnbindExternalFunction("FadeOut");
        story.UnbindExternalFunction("unlockSuspect");
    }
}
