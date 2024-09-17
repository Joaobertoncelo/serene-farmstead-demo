﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObject;
    public Image profileSprite;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowingWindow;
    private int index;
    private string[] sentences;

    public static DialogueController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && isShowingWindow)
        {
            NextSentence();
        }
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObject.SetActive(false);
                isShowingWindow = false;
                sentences = null;
            }
        }
    }

    public void CallSpeech(string[] txt)
    {
        if (!isShowingWindow)
        {
            dialogueObject.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowingWindow = true;
        }
    }
}
