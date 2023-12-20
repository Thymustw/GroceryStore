using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue Stats/Dialogue Data")]
public class DialogueData_SO : ScriptableObject
{
    public List<TextAsset> dialogueFileList;
}
