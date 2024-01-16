using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    private GameObject dialogue;
    private DialogueStats dialogueStats;
    private bool isActive = false;
    private bool textFinish = false;
    private bool changeScene = false;

    private Animator grandMaAnim, grandPaAnim, kidAnim;
    private bool isGrandMaAnim = false, isGrandPaAnim = false, isKidAnim = false;

    private GameObject AnimaList;

    [Header("UI Object")]
    private Text textLable;
    //private Image faceImage;

    [Header("Dialogue")]
    public TextAsset textFile;
    public int indexText;
    public float textSpeed;

    [Header("Sprite")]
    //public Sprite faceA, faceB, faceC;

    List<string> textList = new List<string>();

    void Awake()
    {
        textLable = GetComponentInChildren<Text>();
        dialogueStats = GetComponent<DialogueStats>();

        textFile = GameManager.Instance.GetOutputTextAsset();

        // Set False.
        dialogue = GameObject.Find("Dialogue");
        dialogue.SetActive(isActive);
    }

    void OnEnable()
    {
        grandMaAnim = GameObject.Find("AnimaList").transform.GetChild(0).GetComponent<Animator>();
        grandMaAnim.gameObject.SetActive(isGrandMaAnim);
        grandPaAnim = GameObject.Find("AnimaList").transform.GetChild(1).GetComponent<Animator>();
        grandPaAnim.gameObject.SetActive(isGrandPaAnim);
        kidAnim = GameObject.Find("AnimaList").transform.GetChild(2).GetComponent<Animator>();
        kidAnim.gameObject.SetActive(isKidAnim);

        GetTextFromFile(textFile);
        indexText = 0;

        IEnumerator Appear = WaitDialogueSecond(2);
        StartCoroutine(Appear);
        
        //textLable.text = textList[indexText];
        //indexText++;
        //StartCoroutine(WaitSecond(10));

        //IEnumerator firstLine = SetTextUI();
        //StartCoroutine(firstLine);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && indexText == textList.Count && !changeScene)
        {
            changeScene = true;
            isGrandMaAnim = false;
            isGrandPaAnim = false;
            isKidAnim = false;

            IEnumerator disappearAndChangeScene = WaitDialogueSecond(1);
            StartCoroutine(disappearAndChangeScene);
            //SceneManager.LoadScene("BattleScene");
        }
        else if (Input.GetKeyDown(KeyCode.E) && indexText < textList.Count && textFinish)
        {
            IEnumerator nextLine = SetTextUI();
            StartCoroutine(nextLine);
        }
    }

    void GetTextFromFile(TextAsset textFile)
    {
        textList.Clear();
        indexText = 0;

        var lineData = textFile.text.Split('\n');

        foreach (var line in lineData)
        {
            switch(line.Trim())
            {
                case "阿嬤":
                    isGrandMaAnim = true;
                    break;
                case "阿公":
                    isGrandPaAnim = true;
                    break;
                case "橘皮":
                    isKidAnim = true;
                    break;
            }

            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textLable.text="";
        textFinish = false;

        switch(textList[indexText].Trim())
        {
            case "阿嬤":
                StopAnim();
                grandMaAnim.Play("Speak");
                indexText++;
                break;
            case "阿公":
                StopAnim();
                grandPaAnim.Play("Speak");
                indexText++;
                break;
            case "橘皮":
                StopAnim();
                kidAnim.Play("Speak");
                indexText++;
                break;
        }

        string sentenceLine = "";
        for (int i = 0; i < textList[indexText].Length; i++)
        {
            sentenceLine += textList[indexText][i];
            textLable.text = textList[indexText - 1] + "：\n\t" + sentenceLine;

            yield return new WaitForSeconds(textSpeed);
        }

        indexText++;
        textFinish = true;

        // yield return new WaitForSeconds(0.05f);
        StopAnim();
    }

    IEnumerator WaitDialogueSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        isActive = !isActive;
        dialogue.SetActive(isActive);

        SetAnimEnable();
        StopAnim();

        if (dialogue.activeSelf)
        {
            IEnumerator firstLine = SetTextUI();
            StartCoroutine(firstLine);
        }

        yield return new WaitForSeconds(1);
        
        if (changeScene)
        {
            AudioManager.Instance.PlayAudio(new IPlayAudioChangeScene());
            SceneManager.LoadScene("BattleScene");
        }
        //SceneManager.LoadScene("VideoScene");
    }

    void SetAnimEnable()
    {
        grandMaAnim.gameObject.SetActive(isGrandMaAnim);
        grandPaAnim.gameObject.SetActive(isGrandPaAnim);
        kidAnim.gameObject.SetActive(isKidAnim);
    }

    void StopAnim()
    {
        if(grandMaAnim.gameObject.activeSelf)
            grandMaAnim.Play("Idle");
        if(grandPaAnim.gameObject.activeSelf)
            grandPaAnim.Play("Idle");
        if(kidAnim.gameObject.activeSelf)
            kidAnim.Play("Idle");
    }
}
