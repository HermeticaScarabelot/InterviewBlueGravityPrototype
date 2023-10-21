using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager ShopManagerInstance;

    [SerializeField] private GameObject shopGo;
    
    [SerializeField] private GameObject purchasableSlotsPanel;
    [SerializeField] private PurchasableSlotUI[] purchasableSlotsUI;

    [SerializeField] private GameObject inventorySlotsShopPanel;
    [SerializeField] private InventorySlotShopUI[] inventorySlotsShopUI;

    [SerializeField] private InventoryManager inventoryManager;
    
    private void Awake()
    {
        ShopManagerInstance = this;
        
        PurchasableSlotUI[] purchasableSlots = purchasableSlotsPanel.GetComponentsInChildren<PurchasableSlotUI>();
        purchasableSlotsUI = new PurchasableSlotUI[purchasableSlots.Length];
        for (int i = 0; i < purchasableSlots.Length; i++)
        {
            purchasableSlotsUI[i] = purchasableSlots[i];
        }

        InventorySlotShopUI[] inventorySlotsShop = inventorySlotsShopPanel.GetComponentsInChildren<InventorySlotShopUI>();
        inventorySlotsShopUI = new InventorySlotShopUI[inventorySlotsShop.Length];
        for (int i = 0; i < inventorySlotsShop.Length; i++)
        {
            inventorySlotsShopUI[i] = inventorySlotsShop[i];
            inventorySlotsShopUI[i].slotId = i;
        }

    }

    private void Start()
    {
        inventoryManager = InventoryManager.InventoryManagerInstance;
    }

    public void OpenShop()
    {
        ChangeToPurchasable();
        shopGo.SetActive(true);
    }

    public void CloseShop()
    {
        shopGo.SetActive(false);
    }

    public void ChangeToPurchasable()
    {
        inventorySlotsShopPanel.SetActive(false);
        purchasableSlotsPanel.SetActive(true);
        
    }

    public void ChangeToSellable()
    {
        purchasableSlotsPanel.SetActive(false);
        inventorySlotsShopPanel.SetActive(true);
        LoadInventory();
    }

    public void LoadInventory()
    {
        for (int i = 0; i < inventorySlotsShopUI.Length; i++)
        {
            inventorySlotsShopUI[i].LoadItem(inventoryManager);
        }
    }
}
