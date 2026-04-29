using UnityEngine;
using UnityEngine.Video;
public class albumTV : MonoBehaviour
{
    public VideoPlayer tvPlayer;
    public VideoClip loopingClip;

    void Start()
    {
        // If you forgot to assign the player, it finds it automatically
        if (tvPlayer == null)
        {
            tvPlayer = GetComponent<VideoPlayer>();
        }

        // This tells Unity: "When the video finishes, run the function below"
        tvPlayer.loopPointReached += OnFirstVideoEnded;
    }

    void OnFirstVideoEnded(VideoPlayer vp)
    {
        // Swap out the first clip for the second one
        vp.clip = loopingClip;

        // Turn on the loop function so the second video plays forever
        vp.isLooping = true;

        // Hit play
        vp.Play();

        // Disconnect the alarm so it doesn't keep trying to swap videos
        vp.loopPointReached -= OnFirstVideoEnded;
    }
}
