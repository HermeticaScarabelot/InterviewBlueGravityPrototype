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
        }

    }

    public void OpenShop()
    {
        shopGo.SetActive(true);
    }

    void CloseShop()
    {
        shopGo.SetActive(false);
    }
}
