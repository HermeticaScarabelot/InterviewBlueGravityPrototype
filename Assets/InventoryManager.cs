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

}
