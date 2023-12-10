using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : Singleton<VideoManager>
{
    private VideoPlayer videoPlayer;

    protected override void Awake() 
    {
        base.Awake();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Application.runInBackground = true;

        if (!videoPlayer)
            GetVideoPlayer();
        if (scene.name == "VideoScene")
        {
            StartCoroutine(PlayVideo("Sample"));
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        StopAllCoroutines();
    }


    IEnumerator PlayVideo(string videoPath)
    {
        videoPlayer.source = VideoSource.VideoClip;

        if(videoPlayer.clip == null)
            videoPlayer.clip = Resources.Load("Video/" + videoPath, typeof(VideoClip)) as VideoClip;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }


    private void TheEnd(UnityEngine.Video.VideoPlayer vp)
    {
        print("video is over");
        videoPlayer.Stop();
        //SceneManager.LoadScene("BattleScene");
        //SceneManager.LoadScene("DialogueScene");
        SceneManager.LoadScene("ChooseScene");
    }

    private void GetVideoPlayer()
    {
        if (SceneManager.GetActiveScene().name == "VideoScene" && videoPlayer == null)
        {
            videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();

            videoPlayer.isLooping = false;
            videoPlayer.loopPointReached += TheEnd;
        }
        else
            videoPlayer = null;
    }
}
