using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueComponent
{
    [SerializeField]
    private string characterName;
    
    [SerializeField]
    private List<string> dialogueLines = new List<string>();

    public string GetLine()
    {
        if (dialogueLines.Count > 0)
        {
            string dialogueLine = dialogueLines[0];
            dialogueLines.RemoveAt(0);
            return dialogueLine;
        }
        return null;
    }

    public string GetCharacterName()
    {
        return characterName;
    }
}
