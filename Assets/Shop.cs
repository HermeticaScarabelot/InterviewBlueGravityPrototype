using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    
    [SerializeField] private bool hasDialogue;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] public ItemScriptableObject[] availableItemsForSale;
 
    private ShopManager shopManager;

    private void Awake()
    {
        if (hasDialogue)
        {
            dialogue = GetComponent<Dialogue>();
        }
    }
    
    

    public override void Interact()
    {
        if (hasDialogue)
        {
            return;
        }
        base.Interact();
        ShopTest();
    }

    private void Start()
    {
        shopManager = ShopManager.ShopManagerInstance;
        if (hasDialogue)
        {
            dialogue.OnDialogueFinished += ShopTest;
        }
    }

    void ShopTest()
    {
        shopManager.OpenShop(availableItemsForSale);
    }

    private void OnDisable()
    {
        if (hasDialogue)
        {
            dialogue.OnDialogueFinished -= ShopTest;
        }
    }
}
