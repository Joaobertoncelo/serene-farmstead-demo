using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings currentDialogue;

    bool playerHit;

    private List<string> sentences = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        GetNPCInfo();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueController.instance.CallSpeech(sentences.ToArray());
        }
    }

    void GetNPCInfo()
    {
        for(int i = 0; i< currentDialogue.dialogues.Count; i++)
        {
            sentences.Add(currentDialogue.dialogues[i].language.portuguese);
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        if (hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
