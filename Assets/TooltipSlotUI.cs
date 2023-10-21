using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    
    [SerializeField] private Image image;
    private Sprite defaultSprite;

    private void Awake()
    {
        defaultSprite = image.sprite;
    }

    public void UpdateTooltipSlot(ItemScriptableObject item)
    {
        image.sprite = item.itemSprite;
        itemName.text = item.name;
    }

    public void Reset()
    {
        image.sprite = defaultSprite;
        itemName.text = String.Empty;
        itemDescription.text = String.Empty;
        
    }
    
}
