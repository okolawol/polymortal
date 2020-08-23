using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DialogBoxController : MonoBehaviour
{
    private string[] dialogString;
    private Text textDisplay;
    private string textDisplayString = "";
    private int currentSentenceIndex = 0;
    private bool isSpeaking = false;
    private Animator animator;
    private Image arrowImage;
    private Image portraitImage;
    private float[] textTimeOptions = new float[] { 0.05f, 0.01f };
    private float textTime;
    private Action dialogComplete;

    private void Awake()
    {
        textTime = textTimeOptions[0];
        animator = GetComponent<Animator>();
        textDisplay = Helpers.FindComponentInChildWithTag<Text>(this.gameObject, DialogGlobals.DIALOG_BOX_TEXT_TAG);
        arrowImage = Helpers.FindComponentInChildWithTag<Image>(this.gameObject, DialogGlobals.DIALOG_BOX_ARROW_TAG);
        portraitImage = Helpers.FindComponentInChildWithTag<Image>(this.gameObject, DialogGlobals.DIALOG_BOX_PORTRAIT_TAG);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(string[] dialogString,string characterName, Action dialogComplete)
    {
        this.dialogString = dialogString;
        portraitImage.sprite = Resources.Load<Sprite>(DialogGlobals.CHARACTER_PORTRAIT_PATH + characterName);
        portraitImage.preserveAspect = true;
        this.dialogComplete = dialogComplete;
    }

    //Called from editor click event
    public void speaking()
    {
        if (!isSpeaking)
        {
            textTime = textTimeOptions[0];
            StartCoroutine(speak());
        } else
        {
            //when clicked and currently speaking increase the text speed
            textTime = textTimeOptions[1];
        }
    }

    private IEnumerator speak()
    {
        if (currentSentenceIndex < dialogString.Length)
        {
            arrowImage.enabled = false;
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
                yield return new WaitForSeconds(textTime);
            }
            currentSentenceIndex++;
            isSpeaking = false;
            arrowImage.enabled = true;
        } else
        {
            //dialog over
            animator.SetBool("go_out",true);
        }
    }

    private void destroyObj()
    {
        Destroy(gameObject);
    }

    //called from end of animation event
    public void onDialogComplete()
    {
        dialogComplete?.Invoke();
        destroyObj();
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
