using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour {
    public Transform rootObject;
    [Space]

    [SerializeField] private VideoPlayer videoPlayer;
    [TextArea]
    [SerializeField]
    public string videoUrl =
        "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";


    [Space]
    [SerializeField] private Transform pauseMarkerRoot;

    [Space]
    public IPAddressController iPAddressController;

    // Start is called before the first frame update
    void OnEnable() {
        if (videoPlayer == null) {
            videoPlayer = FindObjectOfType<VideoPlayer>();
        }
        // videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer) {
            //videoPlayer.url = videoUrl;
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnVideoPrepared;
            Debug.Log("VVVV :: " + videoUrl);
        }
    }

    private void OnDisable() {
        videoPlayer.prepareCompleted -= OnVideoPrepared;
    }

    public void SetVideoURL(string rURL) {
        videoUrl = rURL;
        videoPlayer.url = rURL;
    }


    private void OnVideoPrepared(VideoPlayer source) {
        videoPlayer.Play();
        pauseMarkerRoot.gameObject.SetActive(false);
    }



    public void ToggleVideo() {
        if (videoPlayer) {
            if (videoPlayer.isPlaying) {
                videoPlayer.Pause();
                pauseMarkerRoot.gameObject.SetActive(true);
            } else {
                if (videoPlayer.isPrepared) {
                    videoPlayer.Play();
                    pauseMarkerRoot.gameObject.SetActive(false);
                } else {
                    videoPlayer.Prepare();
                }
            }
        }
    }

    internal void Activate(bool rBool) {
        rootObject.gameObject.SetActive(rBool);
    }

    public void ShowIPAddressSetting() {
        iPAddressController.Activate(true);
        this.Activate(false);
    }
}
