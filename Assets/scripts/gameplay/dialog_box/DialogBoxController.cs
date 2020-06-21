using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogBoxController : MonoBehaviour
{
    private string[] dialogString;
    private Text textDisplay;
    private string textDisplayString = "";
    private int currentSentenceIndex = 0;
    private bool isSpeaking = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        textDisplay = Helpers.FindComponentInChildWithTag<Text>(this.gameObject,DialogGlobals.DIALOG_BOX_TEXT_TAG);
        initialize(new string[] {
            "What is your name?",
            "Who are you?",
            "Why are you here?"
        });
        speaking();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(string[] dialogString)
    {
        this.dialogString = dialogString;
    }

    public void speaking()
    {
        if (!isSpeaking)
        {
            StartCoroutine(speak());
        }
    }

    private IEnumerator speak()
    {
        if (currentSentenceIndex < dialogString.Length)
        {
            isSpeaking = true;
            textDisplayString = "";
            textDisplay.text = textDisplayString;
            string sentence = dialogString[currentSentenceIndex];
            //display each character
            for (int j = 0; j < sentence.Length; j++)
            {
                char character = sentence[j];
                textDisplayString = textDisplayString + character;
                textDisplay.text = textDisplayString;
                //delay between each character
                yield return new WaitForSeconds(0.05f);
            }
            currentSentenceIndex++;
            isSpeaking = false;
        } else
        {
            //dialog over
            animator.SetBool("go_out",true);
        }
    }

    private IEnumerator speakAll()
    {
        //display each sentence
        for (int i=0; i < dialogString.Length; i++)
        {
            string sentence = dialogString[i];
            //display each character
            for (int j=0; j < sentence.Length; j++)
            {
                char character = sentence[j];
                textDisplayString = textDisplayString + character;
                textDisplay.text = textDisplayString;
                //delay between each character
                yield return new WaitForSeconds(0.05f);
            }
            //delay between each sentence
            yield return new WaitForSeconds(2f);
            //prepare for next sentence
            textDisplayString = "";
            textDisplay.text = textDisplayString;
        }
    }
}
