using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager ShopManagerInstance;

    private void Awake()
    {
        ShopManagerInstance = this;
    }

    public void OpenShop(GameObject shopGo)
    {
        shopGo.SetActive(true);
    }
}
