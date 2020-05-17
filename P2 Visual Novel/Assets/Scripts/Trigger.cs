using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueComponent;

    [SerializeField]
    private bool timedTrigger;

    [SerializeField]
    private float secondsToPass;

    // Update is called once per frame
    void Update()
    {
        if (timedTrigger)
        {
            secondsToPass -= Time.deltaTime;
        }

        if (secondsToPass <= 0)
        {
            timedTrigger = false;
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        Instantiate(dialogueComponent);
    }

    public void TriggerDialogue(GameObject dialogueComponent)
    {
        Instantiate(dialogueComponent);
    }
}
