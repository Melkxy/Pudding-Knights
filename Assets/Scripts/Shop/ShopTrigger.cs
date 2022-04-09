using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField]private ShopObject shop;

    public void TriggerShop()
    {
	FindObjectOfType<ShopMenuManager>().OpenMenuByShop(shop);
    }
}
