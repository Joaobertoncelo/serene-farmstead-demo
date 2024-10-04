using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom selectIdiom;
 
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
    private string[] currentActorName;
    private Sprite[] currentActorProfile;

    private Player player;
    public static DialogueController instance;

    public bool IsShowingWindow { get => isShowingWindow; set => isShowingWindow = value; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && IsShowingWindow)
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
                profileSprite.sprite = currentActorProfile[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObject.SetActive(false);
                IsShowingWindow = false;
                sentences = null;
                player.isPaused = false;
            }
        }
    }

    public void CallSpeech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if (!IsShowingWindow)
        {
            dialogueObject.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            currentActorProfile = actorProfile;
            profileSprite.sprite = currentActorProfile[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            IsShowingWindow = true;
            player.isPaused = true;
        }
    }
}
