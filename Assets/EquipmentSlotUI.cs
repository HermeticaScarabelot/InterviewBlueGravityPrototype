using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{

    public enum EquipmentSlotType
    {
        Helm,
        Torso,
        Legs
    }
    
    [SerializeField] private EquipmentSlotType slotType;
    [SerializeField] private Sprite equipmentSprite;
    public ItemScriptableObject equippedItem;
    
    [SerializeField] private Image image;

    private void Awake()
    {
        if (!image)
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }

        if (equipmentSprite)
        {
            image.sprite = equipmentSprite;
        }
    }

    public void UpdateEquipmentSlot(ItemScriptableObject newItem)
    {
        equipmentSprite = newItem.itemSprite;
        equippedItem = newItem;
        image.sprite = equipmentSprite;
    }
    
}
