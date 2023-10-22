using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public enum EquipmentSlotType
    {
        Helm,
        Torso,
        Legs
    }
    
    [SerializeField] private EquipmentSlotType slotType;
    [SerializeField] private Sprite equipmentSprite;
    [SerializeField] private ItemScriptableObject equippedItem;
    
    [SerializeField] private Image image;

    [SerializeField]private InventoryManager inventoryManager;

    private void Awake()
    {
        if (!image)
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }

        if (equippedItem)
        {
            image.sprite = equipmentSprite;
        }
    }

    private void Start()
    {
        if (!inventoryManager)
        {
            inventoryManager = InventoryManager.InventoryManagerInstance;
            
        }

    }

    public void UpdateEquipmentSlot(ItemScriptableObject newItem)
    {
        equipmentSprite = newItem.itemSprite;
        equippedItem = newItem;
        image.sprite = equipmentSprite;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedItem == null)
        {
            return;
        }
        inventoryManager.UpdateTooltip(equippedItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedItem == null)
        {
            return;
        }
        inventoryManager.ResetTooltip();
    }
    
}
