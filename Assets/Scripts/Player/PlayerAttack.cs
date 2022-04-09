using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    [SerializeField]
    private GameObject attackPrefab;

    [SerializeField]
    private KeyCode attackKey, attackKeyJoyStick;

    [SerializeField]
    private float attackDuration;

    [SerializeField]
    private float attackCoolDown;

    private bool canAttack;

    [SerializeField]
    private Transform attackPosition;

    [SerializeField]
    private float attackDistance;

    [SerializeField]
    public Vector2 attackSizeHorizontal, attackSizeVertical;

    private void Awake()
    {
	playerMovement = GetComponent<PlayerMovement>();
	playerStats = GetComponent<PlayerStats>();
	canAttack = true;
    }

    private void Update()
    {
	StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
	if ((Input.GetKeyDown(attackKey) || Input.GetKeyDown(attackKeyJoyStick)) && canAttack && !GameState.IsBusy() && FindObjectOfType<Player>().HasWeapon())
	{
	    canAttack = false;
	    SetAttack();
	    playerMovement.anim.SetTrigger("Attack");
	    if (playerStats.attackSpeed.value.modifiedValue > 0)
	    {
	        yield return new WaitForSeconds(attackCoolDown / playerStats.attackSpeed.value.modifiedValue);
	    }
	    else
	    {
	        yield return new WaitForSeconds(attackCoolDown);
	    }
	    canAttack = true;
	}
    }

    private void SetAttack()
    {
	StartCoroutine(InstantiateAttack(new Vector3(
        attackPosition.localPosition.x + (playerMovement.lastInput.x * attackDistance), 
        attackPosition.localPosition.y + (playerMovement.lastInput.y * attackDistance), 0))
        );
    }

    private IEnumerator InstantiateAttack(Vector3 attackPos)
    {
	GameObject playerAttack = Instantiate(attackPrefab, transform.position + attackPos, Quaternion.identity, transform);
    if (playerAttack.transform.localPosition.x != 0)
	{
        playerAttack.GetComponent<BoxCollider2D>().size = attackSizeVertical;
        playerAttack.transform.GetChild(0).localScale = new Vector3(attackSizeVertical.x, attackSizeVertical.y, 0f);
    }
    else
    {
	playerAttack.GetComponent<BoxCollider2D>().size = attackSizeHorizontal;
        playerAttack.transform.GetChild(0).localScale = new Vector3(attackSizeHorizontal.x, attackSizeHorizontal.y, 0f);
    }
	playerMovement.PausePlayer(attackDuration);
	yield return new WaitForSeconds(attackDuration);
	GameObject.Destroy(playerAttack);
    }
}