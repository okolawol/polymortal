using System;
using UnityEngine;

public class DialogBox : Occurrence
{
    private string[] dialogString;
    private string characterName;
    private GameObject canvas;
    private GameObject dialogBoxPrefab;

    public DialogBox(string[] dialogString, string characterName, GameObject canvas, GameObject prefab)
    {

        this.dialogString = dialogString;
        this.characterName = characterName;
        this.canvas = canvas;
        dialogBoxPrefab = prefab;
    }

    private void playDialog(Action onComplete)
    {
        try
        {
            GameObject instance = GameObject.Instantiate<GameObject>(dialogBoxPrefab);
            DialogBoxController instanceController = instance.GetComponent<DialogBoxController>();
            instanceController.initialize(dialogString, characterName, onComplete);
            instance.transform.SetParent(canvas.transform, false);
            instanceController.speaking();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

    }

    public override void execute(Action onOccurrenceComplete)
    {
        playDialog(onOccurrenceComplete);
    }
}
