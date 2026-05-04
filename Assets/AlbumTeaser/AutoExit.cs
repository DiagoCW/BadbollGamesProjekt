using UnityEngine;
using UnityEngine.Playables;

public class AutoExit : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }

        if (director != null)
        {
            director.stopped += OnTimelineFinished;
        }
    }

    void OnTimelineFinished(PlayableDirector d)
    {
        Debug.Log("Timeline finished! Exiting game...");

        // This quits the game if you build it
        Application.Quit();

        // This stops Play Mode if you are testing inside theEditor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
