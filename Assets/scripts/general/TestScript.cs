using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{
    private int occurenceIndex = 0;
    private DialogBox[] occurences;
    private Action onOccurenceComplete;
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag(UiGlobals.UI_CANVAS_TAG);
        GameObject dialogBoxPrefab = Resources.Load<GameObject>(DialogGlobals.DIALOG_BOX_PREFAB);
        onOccurenceComplete = delegate ()
        {
            occurenceComplete();
        };

        occurences = new DialogBox[] {
            new DialogBox(new string[] {
                    "What is your name? and who is there?",
                    "Who are you?",
                    "Why are you here?"
                },CharacterNames.XENON,canvas,dialogBoxPrefab
            ),
            new DialogBox(new string[] {
                    "I am your worst nightmare",
                    "and you must fear me!",
                },CharacterNames.MAINGEL,canvas,dialogBoxPrefab
            ),
            new DialogBox(new string[] {
                    "Beep! boop! I am robot!",
                    "can not compute! can not compute!",
                    "shutting dowwwwwn..bzzzzzt"
                },CharacterNames.CE177,canvas,dialogBoxPrefab
            )
        };
        occurences[occurenceIndex].playDialog(onOccurenceComplete);
    }

    private void occurenceComplete()
    {
        occurenceIndex += 1;
        if (occurenceIndex < occurences.Length)
        {
            occurences[occurenceIndex].playDialog(onOccurenceComplete);
        } else
        {
            //invoke on scene finished
            Debug.Log("Scene COMPLETE.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
