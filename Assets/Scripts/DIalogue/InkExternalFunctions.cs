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

        // Plays a clip without looping
        story.BindExternalFunction("playAudio", (string id) =>
        {
            StartAudioManager.Instance.PlaySFX(id);
        });
        // Plays an audio on loop until stopped, only one can play at a time
        story.BindExternalFunction("playAmbience", (string id) =>
        {
            StartAudioManager.Instance.PlayAmbience(id);
        });
        // Stops a currently playing ambience
        story.BindExternalFunction("stopAmbience", (string id) =>
        {
            StartAudioManager.Instance.StopAmbience(id);
        });
        // Or add a new function to stop all
        story.BindExternalFunction("stopAllAmbience", () =>
        {
            StartAudioManager.Instance.StopAllAmbience();
        });
        story.BindExternalFunction("lowerPitch", (string id) =>
        {
            CoroutineRunner.instance.StartCoroutine(StartAudioManager.Instance.LowerPitch(id));
        });
        // Allow ink to know if the solution is correct
        story.BindExternalFunction("isSolutionCorrect", () =>
        {
            return ThreadManager.Instance.EvaluateBoard();
        });
        // Allows ink to unlock the clueboard if the players gets the solution wrong (This is for the tutorial)
        story.BindExternalFunction("unlockBoard", () =>
        {
            ThreadManager.Instance.SetBoardLockState(false);
        });
        // allows ink to ask if the board is locked before evaluating
        story.BindExternalFunction("isBoardLocked", () =>
        {
            return ThreadManager.Instance.isLocked;
        });

    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startMovement");
        story.UnbindExternalFunction("FadeIn");
        story.UnbindExternalFunction("FadeOut");
        story.UnbindExternalFunction("unlockSuspect");
        story.UnbindExternalFunction("isSolutionCorrect");
        story.UnbindExternalFunction("unlockBoard");
        story.UnbindExternalFunction("isBoardLocked");

        // Audio functions
        story.UnbindExternalFunction("playAudio");
        story.UnbindExternalFunction("playAmbience");
        story.UnbindExternalFunction("stopAmbience");
        story.UnbindExternalFunction("stopAllAmbience");
        story.UnbindExternalFunction("lowerPitch");
    }
}
