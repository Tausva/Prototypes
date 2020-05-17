using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> dialogues;

    public Sprite tamsa;
    public Sprite lova;

    private int cursor = -1;
    private GameObject component;

    public void PlayNext()
    {
        if (cursor >= dialogues.Count)
        {
            return;
        }
        else if (cursor < 0 || !component.GetComponent<DialogueContainer>().ContainsLines())
        {
            component = Instantiate(dialogues[++cursor]);
        }

        //Slyksti vieta kode
        if (cursor == 4)
        {
            GetComponent<Image>().sprite = tamsa;
        }
        else if (cursor == 5)
        {
            GetComponent<Image>().sprite = lova;
        }
        //aaaaaaaaaaaa kaip baisu, bet tai biagiasi

        component.GetComponent<DialogueContainer>().GetLine();
    }
}
