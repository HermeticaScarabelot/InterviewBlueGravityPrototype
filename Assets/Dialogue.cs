using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : Interactable
{
    public delegate void DialogueFinishedEventHandler();
    public event DialogueFinishedEventHandler OnDialogueFinished;
    
    
    [SerializeField] public string[] lines;
    private int linesIndex = 0;
    [SerializeField] private GameObject interactTextGo;

    
    private DialogueManager dialogueManager;
    private float distanceFromPlayer;

    private void Start()
    {
        dialogueManager = DialogueManager.DialogueManagerInstance;
 
    }

    public override void Interact()
    {
        base.Interact();
        interactedRecently = true;
        NextLine();
    }

    private void Update()
    {
        distanceFromPlayer = Vector2.Distance(dialogueManager.playerGo.transform.position, transform.position);
        UpdateInteractText();
        
        if (interactedRecently)
        {
            Debug.Log(distanceFromPlayer);
            if (distanceFromPlayer > 2)
            {
                interactedRecently = false;
                linesIndex = 0;
                dialogueManager.CloseDialogue();
            }
        }
    }

    void UpdateInteractText()
    {
        if (distanceFromPlayer > 2 && interactTextGo.activeSelf)
        {
            interactTextGo.SetActive(false);
        }
        else if(distanceFromPlayer < 2 && !interactTextGo.activeSelf)
        {   
            interactTextGo.SetActive(true);
        }
    }

    public void NextLine()
    {
        if (dialogueManager.isTyping || dialogueManager.shopManager.shopGo.activeSelf)
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
