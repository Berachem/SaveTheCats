using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI dialogue;
    public Animator animator;
    public CustomFirstPersonController FPVcamera;


    private Queue<string> sentences;
    private bool inDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (inDialogue)
        {
            if (Input.GetKeyDown("c"))
            {
                DisplayNextSentence();
            }
            else if (Input.GetKeyDown("x"))
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        inDialogue = true;
        FPVcamera.lockMovement();

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        name.text = dialogue.name + " : ";

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogue.text = sentence;

    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        inDialogue = false;
        FPVcamera.unlockMovement();
    }

    public bool isInDialogue()
    {
        return inDialogue;
    }
}
