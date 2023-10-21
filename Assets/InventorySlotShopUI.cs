using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotShopUI : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject inventorySlotShopItem;
    [SerializeField] public int slotId;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;

    private Image image;
    private Sprite defaultSpriteBg;
    
    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        defaultSpriteBg = image.sprite;
    }


    public void LoadItem(InventoryManager inventoryManager)
    {
        if (inventoryManager.inventoryItems[slotId] == null)
        {
            image.sprite = defaultSpriteBg;
            Debug.Log("empty slot");
        }
        else
        {
            inventorySlotShopItem = inventoryManager.inventoryItems[slotId];
            image.sprite = inventorySlotShopItem.itemSprite;
        }
    }
    
}
