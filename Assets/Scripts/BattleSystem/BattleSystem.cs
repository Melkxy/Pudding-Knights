using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum BattleState
{
    START, PLAYER_TURN, ENEMY_TURN, CONFLICT, WON, LOST, ESCAPED
}

public enum PlayerAction
{
    Attack,
    Skill,
    Item,
    Escape,
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField]private MenuManager battleMenu;

    private EnemyUnit enemy;
    
    private BattleState currentState;

    [SerializeField]private Transform playerStation, enemyStation;

    private BattleHUD playerBattleHUD, enemyBattleHUD;

    private ActionMenu actionMenu;

    private PlayerAction playerAction;

    private void Awake()
    {
	playerBattleHUD = FindObjectOfType<PlayerBattleHUD>();
	enemyBattleHUD = FindObjectOfType<EnemyBattleHUD>();
    }

    public void StartBattle(EnemyUnit _enemy)
    {
	battleMenu.OpenMenu();
	SetupBattle(_enemy);
	actionMenu.SetActions(false);
    }

    private void SetupBattle(EnemyUnit _enemy)
    {
	currentState = BattleState.START;
	enemy = new EnemyUnit(_enemy);
	GameObject enemyGO = Instantiate(enemy.enemyPrefab, enemyStation);

	playerBattleHUD.SetHUD();
	enemyBattleHUD.SetHUD();

	PlayerTurn();
    }

    private void PlayerTurn()
    {
	currentState = BattleState.PLAYER_TURN;
	actionMenu.SetActions(true);
    }

    private void EnemyTurn()
    {
	currentState = BattleState.ENEMY_TURN;
    }

    public void PlayerChoose(PlayerAction action)
    {
	if (currentState != BattleState.PLAYER_TURN)
	{
	    return;
	}
	playerAction = action;
	actionMenu.SetActions(false);

	Conflict();
    }

    private void Conflict()
    {

    }

    private void VerifyEndBattle()
    {
	switch (currentState)
	{
	    case BattleState.WON:
	    	BattleWon();
		break;
	    case BattleState.LOST:
	    	BattleLost();
		break;
	    case BattleState.ESCAPED:
	    	BattleEscaped();
		break;
	    default:
		break;
	}
	
    }

    private void BattleWon()
    {
	
    }

    private void BattleLost()
    {
	
    }

    private void BattleEscaped()
    {
	
    }

    private void EndBattle()
    {
	battleMenu.CloseMenu();
    }
}
