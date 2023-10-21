using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager InventoryManagerInstance;

    [SerializeField] private GameObject inventoryGo;
    [SerializeField] private GameObject inventoryPanelGo;
    [SerializeField] private InventorySlotUI[] inventorySlotsUI = new InventorySlotUI[0];
    
    public ItemScriptableObject[] inventoryItems = new ItemScriptableObject[0];

    public int heldItemSlotId;
    public bool holdingItem;

    [SerializeField] private TooltipSlotUI tooltipSlotUI;
    
    //public List<ScriptableObject> inventoryItems = new List<ScriptableObject>();

    private void Awake()
    {
        if (!InventoryManagerInstance)
        {
            InventoryManagerInstance = this;
        }
        
        if (inventoryPanelGo)
        {
            inventorySlotsUI = inventoryPanelGo.GetComponentsInChildren<InventorySlotUI>(); //Get all 24 Slots, then automatically set the Id on each one
            for (int i = 0; i < inventorySlotsUI.Length; i++)
            {
                inventorySlotsUI[i].slotId = i;
            }
            inventoryItems = new ItemScriptableObject[inventorySlotsUI.Length]; //Initialize the Inventory Items with null
        }
        Invoke("CloseInventory",0.1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
        
    }

    public void OpenInventory()
    {
        inventoryGo.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryGo.SetActive(false);
    }

    public bool PickupItem(ItemScriptableObject item)
    {
        for (int i = 0; i < inventoryItems.Length; i++) //Search for a Empty Slot, then place the Picked item in this Slot
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item;
                inventorySlotsUI[i].UpdateItemSlot(item);
                return true;
            }
        }
        return false;
    }

    public void AddItemToSlot(ItemScriptableObject item, int slotId) //Add Item to specific slot
    {
        inventoryItems[slotId] = item;
        inventorySlotsUI[slotId].UpdateItemSlot(item);
    }

    public void SwapItemPosition(int secondSlotID)
    {
        ItemScriptableObject firstItem = inventoryItems[heldItemSlotId];
        ItemScriptableObject secondItem = inventoryItems[secondSlotID];

        RemoveItemByIndex(heldItemSlotId);//Remove First item
        
        if (secondItem != null) //If second Slot isn't a empty Space, temporarily Remove it, then Add it to the First Slot
        {
            RemoveItemByIndex(secondSlotID); 
            AddItemToSlot(secondItem, heldItemSlotId); //Add second item to first slot ;

        }
        
        AddItemToSlot(firstItem, secondSlotID); //Add First item to second Slot
        UpdateTooltip(firstItem);
        
        holdingItem = false;
        heldItemSlotId = 0;

    }
    public bool RemoveItemByName(ItemScriptableObject item)
    {
        if (inventoryItems.Contains(item))
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == item)
                {
                    inventoryItems[i] = null;
                    inventorySlotsUI[i].ResetItemSlot();
                    return true;
                }
            }
        }
        return false;
    }

    public bool RemoveItemByIndex(int index)
    {
        inventoryItems[index] = null;
        inventorySlotsUI[index].ResetItemSlot();
        return true;
    }

    public void UpdateTooltip(ItemScriptableObject item)
    {
        tooltipSlotUI.UpdateTooltipSlot(item);
    }

    public void ResetTooltip()
    {
        tooltipSlotUI.Reset();
    }

}
