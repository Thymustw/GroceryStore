using UnityEngine;

public class BattleBackgroundChanger : MonoBehaviour
{
    public Sprite[] bg;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bg[Random.Range(0, bg.Length)];
    }
}
