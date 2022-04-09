using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopInterfaceComponents : MonoBehaviour
{
    [SerializeField]private TMP_Text shopName;

    public void UpdateComponents(ShopInterface shopInterface)
    {
	shopName.text = shopInterface.shop.shopName;
    }
}
