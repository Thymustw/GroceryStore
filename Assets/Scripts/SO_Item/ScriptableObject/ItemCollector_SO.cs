using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Getter Data", menuName = "Item/Item Getter Data")]
public class ItemCollector_SO : ScriptableObject
{
    public List<GameObject> itemLibGameobjects;
}