using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class Test2 : MonoBehaviour {
    public string videoStreamURL; // URL of the Flask server's /video endpoint
    private VideoPlayer videoPlayer;
    private bool isStreaming = false;

    void Start() {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        StartCoroutine(StartVideoStream());
    }

    IEnumerator StartVideoStream() {
        isStreaming = true;
        while (isStreaming) {
            UnityWebRequest www = UnityWebRequest.Get(videoStreamURL);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                byte[] videoData = www.downloadHandler.data;
                CreateVideoClip(videoData);
            } else {
                Debug.LogError("Failed to receive video stream: " + www.error);
            }
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        }
    }

    public VideoClip tgtVideoClip;
    void CreateVideoClip(byte[] videoData) {
        string tempFilePath = Application.persistentDataPath + "/tempVideo.mp4";
        System.IO.File.WriteAllBytes(tempFilePath, videoData);

        VideoClip videoClip = tgtVideoClip; // VideoClip.CreateVideoClipFromFile(tempFilePath, "video", VideoAudioOutputMode.Direct);
        videoPlayer.clip = videoClip;
        videoPlayer.playOnAwake = true;
        videoPlayer.isLooping = true; // Stream will keep looping

        // Optionally, set other VideoPlayer properties like renderMode, audioOutputMode, etc.
    }

    void OnDisable() {
        isStreaming = false;
    }
}