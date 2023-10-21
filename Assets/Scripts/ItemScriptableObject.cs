using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public enum ItemType
    {
        Empty,
        Consumable,
        Outfit
    }

    public ItemType type;
    public string itemName;
    public Sprite itemSprite;
    
}

