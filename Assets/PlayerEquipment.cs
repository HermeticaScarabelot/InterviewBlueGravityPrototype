using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment PlayerEquipmentInstance;

    [SerializeField] private EquipmentSlotUI torsoSlotUI;
    [SerializeField] public SpriteRenderer torsoRenderer;
    [SerializeField] public ItemScriptableObject equippedTorso;

    private InventoryManager inventoryManager;

    
    private void Awake()
    {
        PlayerEquipmentInstance = this;
    }

    private void Start()
    {
        inventoryManager = InventoryManager.InventoryManagerInstance;
    }

    public void EquipNewTorso(ItemScriptableObject newTorso, int newTorsoSlotId=0)
    {
        if (equippedTorso)
        {
            inventoryManager.AddItemToSlot(equippedTorso, newTorsoSlotId);
        }
        equippedTorso = newTorso;
        torsoRenderer.sprite = equippedTorso.itemSprite;
        torsoSlotUI.UpdateEquipmentSlot(newTorso);
    }
}
