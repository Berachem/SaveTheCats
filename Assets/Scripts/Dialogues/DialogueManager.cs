using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI dialogue;
    public Animator animator;

    public InputActionReference nextDialogueAction; 
    public InputActionReference endDialogueAction;   

    private Queue<string> sentences;
    private bool inDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void OnEnable()
    {
        nextDialogueAction.action.Enable();
        endDialogueAction.action.Enable();
    }

    private void OnDisable()
    {
        nextDialogueAction.action.Disable();
        endDialogueAction.action.Disable();
    }

    private void Update()
    {
        if (inDialogue)
        {
            if (nextDialogueAction.action.WasPressedThisFrame())
            {
                DisplayNextSentence();
            }
          
            else if (endDialogueAction.action.WasPressedThisFrame())
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        inDialogue = true;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        name.text = dialogue.name + " : ";

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
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
    }

    public bool isInDialogue()
    {
        return inDialogue;
    }
}
