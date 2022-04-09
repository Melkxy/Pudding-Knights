using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpRewardInput : MonoBehaviour
{
    [SerializeField]private GameObject levelUpRewardPrefab;
    [SerializeField]private float visibleTime;

    public void ShowLevelUp()
    {
	StartCoroutine(InstantiateLevelUp());
    }

    private IEnumerator InstantiateLevelUp()
    {
	GameObject levelUpInstance = Instantiate(levelUpRewardPrefab, transform.position, Quaternion.identity, transform);
	yield return new WaitForSeconds(visibleTime);
	Destroy(levelUpInstance);
    }
}
