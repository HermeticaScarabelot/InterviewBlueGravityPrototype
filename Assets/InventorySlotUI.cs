using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{

    public ItemScriptableObject.ItemType type;
    public string itemName;
    public Sprite itemSprite;

    [SerializeField]private Image image;

    private void Awake()
    {
        if (transform.childCount > 0)
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }
    }

    public void UpdateItemSlot(ItemScriptableObject newItem)
    {
        type = newItem.type;
        itemName = newItem.itemName;
        itemSprite = newItem.itemSprite;
        image.sprite = itemSprite;
    }
    
}
