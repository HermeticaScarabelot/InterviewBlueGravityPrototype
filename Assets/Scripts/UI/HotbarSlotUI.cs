using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarSlotUI : MonoBehaviour, IPointerClickHandler
{
    public int slotId;
    public ItemScriptableObject scriptableItem;
    public ItemScriptableObject.ItemType type;
    public string itemName;
    public Sprite itemSprite;
    
    [SerializeField] private Image image;
    [SerializeField] private Sprite defaultSprite;

    private InventoryManager inventoryManager;
    private PlayerEquipment playerEquipment;


    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        defaultSprite = image.sprite;
        inventoryManager = InventoryManager.InventoryManagerInstance;

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PlaceItemInHotbar();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RemoveItemFromHotbar();
        }
        
        
        
    }

    public void PlaceItemInHotbar()
    {
        if (inventoryManager.holdingItem && scriptableItem == null)
        {
            scriptableItem = inventoryManager.inventoryItems[inventoryManager.heldItemSlotId];
            image.sprite = scriptableItem.itemSprite;
            inventoryManager.holdingItem = false;
            inventoryManager.RemoveItemByIndex(inventoryManager.heldItemSlotId);
            inventoryManager.heldItemSlotId = 0;
        }
        
    }

    public void RemoveItemFromHotbar()
    {
        if (scriptableItem == null)
        {
            return;
        }

        inventoryManager.PickupItem(scriptableItem);
        scriptableItem = null;
        image.sprite = defaultSprite;
        

    }
}
