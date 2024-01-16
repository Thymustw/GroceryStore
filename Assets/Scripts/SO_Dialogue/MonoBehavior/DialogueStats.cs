using UnityEngine;

public class DialogueStats : MonoBehaviour
{
    public DialogueData_SO dialogueData;


    #region "Read from DialogueData_SO"
    public TextAsset GetSelectTextAsset(int index)
    {
        if (dialogueData != null)
            return dialogueData.dialogueFileList[index];
        else return null;
    }
    #endregion
}
