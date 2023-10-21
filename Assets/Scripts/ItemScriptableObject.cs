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

    public enum OutfitPiece
    {
        Null,
        Head,
        Torso
    }

    public ItemType type;
    public OutfitPiece outfitType;
    
    public string itemName;
    public Sprite itemSprite;
    
}

