using UnityEngine;
using UnityEngine.UI;

public class ItemGetter : Singleton<ItemGetter>
{
    private Text textWin;
    private Image image;
    protected override void Awake()
    {
        base.Awake();
        textWin = GameObject.Find("WinText").GetComponent<Text>();
        image = GameObject.Find("ItemImage").GetComponent<Image>();
        GameManager.Instance.RigisterItemGetter(this);
    }

    public void SetItemThing(GameObject item)
    {
        textWin.text = item.name;
        image.sprite = item.gameObject.GetComponent<Image>().sprite;
    }
}
