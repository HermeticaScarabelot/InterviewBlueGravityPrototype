using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PurchasableSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private ItemScriptableObject purchasableItem;
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
        image = transform.GetChild(0).GetComponent<Image>();
        defaultSpriteBg = image.sprite;
    }

    private void Start()
    {
        shopManager = ShopManager.ShopManagerInstance;
    }

    public void LoadItem(ItemScriptableObject itemToLoad)
    {
        //Load each item from each Shop Slot
        if (itemToLoad == null)
        {
            image.sprite = defaultSpriteBg;
        }
        else
        {
            purchasableItem = itemToLoad;
            image.sprite = itemToLoad.itemSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (purchasableItem == null || shopManager.selectedItem == true)
        {
            return;
        }
        shopManager.tooltipSlotUI.UpdateTooltipSlot(purchasableItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Make sure to select the Item so the ShopManager can have a function to buy any Selected Item
            shopManager.SelectItem(purchasableItem, slotId);
            shopManager.tooltipSlotUI.UpdateTooltipSlot(purchasableItem);
        }
    }
}
