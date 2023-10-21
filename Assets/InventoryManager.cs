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
    
    public InventorySlotUI[] inventorySlotsUI = new InventorySlotUI[0];
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
            inventorySlotsUI = inventoryPanelGo.GetComponentsInChildren<InventorySlotUI>();
            for (int i = 0; i < inventorySlotsUI.Length; i++)
            {
                inventorySlotsUI[i].slotId = i;
            }
            inventoryItems = new ItemScriptableObject[inventorySlotsUI.Length];
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
        for (int i = 0; i < inventoryItems.Length; i++)
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

    public void AddItemToSlot(ItemScriptableObject item, int slotId)
    {
        inventoryItems[slotId] = item;
        inventorySlotsUI[slotId].UpdateItemSlot(item);
    }

    public void SwapItemPosition(int newSlotID)
    {
        ItemScriptableObject itemToNewSlot = inventoryItems[heldItemSlotId];
        ItemScriptableObject itemToOldSlot = inventoryItems[newSlotID];

        RemoveItemByIndex(heldItemSlotId);//Remove First item
        
        if (itemToOldSlot != null)
        {
            RemoveItemByIndex(newSlotID); //If second Slot has a Item, also remove it
        }
        
        AddItemToSlot(itemToNewSlot, newSlotID); //Add First item to second Slot
        
        if (itemToOldSlot != null)
        {
            AddItemToSlot(itemToOldSlot, heldItemSlotId); //Add second item to first slot ;
        }

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

}
