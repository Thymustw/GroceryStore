using System.Collections.Generic;
using UnityEngine;

public class ChooseBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.GetChooseBox().Count == 0)
        {
            GameManager.Instance.SetChooseBox(SetBox());
        }

        int index = 0;
        foreach(var temp in GameManager.Instance.GetChooseBox())
        {
            transform.GetChild(index).GetComponent<SpriteRenderer>().color = temp;
            index++;
        }
    }

    void OnDestroy() 
    {
        GameManager.Instance.SetChooseBox(SetBox());
    }

    List<Color> SetBox()
    {
        List<Color> tempList = new List<Color>();
        for(int i = 0; i < transform.childCount; i++)
        {
            Color tempColor = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            tempList.Add(tempColor);
        }
        return tempList;
    }
}
