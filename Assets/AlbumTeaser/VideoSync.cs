using UnityEngine;
using UnityEngine.Video;

public class VideoSync : MonoBehaviour
{
    public VideoPlayer vp;

    void Start()
    {
        // Brutally cut the power so it can never play on its own
        vp.Pause();
    }

    void Update()
    {
        // Calculate the exact frame based on the slowed-down game clock and force the video to jump to it
        vp.frame = (long)(Time.timeSinceLevelLoad * vp.frameRate);
    }
}