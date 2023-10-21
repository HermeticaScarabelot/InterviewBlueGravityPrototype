using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedOutfit : Interactable
{


    [SerializeField] private ItemScriptableObject itemScriptableObject;
    [SerializeField] private Sprite outfitSprite;
    
    private InventoryManager inventoryManager;

    private void Start()
    {
        if (!inventoryManager)
        {
            inventoryManager = InventoryManager.InventoryManagerInstance;
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (inventoryManager.PickupItem(itemScriptableObject))
        {
            Destroy(this.gameObject);
        };
    }
}
