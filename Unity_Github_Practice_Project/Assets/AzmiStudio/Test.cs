using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Test : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [TextArea]
    [SerializeField] private string videoUrl = 
        "https://github.com/sabbirahmedsr/VideoHostTutorial/raw/main/184734-873923034_tiny.mp4";
    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayer == null) {
            videoPlayer = FindObjectOfType<VideoPlayer>();
        }
       // videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer) {
            videoPlayer.url = videoUrl;
            videoPlayer.playOnAwake = false;
            videoPlayer.Prepare();

            videoPlayer.prepareCompleted += OnVideoPrepared;
        }
    }

    private void OnVideoPrepared(VideoPlayer source) {
        videoPlayer.Play();
    }
}
