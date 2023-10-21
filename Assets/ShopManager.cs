using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager ShopManagerInstance;

    [SerializeField] private GameObject shopGo;
    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject sellButton;
    
    [SerializeField] private GameObject purchasableSlotsPanel;
    [SerializeField] private PurchasableSlotUI[] purchasableSlotsUI;

    [SerializeField] private GameObject inventorySlotsShopPanel;
    [SerializeField] private InventorySlotShopUI[] inventorySlotsShopUI;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private ItemScriptableObject[] activeShopItems;
    [SerializeField] public ItemScriptableObject selectedItem;
    [SerializeField] public int selectedItemSlotId;

    [SerializeField] public TooltipSlotUI tooltipSlotUI;
    
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

        if (shopGo.activeSelf)
        {
            Invoke("DeActivateShopOnStart", 0.15f);
        }
    }

    void DeActivateShopOnStart()
    {
        shopGo.SetActive(false);    
    }
    
    private void Start()
    {
        inventoryManager = InventoryManager.InventoryManagerInstance;
    }

    public void OpenShop(ItemScriptableObject[] itemsForSale)
    {
        DeSelectItem();
        activeShopItems = itemsForSale;
        ChangeToPurchasable();
        shopGo.SetActive(true);
    }



    public void CloseShop()
    {
        DeSelectItem();
        shopGo.SetActive(false);
    }

    public void ChangeToPurchasable()
    {
        DeSelectItem();
        inventorySlotsShopPanel.SetActive(false);
        sellButton.SetActive(false);
        
        purchasableSlotsPanel.SetActive(true);
        purchaseButton.SetActive(true);
        LoadShop();
    }

    public void LoadShop()
    {
        for (int i = 0; i < activeShopItems.Length; i++)
        {
            purchasableSlotsUI[i].LoadItem(activeShopItems[i]);
        }
    }

    public void SelectItem(ItemScriptableObject item, int slotId)
    {
        selectedItem = item;
        selectedItemSlotId = slotId;
    }

    public void DeSelectItem()
    {
        selectedItem = null;
        selectedItemSlotId = 0;
        tooltipSlotUI.UpdateTooltipSlot(null);        
    }
    
    public void ChangeToSellable()
    {
        DeSelectItem();
        purchaseButton.SetActive(false);
        purchasableSlotsPanel.SetActive(false);
        
        inventorySlotsShopPanel.SetActive(true);
        sellButton.SetActive(true);
        LoadInventory();
    }

    public void LoadInventory()
    {
        for (int i = 0; i < inventorySlotsShopUI.Length; i++)
        {
            inventorySlotsShopUI[i].LoadItem(inventoryManager);
        }
    }

    public void PurchaseSelectedItem()
    {
        if (selectedItem == null)
        {
            return;
        }
        inventoryManager.PickupItem(selectedItem);
        DeSelectItem();
    }

    public void SellSelectedItem()
    {
        if (selectedItem == null)
        {
            return;
        }
        inventoryManager.RemoveItemByIndex(selectedItemSlotId);
        LoadInventory();
        DeSelectItem();
    }
}
