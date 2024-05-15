using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public delegate void VideoPlayBackCompleted();
    public event VideoPlayBackCompleted OnVideoPlayBackCompleted;

    public void StartVideo(VideoClip videoToPlay) {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = true;
        videoPlayer.clip = videoToPlay;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.frame = 100;
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp) {
        Destroy(vp);
        OnVideoPlayBackCompleted?.Invoke();
    }
}
