using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText, addedMoneyText;

	[SerializeField] private float moneyAnimationSpeed;

    public void UpdateMoney()
    {
        moneyText.text = FindObjectOfType<PlayerMoney>().money.ToString();
    }

    public void UpMoneyInput(int playerMoney, int money)
    {
	StartCoroutine(UpMoney(playerMoney, money));
	StartCoroutine(DownAddedMoney(money));
    }

    public void DownMoneyInput(int playerMoney, int money)
    {
	StartCoroutine(DownMoney(playerMoney, money));
    }

    private IEnumerator DownAddedMoney(int money)
    {
	addedMoneyText.text = "+" + money.ToString();
	yield return new WaitForSeconds(1);
	for (int i = money; i > 0; i--)
	{
	    addedMoneyText.text = "+" + i.ToString();
	    yield return new WaitForSeconds(moneyAnimationSpeed);
	}
	addedMoneyText.text = "";
    }

    private IEnumerator UpMoney(int playerMoney, int money)
    {
	yield return new WaitForSeconds(1);
	for (int i = 0; i < money; i++)
	{
	    int display = playerMoney + i;
	    moneyText.text = display.ToString();
	    yield return new WaitForSeconds(moneyAnimationSpeed);
	}
	UpdateMoney();
    }

    private IEnumerator DownMoney(int playerMoney, int money)
    {
	for (int i = 0; i < money; i++)
	{
	    int display = playerMoney - i;
	    moneyText.text = display.ToString();
	    yield return new WaitForSeconds(moneyAnimationSpeed);
	}
	UpdateMoney();
    }
}
