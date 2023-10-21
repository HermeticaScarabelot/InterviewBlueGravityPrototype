using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotShopUI : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject purchasableItem;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;
}
