using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private void Awake() 
    {
        Transform parent = GameObject.Find("EnemyPool").transform;

        JsonReader.Instance.GetCurrentEnemyPool(GameManager.Instance.GetIndexOfTheEnemyPool(), parent);
        GameManager.Instance.SetIndexOfTheEnemyPool(GameManager.Instance.GetIndexOfTheEnemyPool() + 1);
        // for (int i = 0; i < 3; i++)
        // {
        //     Vector2 tempPosition = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-3.5f, 3.5f));
        //     while (Physics2D.OverlapCircle(transform.position, 2f, 1<<7))
        //     {
        //         tempPosition = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-3.5f, 3.5f));
        //     }
        //     var tempObj = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Enemy/Enemy"), parent) as GameObject;
        //     tempObj.transform.position = tempPosition;
        // }
    }
}
