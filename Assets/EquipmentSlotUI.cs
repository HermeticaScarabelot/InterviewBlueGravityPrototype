using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerEquipment playerEquipment;

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
        defaultSprite = GetComponent<Image>().sprite;
    }

    private void Start()
    {
        if (!inventoryManager)
        {
            inventoryManager = InventoryManager.InventoryManagerInstance;
        }

        if (!playerEquipment)
        {
            playerEquipment = PlayerEquipment.PlayerEquipmentInstance;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (equippedItem == null)
            {
                return;
            }
            UnEquipItem();
        }
    }

    void UnEquipItem()
    {
        inventoryManager.PickupItem(equippedItem);
        playerEquipment.UnEquipItem(equippedItem);
        equippedItem = null;
        image.sprite = defaultSprite;
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
