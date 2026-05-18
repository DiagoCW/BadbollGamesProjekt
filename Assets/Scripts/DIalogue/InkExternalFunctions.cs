using Ink.Runtime;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class InkExternalFunctions
{
    public void Bind(Story story, TestAIScript aiAgent)
    {
        story.BindExternalFunction("startMovement", (string name) =>
        {
            Debug.Log($"External function triggered!");
            aiAgent?.StartPath(name);
            //aiAgent.tag = "Untagged";
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

    //private void FadeInAndLoadScene()
    //{
    //    //FadeInOut.Instance.Play("FadeIn");
    //    // Start a coroutine to wait for the fade-in animation to complete before loading the scene
    //    CoroutineRunner.instance.StartCoroutine(FadeInAndLoadSceneCoroutine());
    //}

    //IEnumerator FadeInAndLoadSceneCoroutine()
    //{
    //    FadeInOut.Instance.Play("FadeIn");
    //    yield return new WaitForSeconds(5f); // Adjust the wait time as needed
    //    SceneManager.LoadScene("MainScene");
    //}

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startMovement");
        story.UnbindExternalFunction("FadeIn");
        story.UnbindExternalFunction("FadeOut");
        story.UnbindExternalFunction("unlockSuspect");
    }
}
