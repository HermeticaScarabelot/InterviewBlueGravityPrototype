using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager InventoryManagerInstance;

    [SerializeField] private GameObject inventoryGo;
    [SerializeField] private GameObject inventoryPanelGo;
    
    public InventorySlotUI[] inventorySlotsUI = new InventorySlotUI[0];
    public List<ScriptableObject> inventoryItems = new List<ScriptableObject>();

    private void Awake()
    {
        if (!InventoryManagerInstance)
        {
            InventoryManagerInstance = this;
        }
        
        if (inventoryPanelGo)
        {
            inventorySlotsUI = inventoryPanelGo.GetComponentsInChildren<InventorySlotUI>();
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
        if (inventoryItems.Count < inventorySlotsUI.Length)
        {
            Debug.Log(inventoryItems.Count);
            inventoryItems.Add(Instantiate(item));
            Debug.Log(inventoryItems.Count);

            foreach (var inventorySlotUI in inventorySlotsUI)
            {
                if (inventorySlotUI.type == ItemScriptableObject.ItemType.Empty)
                {
                    inventorySlotUI.UpdateItemSlot(item);
                    break;
                }
            }
            return true;
        }

        return false;

    }
    
}
