using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : Singleton<VideoManager>
{
    private VideoPlayer videoPlayer;
    bool opening, ending;

    protected override void Awake() 
    {
        base.Awake();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this);
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Application.runInBackground = true;

        if (!videoPlayer)
            GetVideoPlayer();
        if (scene.name == "VideoScene" && opening)
            StartCoroutine(PlayVideo("Opening"));
        else if (scene.name == "VideoScene" && ending)
            StartCoroutine(PlayVideo("Ending"));
    }

    public void SetOpening(bool value)
    {
        opening = value;
    }

    public void SetEnding(bool value)
    {
        ending = value;
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
        if(opening)
        {
            AudioManager.Instance.PlayAudio(new IPlayAudioChangeScene());
            opening = false;
            SceneManager.LoadScene("ChooseScene");
        }
        else if (ending)
        {
            ending = false;
            StartCoroutine(GameManager.Instance.EndGame(1));
        }
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
