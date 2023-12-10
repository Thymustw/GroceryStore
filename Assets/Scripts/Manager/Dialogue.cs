using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Dialogue : MonoBehaviour
{
    private GameObject dialogue;
    private bool isActive = false;
    private bool changeScene = false;

    [Header("UI Object")]
    private Text textLable;
    private Image image;

    [Header("Dialogue")]
    public TextAsset textFile;
    public int indexText;

    List<string> textList = new List<string>();

    void Awake()
    {
        textLable = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();

        dialogue = GameObject.Find("Dialogue");
        dialogue.SetActive(isActive);
        StartCoroutine(WaitDialogueSecond(2));
    }

    void OnEnable()
    {
        GetTextFromFile(textFile);
        indexText = 0;
        textLable.text = textList[indexText];
        indexText++;
        //StartCoroutine(WaitSecond(10));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogue.activeSelf && indexText != textList.Count)
        {
            textLable.text = textList[indexText];
            if (indexText++ == (textList.Count - 1))
            {
                changeScene = true;
                StartCoroutine(WaitDialogueSecond(1));
                //SceneManager.LoadScene("BattleScene");
            }
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

    IEnumerator WaitDialogueSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isActive = !isActive;
        dialogue.SetActive(isActive);
        if (changeScene)
            SceneManager.LoadScene("BattleScene");
        //SceneManager.LoadScene("VideoScene");
    }
}
