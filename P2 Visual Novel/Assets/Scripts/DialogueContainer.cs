using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    [SerializeField]
    List<DialogueComponent> lines = new List<DialogueComponent>();

    private GameObject dialogueBox;
    private GameObject nameBox;
    private GameObject character;

    private int cursor;
    private bool firstLine = true;

    private string defaultLine;
    private string defaultTitle;
    private Sprite defaultSprite;

    private bool containsLines = true;

    //[SerializeField]
    //private Trigger trigger;

    public float textSpeed = 0.1f;

    private string text;
    private int textCursor;
    private bool newWord = false;
    private float leftTime;

    private void Start()
    {
        dialogueBox = GameObject.Find("TextBoxText");
        nameBox = GameObject.Find("CharacterText");
        character = GameObject.Find("Character");
    }

    public void GetLine()
    {
        if (firstLine)
        {
            Start();

            defaultTitle = nameBox.GetComponent<TextMeshProUGUI>().text;
            defaultLine = dialogueBox.GetComponent<TextMeshProUGUI>().text;
            defaultSprite = character.GetComponent<Image>().sprite;

            cursor = 0;
            nameBox.GetComponent<TextMeshProUGUI>().text = lines[cursor].GetCharacterName();
            character.GetComponent<Image>().sprite = lines[cursor].GetCharacterSprite();
            ChangeColor();
            firstLine = false;
        }

        if (lines[cursor].GetLineCount() <= 0)
        {
            if (cursor < lines.Count - 1)
            {
                nameBox.GetComponent<TextMeshProUGUI>().text = lines[++cursor].GetCharacterName();
                character.GetComponent<Image>().sprite = lines[cursor].GetCharacterSprite();
                dialogueBox.GetComponent<TextMeshProUGUI>().text = "";
                ChangeColor();
            }
            else
            {
                nameBox.GetComponent<TextMeshProUGUI>().text = defaultTitle;
                character.GetComponent<Image>().sprite = defaultSprite;
                dialogueBox.GetComponent<TextMeshProUGUI>().text = defaultLine;
                ChangeColor(new Color(1, 1, 1));
                containsLines = false;
                return;
            }
        }
        //dialogueBox.GetComponent<TextMeshProUGUI>().text = lines[cursor].GetLine();
        newWord = true;
    }

    private void Update()
    {
        if (newWord)
        {
            text = lines[cursor].GetLine();
            textCursor = 0;
            leftTime = Time.time + textSpeed;
        }

        if (text != null && textCursor < text.Length)
        {
            newWord = false;
            if (leftTime < Time.time)
            {
                leftTime = Time.time + textSpeed;
                dialogueBox.GetComponent<TextMeshProUGUI>().text = text.Substring(0, ++textCursor);
            }
        }

    }

    private void ChangeColor(Color color)
    {
        nameBox.GetComponent<TextMeshProUGUI>().color = color;
        dialogueBox.GetComponent<TextMeshProUGUI>().color = color;
    }

    private void ChangeColor()
    {
        ChangeColor(lines[cursor].GetTextColor());
    }

    public bool ContainsLines()
    {
        return containsLines;
    }
}
