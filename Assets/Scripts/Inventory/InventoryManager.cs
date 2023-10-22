using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager InventoryManagerInstance;

    [SerializeField] private int playerCurrency;
    [SerializeField] private TextMeshProUGUI topRightCurrencyDisplay;
    [SerializeField] private TextMeshProUGUI inventoryCurrencyDisplay;
    [SerializeField] private GameObject inventoryGo;
    [SerializeField] private GameObject inventoryPanelGo;
    [SerializeField] private InventorySlotUI[] inventorySlotsUI = new InventorySlotUI[0];
    public ItemScriptableObject[] inventoryItems = new ItemScriptableObject[0];

    public int heldItemSlotId;
    public bool holdingItem;

    [SerializeField] private TooltipSlotUI tooltipSlotUI;

    [SerializeField] private float tweenScaleDuration;
    [SerializeField] private Ease tweenEase;
    [SerializeField] private bool isTweening;
    
    [SerializeField] private DialogueManager dialogueManager;
    

    private void Awake()
    {

        InventoryManagerInstance = this;
        
        

        inventorySlotsUI = inventoryPanelGo.GetComponentsInChildren<InventorySlotUI>(); //Get all 24 Slots, then automatically set the Id on each one
        for (int i = 0; i < inventorySlotsUI.Length; i++)
        {
            inventorySlotsUI[i].slotId = i;
        }
        inventoryItems = new ItemScriptableObject[inventorySlotsUI.Length]; //Initialize the Inventory Items with null
        
        Invoke("CloseInventory",0.1f);
    }

    private void Start()
    {
        dialogueManager = DialogueManager.DialogueManagerInstance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !dialogueManager.IsInDialogue())
        {
            if (!inventoryGo.activeSelf)
            {
                OpenInventory();
            } else if (inventoryGo.activeSelf && !isTweening)
            {
                holdingItem = false;
                heldItemSlotId = 0;
                CloseInventory();
            }
        }
        
    }

    public int GetPlayerCurrency()
    {
        return playerCurrency;
    }

    public void OpenInventory()
    {
        inventoryGo.SetActive(true);
        TweenUpInventory();
        UpdateCurrencyUI();
    }

    void TweenUpInventory()
    {
        isTweening = true;
        inventoryGo.transform.localScale = new Vector3(0f,0,0);
        inventoryGo.transform.DOKill();
        inventoryGo.transform.DOScale(1, tweenScaleDuration).SetEase(tweenEase).OnComplete(()=>isTweening = false);
    }

    public void CloseInventory()
    {
        TweenDownInventory().OnComplete(()=>inventoryGo.SetActive(false));
    }
    
    Tween TweenDownInventory()
    {
        inventoryGo.transform.DOKill();
        isTweening = true;
        return inventoryGo.transform.DOScale(0f, tweenScaleDuration).SetEase(tweenEase);
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

    public bool SpendMoney(int price)
    {
        if (playerCurrency < price)
        {
            return false;
        }
        playerCurrency -= price;
        UpdateCurrencyUI();
        return true;
    }

    public void ReceiveMoney(int moneyToReceive)
    {
        playerCurrency += moneyToReceive;
        UpdateCurrencyUI();
    }

    void UpdateCurrencyUI()
    {
        inventoryCurrencyDisplay.text = "$" + GetPlayerCurrency().ToString();
        topRightCurrencyDisplay.text = "$" + GetPlayerCurrency().ToString();

    }

}
