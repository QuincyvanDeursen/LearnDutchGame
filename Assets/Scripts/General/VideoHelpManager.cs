using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHelpManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public void PlayVideoClip()
    {
        videoPlayer.Play();
    }
}
