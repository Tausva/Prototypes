using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueComponent
{
    [SerializeField]
    private string characterName;

    [SerializeField]
    private Color textColor = new Color(1, 1, 1, 1);

    [SerializeField]
    private Sprite characterSprite;

    [SerializeField]
    [TextArea(3, 10)]
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

    public int GetLineCount()
    {
        return dialogueLines.Count;
    }

    public Color GetTextColor()
    {
        return textColor;
    }

    public Sprite GetCharacterSprite()
    {
        return characterSprite;
    }
}
