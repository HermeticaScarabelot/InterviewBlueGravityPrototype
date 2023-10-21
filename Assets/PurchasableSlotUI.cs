using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasableSlotUI : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject purchasableItem;
    [SerializeField] public int slotId;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;
    
    private Image image;
    private Sprite defaultSpriteBg;
    
    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        defaultSpriteBg = image.sprite;
    }


    public void LoadItem(ItemScriptableObject itemToLoad)
    {
        if (itemToLoad == null)
        {
            image.sprite = defaultSpriteBg;
            Debug.Log("empty slot");
        }
        else
        {
            purchasableItem = itemToLoad;
            image.sprite = itemToLoad.itemSprite;
        }
    }
    
}
