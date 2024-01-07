using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;
    public IPlayAudioBehavior playAudioBehavior { get; set; }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

        AudioClip BGM = Resources.Load("Audio/Game_BGM_Double") as AudioClip;
        audioSource.PlayOneShot(BGM);
    }

    public void PlayAudio(IPlayAudioBehavior audioBehavior)
    {
        playAudioBehavior = audioBehavior;

        AudioClip BGM = Resources.Load(playAudioBehavior.AudioPath()) as AudioClip;
        audioSource.PlayOneShot(BGM);
    }
}
