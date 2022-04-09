using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField]private int _money;
    public int money { get { return _money; } private set { _money = value; } }
    private MoneyInput moneyInput;

    private void Awake() {
        moneyInput = FindObjectOfType<MoneyInput>();
        moneyInput.UpdateMoney();
    }

    public void SetMoney(PlayerData data)
    {
	money = data.playerMoney;
        moneyInput.UpdateMoney();
    }

    public bool HaveEnoughMoney(int price)
    {
	return price <= money;
    }

    public bool Pay(int price)
    {
	if (HaveEnoughMoney(price))
	{
	    moneyInput.DownMoneyInput(money, price);
	    money -= price;
	    return true;
	}
	return false;
    }

    public void GiveMoney(int money_)
    {
	moneyInput.UpMoneyInput(money, money_);
	money += money_;
    }
}
