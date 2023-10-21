using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    
    [SerializeField] private bool hasDialogue;
    private Dialogue dialogue;

    private void Awake()
    {
        if (hasDialogue)
        {
            dialogue = GetComponent<Dialogue>();
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (!hasDialogue)
        {
            ShopTest();
        }
    }

    private void Start()
    {
        if (hasDialogue)
        {
            dialogue.OnDialogueFinished += ShopTest;
        }
    }

    void ShopTest()
    {
        Debug.Log("Shop Test");
    }

    private void OnDisable()
    {
        if (hasDialogue)
        {
            dialogue.OnDialogueFinished -= ShopTest;
        }
    }
}
