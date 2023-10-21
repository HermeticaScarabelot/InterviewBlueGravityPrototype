using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{

    public ItemScriptableObject scriptableItem;
    public ItemScriptableObject.ItemType type;
    public string itemName;
    public Sprite itemSprite;
    
    [SerializeField] private Image image;
    
    
    public SpriteRenderer testPlayerTorsoSprite;
    private void Awake()
    {
        if (transform.childCount > 0 && !image)
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }
    }

    public void UpdateItemSlot(ItemScriptableObject newItem)
    {
        scriptableItem = newItem;
        type = newItem.type;
        itemName = newItem.itemName;
        itemSprite = newItem.itemSprite;
        image.sprite = itemSprite;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClickItem();
        }
    }

    void RightClickItem()
    {
        switch (type)
        {
            case ItemScriptableObject.ItemType.Empty:
                return;
            case ItemScriptableObject.ItemType.Outfit:
                testPlayerTorsoSprite.sprite = scriptableItem.itemSprite;
                Debug.Log("ChangeOutfit");
                break;
        }
    }
    
}
