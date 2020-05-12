using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TempScript : MonoBehaviour
{
   // [SerializeField]
    //List<DialogueComponent> lines = new List<DialogueComponent>();
    public DialogueComponent lines;
    [SerializeField]
    private GameObject text;

    public void PressButton()
    {
        text.GetComponent<TextMeshProUGUI>().text = lines.GetLine();
    }

}
