using UnityEngine;
using UnityEngine.UI;

public class ItemGetter : Singleton<ItemGetter>
{
    private Text textWin;
    protected override void Awake()
    {
        base.Awake();
        textWin = GameObject.Find("WinText").GetComponent<Text>();
        GameManager.Instance.RigisterItemGetter(this);
    }

    public void SetText(GameObject item)
    {
        textWin.text = item.name;
    }
}
