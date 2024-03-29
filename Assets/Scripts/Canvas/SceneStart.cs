using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    Button buttonChangeScene;
    void Awake()
    {
        buttonChangeScene = transform.GetChild(1).GetComponent<Button>();
        buttonChangeScene.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        AudioManager.Instance.PlayAudio(new IPlayAudioStart());
        VideoManager.Instance.SetOpening(true);
        SceneManager.LoadScene("VideoScene");
    }
}
