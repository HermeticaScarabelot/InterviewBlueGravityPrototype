using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{

    public int slotId;
    public ItemScriptableObject scriptableItem;
    public ItemScriptableObject.ItemType type;
    public string itemName;
    public Sprite itemSprite;
    
    [SerializeField] private Image image;

    private InventoryManager inventoryManager;
    
    
    public SpriteRenderer testPlayerTorsoSprite;
    private void Awake()
    {
        if (transform.childCount > 0 && !image)
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }
    }

    private void Start()
    {
        inventoryManager = InventoryManager.InventoryManagerInstance;
    }

    public void UpdateItemSlot(ItemScriptableObject newItem)
    {
        scriptableItem = newItem;
        type = newItem.type;
        itemName = newItem.itemName;
        itemSprite = newItem.itemSprite;
        image.sprite = itemSprite;
    }

    public void ResetItemSlot()
    {
        scriptableItem = null;
        type = ItemScriptableObject.ItemType.Empty;
        itemName = "";
        itemSprite = null;
        image.sprite = null;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            HoldItem();
        }
        
    }

    public void HoldItem()
    {
        if (!inventoryManager.holdingItem && type != ItemScriptableObject.ItemType.Empty)
        {
            inventoryManager.holdingItem = true;
            inventoryManager.heldItemSlotId = slotId;
        }
        else if(inventoryManager.holdingItem)
        {
            Debug.Log(slotId);
            inventoryManager.SwapItemPosition(slotId);
        }
    }



    void UseItem()
    {
        switch (type)
        {
            case ItemScriptableObject.ItemType.Empty:
                return;
            case ItemScriptableObject.ItemType.Outfit:
                testPlayerTorsoSprite.sprite = scriptableItem.itemSprite;
                inventoryManager.RemoveItemByIndex(slotId);
                Debug.Log("ChangeOutfit");
                break;
        }
    }
    
}
