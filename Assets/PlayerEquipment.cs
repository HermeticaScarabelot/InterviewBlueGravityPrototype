using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment PlayerEquipmentInstance;
    
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

    public void EquipNewTorso(ItemScriptableObject newTorso, int newTorsoSlotId)
    {
        inventoryManager.AddItemToSlot(equippedTorso, newTorsoSlotId);
        equippedTorso = newTorso;
        torsoRenderer.sprite = equippedTorso.itemSprite;
    }
}
