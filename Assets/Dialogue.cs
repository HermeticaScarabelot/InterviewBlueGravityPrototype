using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{

    public string[] lines;
    private int linesIndex = 0;

    public bool canInteract;
    
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.DialogueManagerInstance;
        dialogueManager.dialogueButton.onClick.AddListener(CustomButtonClick);
    }

    public override void Interact()
    {
        base.Interact();
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (dialogueManager.isTyping)
        {
            return;
        }
        if (lines.Length > linesIndex)
        {
            dialogueManager.ResetText();
            dialogueManager.StartCoroutine(dialogueManager.TypeString(lines[linesIndex]));
            linesIndex++;
        }
        else if(linesIndex == lines.Length-1)
        {
            
        }
    }
    
    public void CustomButtonClick()
    {
        Debug.Log("A");
    }


}
