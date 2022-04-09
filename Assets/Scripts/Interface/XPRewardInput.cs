using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPRewardInput : MonoBehaviour
{
    [SerializeField]private GameObject xpRewardPrefab;
    [SerializeField]private float xpVisibleTime;

    public void ShowXP(int xp)
    {
	StartCoroutine(InstantiateXpReward(xp));
    }

    private IEnumerator InstantiateXpReward(int xp)
    {
	GameObject xpInstance = Instantiate(xpRewardPrefab, transform.position, Quaternion.identity, transform);
	xpInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + xp.ToString();
	yield return new WaitForSeconds(xpVisibleTime);
	Destroy(xpInstance);
    }
}
