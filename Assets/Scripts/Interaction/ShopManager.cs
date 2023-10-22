using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager ShopManagerInstance;

    [SerializeField] public GameObject shopGo;
    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject sellButton;

    [SerializeField] private GameObject purchasableTabGo;
    [SerializeField] private GameObject purchasableSlotsPanel;
    [SerializeField] private PurchasableSlotUI[] purchasableSlotsUI;

    [SerializeField] private GameObject sellableTabGo;
    [SerializeField] private GameObject inventorySlotsShopPanel;
    [SerializeField] private InventorySlotShopUI[] inventorySlotsShopUI;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private bool isTabTweening;
    
    [SerializeField] private ItemScriptableObject[] activeShopItems;
    [SerializeField] public ItemScriptableObject selectedItem;
    [SerializeField] public int selectedItemSlotId;
    [SerializeField] private TextMeshProUGUI shopCurrencyDisplay;

    [SerializeField] public GameObject playerGo;

    [SerializeField] public TooltipSlotUI tooltipSlotUI;
    

    
    private void Awake()
    {
        ShopManagerInstance = this;
        
        //Initialize Shop Slot by assigning them to each Slot in the PurchaseblePanel
        PurchasableSlotUI[] purchasableSlots = purchasableSlotsPanel.GetComponentsInChildren<PurchasableSlotUI>();
        purchasableSlotsUI = new PurchasableSlotUI[purchasableSlots.Length];
        for (int i = 0; i < purchasableSlots.Length; i++)
        {
            purchasableSlotsUI[i] = purchasableSlots[i];
        }

        //Initialize the Inventory Tab
        InventorySlotShopUI[] inventorySlotsShop = inventorySlotsShopPanel.GetComponentsInChildren<InventorySlotShopUI>();
        inventorySlotsShopUI = new InventorySlotShopUI[inventorySlotsShop.Length];
        for (int i = 0; i < inventorySlotsShop.Length; i++)
        {
            inventorySlotsShopUI[i] = inventorySlotsShop[i];
            inventorySlotsShopUI[i].slotId = i;
        }

        //Close after Initialization
        if (shopGo.activeSelf)
        {
            Invoke("DeActivateShopOnStart", 0.15f);
        }

        if (!playerGo)
        {
            playerGo = GameObject.FindGameObjectWithTag("Player");
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
        //Reset Tooltip and make sure the player isn't holding a Item
        DeSelectItem();
        activeShopItems = itemsForSale;
        //change to purchasable Tab
        ChangeToPurchasable();
        shopGo.SetActive(true);
        UpdateCurrencyUI();
    }



    public void CloseShop()
    {
        DeSelectItem();
        shopGo.SetActive(false);
    }

    public void ChangeToPurchasable()
    {
        DeSelectItem();
        //Deactivates Sell Button and Panel
        inventorySlotsShopPanel.SetActive(false);
        sellButton.SetActive(false);
        
        //Activate and Tween purchasable panel
        purchasableSlotsPanel.SetActive(true);
        if (!isTabTweening)
        {
            isTabTweening = true;
            purchasableTabGo.transform.DOScale(0.75f, 0.15f).SetEase(Ease.InOutCirc).SetLoops(2, LoopType.Yoyo).OnComplete(()=>isTabTweening = false);
        }
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
        //Deactivate purchase button and Panel
        purchaseButton.SetActive(false);
        purchasableSlotsPanel.SetActive(false);
        
        //Activate and Tween inventory tab and sell button
        inventorySlotsShopPanel.SetActive(true);
        if (!isTabTweening)
        {
            isTabTweening = true;
            sellableTabGo.transform.DOScale(0.75f, 0.15f).SetEase(Ease.InOutCirc).SetLoops(2, LoopType.Yoyo).OnComplete(()=>isTabTweening = false);
        }
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

        //Checks if the player has enough currency
        if (inventoryManager.SpendMoney(selectedItem.itemPrice))
        {
            inventoryManager.PickupItem(selectedItem);
            DeSelectItem();
            UpdateCurrencyUI();
        }
    }

    public void SellSelectedItem()
    {
        if (selectedItem == null)
        {
            return;
        }
        inventoryManager.ReceiveMoney(selectedItem.itemPrice);
        inventoryManager.RemoveItemByIndex(selectedItemSlotId);
        //Update the UI
        LoadInventory();
        DeSelectItem();
        UpdateCurrencyUI();
    }

    void UpdateCurrencyUI()
    {
        shopCurrencyDisplay.text = "$" + inventoryManager.GetPlayerCurrency().ToString();
    }
}
