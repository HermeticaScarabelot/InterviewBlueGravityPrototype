using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{
    public delegate void DialogueFinishedEventHandler();
    public event DialogueFinishedEventHandler OnDialogueFinished;
    
    
    [SerializeField] public string[] lines;
    private int linesIndex = 0;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.DialogueManagerInstance;
 
    }

    public override void Interact()
    {
        base.Interact();
        NextLine();
    }

    public void NextLine()
    {
        if (dialogueManager.isTyping)
        {
            return;
        }
        
        if (lines.Length > linesIndex)
        {
            dialogueManager.ResetText();
            dialogueManager.StartCoroutine(dialogueManager.TypeString(lines[linesIndex])); //Display Text Char by Char
            linesIndex++;
            Debug.Log(linesIndex + " / " + lines.Length );
        }
        else if(linesIndex == lines.Length) //Call all Listeners, used to call Interactions like the Shop opening after the initial Dialogue
        {
            OnDialogueFinished?.Invoke();
            dialogueManager.CloseDialogue();
            linesIndex = 0;
            //ShopManager.ShopManagerInstance.OpenShop();
        }
    }



}
