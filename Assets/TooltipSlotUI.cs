using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;

    
    [SerializeField] private Image image;

    public void UpdateTooltipSlot(ItemScriptableObject item)
    {
        image.sprite = item.itemSprite;
        itemName.text = item.name;
    }
    
}
