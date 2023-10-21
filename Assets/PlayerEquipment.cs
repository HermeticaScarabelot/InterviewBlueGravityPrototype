using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment PlayerEquipmentInstance;

    [SerializeField] private EquipmentSlotUI helmSlotUI;
    [SerializeField] public SpriteRenderer helmRenderer;
    [SerializeField] public ItemScriptableObject equippedHelm;
    
    [SerializeField] private EquipmentSlotUI torsoSlotUI;
    [SerializeField] public SpriteRenderer torsoRenderer;
    [SerializeField] public ItemScriptableObject equippedTorso;
        
    [SerializeField] private EquipmentSlotUI legsSlotUI;
    [SerializeField] public SpriteRenderer legsRenderer;
    [SerializeField] public ItemScriptableObject equippedLegs;

    private InventoryManager inventoryManager;

    
    private void Awake()
    {
        PlayerEquipmentInstance = this;
    }

    private void Start()
    {
        inventoryManager = InventoryManager.InventoryManagerInstance;
        UpdateHelmUI(equippedHelm);
        UpdateTorsoUI(equippedTorso);
        UpdateLegsUI(equippedLegs);
    }
    
    
    public void EquipNewHelm(ItemScriptableObject newHelm, int newHelmSlotId=0)
    {
        if (equippedHelm)
        {
            inventoryManager.AddItemToSlot(equippedHelm, newHelmSlotId);
            inventoryManager.UpdateTooltip(equippedHelm);
        }
        else
        {
            inventoryManager.UpdateTooltip(null);
            inventoryManager.RemoveItemByIndex(newHelmSlotId);
        }
        UpdateHelmUI(newHelm);

    }
    
    
    public void EquipNewTorso(ItemScriptableObject newTorso, int newTorsoSlotId=0)
    {
        if (equippedTorso)//If the player already have a Torso Equipped, the Current equipped torso will be moved to the Inventory
        {
            inventoryManager.AddItemToSlot(equippedTorso, newTorsoSlotId);
            inventoryManager.UpdateTooltip(equippedTorso);
        } else
        {
            inventoryManager.RemoveItemByIndex(newTorsoSlotId);
            inventoryManager.UpdateTooltip(null);
        }
        UpdateTorsoUI(newTorso);
    }

    
    public void EquipNewLegs(ItemScriptableObject newLegs, int newLegsSlotId=0)
    {
        if (equippedLegs)
        {
            inventoryManager.AddItemToSlot(equippedLegs, newLegsSlotId);
            inventoryManager.UpdateTooltip(equippedLegs);
        }
        else
        {
            inventoryManager.UpdateTooltip(null);
            inventoryManager.RemoveItemByIndex(newLegsSlotId);
        }
        UpdateLegsUI(newLegs);

    }

    void UpdateHelmUI(ItemScriptableObject helm)
    {
        if (helm == null)
        {
            return;
        }
        equippedHelm = helm;
        helmRenderer.sprite = equippedHelm.itemSprite;
        helmSlotUI.UpdateEquipmentSlot(helm);
    }
    void UpdateTorsoUI(ItemScriptableObject torso)
    {
        if (torso == null)
        {
            return;
        }
        equippedTorso = torso;
        torsoRenderer.sprite = equippedTorso.itemSprite;
        torsoSlotUI.UpdateEquipmentSlot(torso);
    }
    void UpdateLegsUI(ItemScriptableObject legs)
    {
        if (legs == null)
        {
            return;
        }
        equippedLegs = legs;
        legsRenderer.sprite = equippedLegs.itemSprite;
        legsSlotUI.UpdateEquipmentSlot(legs);
    }
}
