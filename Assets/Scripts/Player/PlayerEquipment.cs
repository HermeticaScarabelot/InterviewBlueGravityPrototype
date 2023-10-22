using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment PlayerEquipmentInstance;

    [Header("Helm")]
    [SerializeField] private EquipmentSlotUI helmSlotUI;
    [SerializeField] public SpriteRenderer helmRenderer;
    [SerializeField] public ItemScriptableObject equippedHelm;

    [Header("Torso")]
    [SerializeField] private EquipmentSlotUI torsoSlotUI;
    [SerializeField] public SpriteRenderer torsoRenderer;
    [SerializeField] public ItemScriptableObject equippedTorso;
    [SerializeField] private Sprite defaultTorso;

    [Header("Legs")]
    [SerializeField] private EquipmentSlotUI legsSlotUI;
    [SerializeField] public SpriteRenderer legsRenderer;
    [SerializeField] public ItemScriptableObject equippedLegs;
    [SerializeField] private Sprite defaultLegs;

    private InventoryManager inventoryManager;

    
    private void Awake()
    {
        PlayerEquipmentInstance = this;
    }

    private void Start()
    {
        inventoryManager = InventoryManager.InventoryManagerInstance;
        //Update Initial Equipment UI
        UpdateHelmUI(equippedHelm);
        UpdateTorsoUI(equippedTorso);
        UpdateLegsUI(equippedLegs);
    }
    
    
    public void EquipNewHelm(ItemScriptableObject newHelm, int newHelmSlotId=0)
    {
        //Always make sure to Retrieve the previous equipped Item before placing the new one on top
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
        //Always make sure to Retrieve the previous equipped Item before placing the new one on top
        if (equippedTorso)
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
        //Always make sure to Retrieve the previous equipped Item before placing the new one on top
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

    public void UnEquipItem(ItemScriptableObject itemToUnEquip)
    {
        switch (itemToUnEquip.outfitType)
        {
            case ItemScriptableObject.OutfitPiece.Helm:
                equippedHelm = null;
                helmRenderer.sprite = null;
                break;
            case ItemScriptableObject.OutfitPiece.Torso:
                equippedTorso = null;
                torsoRenderer.sprite = defaultTorso;
                break;
            case ItemScriptableObject.OutfitPiece.Legs:
                equippedLegs = null;
                legsRenderer.sprite = defaultLegs;
                break;
        }
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
