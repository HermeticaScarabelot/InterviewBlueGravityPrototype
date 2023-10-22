using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotShopUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private ItemScriptableObject inventorySlotShopItem;
    [SerializeField] public int slotId;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;

    private Image image;
    private Sprite defaultSpriteBg;
    
    [SerializeField] private ShopManager shopManager;
    
    private void Awake()
    {
        //Always get the Image from the Child Object to make sure the UI Slot still have a Background under the Item Sprite
        image = transform.GetChild(0).GetComponent<Image>();
        defaultSpriteBg = image.sprite;
    }

    private void Start()
    {
        shopManager = ShopManager.ShopManagerInstance;
    }

    public void LoadItem(InventoryManager inventoryManager)
    {
        //If slot is empty, leave it with the default 
        if (inventoryManager.inventoryItems[slotId] == null)
        {
            image.sprite = defaultSpriteBg;
        }
        else
        {
            //Place the correct Item in the correct Slot
            inventorySlotShopItem = inventoryManager.inventoryItems[slotId];
            image.sprite = inventorySlotShopItem.itemSprite;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventorySlotShopItem == null || shopManager.selectedItem == true)
        {
            return;
        }
        shopManager.tooltipSlotUI.UpdateTooltipSlot(inventorySlotShopItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            shopManager.SelectItem(inventorySlotShopItem, slotId);
            shopManager.tooltipSlotUI.UpdateTooltipSlot(inventorySlotShopItem);
        }
    }
}
