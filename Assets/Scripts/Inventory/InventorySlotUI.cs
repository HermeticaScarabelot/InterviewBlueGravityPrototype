using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] public int slotId;
    [SerializeField] private ItemScriptableObject scriptableItem;
    [SerializeField] private ItemScriptableObject.ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int itemPrice;
    [SerializeField] private TextMeshProUGUI itemDescription;
    
    
    [SerializeField] private Image image;

    private InventoryManager inventoryManager;
    private PlayerEquipment playerEquipment;
    
    private void Awake()
    {

        image = transform.GetChild(0).GetComponent<Image>();
        
    }

    private void Start()
    {
        playerEquipment = PlayerEquipment.PlayerEquipmentInstance;
        inventoryManager = InventoryManager.InventoryManagerInstance;

    }

    public void UpdateItemSlot(ItemScriptableObject newItem)
    {
        image.color = new Color(1, 1, 1, 1);
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
        image.color = new Color(1, 1, 1, 0);
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
                EquipOutfit();
                break;
        }
    }

    void EquipOutfit()
    {
        switch (scriptableItem.outfitType)
        {
            case ItemScriptableObject.OutfitPiece.Helm:
                playerEquipment.EquipNewHelm(scriptableItem, slotId);
                break;
            case ItemScriptableObject.OutfitPiece.Torso:
                playerEquipment.EquipNewTorso(scriptableItem, slotId);
                break;
            case ItemScriptableObject.OutfitPiece.Legs:
                playerEquipment.EquipNewLegs(scriptableItem, slotId);
                break;
        }
        //inventoryManager.RemoveItemByIndex(slotId);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (type == ItemScriptableObject.ItemType.Empty)
        {
            return;
        }
        inventoryManager.UpdateTooltip(scriptableItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (type == ItemScriptableObject.ItemType.Empty)
        {
            return;
        }
        inventoryManager.ResetTooltip();
    }
}
