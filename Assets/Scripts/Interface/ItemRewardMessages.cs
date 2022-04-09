using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemRewardMessages : MonoBehaviour
{
    [SerializeField]private GameObject ItemRewardPrefab;
    [SerializeField]private float visibleTime;

    public void ShowLevelUp(ItemObject item, int amount)
    {
	StartCoroutine(InstantiateItemReward(item, amount));
    }

    private IEnumerator InstantiateItemReward(ItemObject item, int amount)
    {
	GameObject ItemRewardInstance = Instantiate(ItemRewardPrefab, transform.position + ItemRewardPrefab.transform.localPosition, Quaternion.identity, transform);
	ItemRewardInstance.transform.GetChild(0).GetComponentInChildren<Image>().sprite = item.icon;
	ItemRewardInstance.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
	ItemRewardInstance.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();

	yield return new WaitForSeconds(visibleTime);
	Destroy(ItemRewardInstance);
    }
}
