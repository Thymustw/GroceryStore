using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Diagnostics;
using UnityEngine.TextCore;

public class Dialogue : MonoBehaviour
{
    private GameObject dialogue;
    private DialogueStats dialogueStats;
    private bool isActive = false;
    private bool textFinish = false;
    private bool changeScene = false;

    [Header("UI Object")]
    private Text textLable;
    private Image faceImage;

    [Header("Dialogue")]
    public TextAsset textFile;
    public int indexText;
    public float textSpeed;

    [Header("Sprite")]
    public Sprite faceA, faceB;

    List<string> textList = new List<string>();

    void Awake()
    {
        textLable = GetComponentInChildren<Text>();
        faceImage = GetComponentInChildren<Image>();
        dialogueStats = GetComponent<DialogueStats>();

        int dialogueIndex = GameManager.Instance.GetIndexOfTheCurrentTextAsset();
        textFile = dialogueStats.GetSelectTextAsset(dialogueIndex);

        dialogueIndex+=1;
        GameManager.Instance.SetIndexOfTheCurrentTextAsset(dialogueIndex);

        // Set False.
        dialogue = GameObject.Find("Dialogue");
        dialogue.SetActive(isActive);

        IEnumerator Appear = WaitDialogueSecond(2);
        StartCoroutine(Appear);
    }

    void OnEnable()
    {
        GetTextFromFile(textFile);
        indexText = 0;
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
            IEnumerator disappearAndChangeScene = WaitDialogueSecond(1);
            StartCoroutine(disappearAndChangeScene);
            //SceneManager.LoadScene("BattleScene");
        }
        else if (Input.GetKeyDown(KeyCode.E) && dialogue.activeSelf && textFinish)
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
            textList.Add(line);
    }

    IEnumerator SetTextUI()
    {
        textLable.text="";
        textFinish = false;

        switch(textList[indexText].Trim())
        {
            case "A":
                faceImage.sprite = faceA;
                indexText++;
                break;
            case "B":
                faceImage.sprite = faceB;
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
    }

    IEnumerator WaitDialogueSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isActive = !isActive;
        dialogue.SetActive(isActive);
        if (dialogue.activeSelf)
        {
            IEnumerator firstLine = SetTextUI();
            StartCoroutine(firstLine);
        }

        yield return new WaitForSeconds(1);

        if (changeScene)
            SceneManager.LoadScene("BattleScene");
        //SceneManager.LoadScene("VideoScene");
    }
}
