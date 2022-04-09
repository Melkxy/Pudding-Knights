using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType
{
    Damage,
    Defense,
    Strength,
    AttackSpeed,
    Impact,
    Weight,
}

public class PlayerStats : MonoBehaviour
{
    [SerializeField]private int _level;
    public int level { get { return _level; } private set { _level = value; } }
	
    [SerializeField]private int _xp, _baseXP;
    public int xp { get { return _xp; } private set { _xp = value; } }
    public int baseXP { get { return _baseXP; } private set { _baseXP = value; } }

    [SerializeField]private int _attributePoints;
    public int attributePoints { get { return _attributePoints; } set { _attributePoints = value; } }

    [SerializeField]private int _maxHP, _maxMP;
    public int maxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public int maxMP { get { return _maxMP; } private set { _maxMP = value; } }

    [SerializeField]private int _currentHP, _currentMP;
    public int currentHP { get { return _currentHP; } private set { _currentHP = value; } }
    public int currentMP { get { return _currentMP; } private set { _currentMP = value; } }

    [SerializeField]private PlayerAttribute _playerDmg;
    public PlayerAttribute playerDmg { get { return _playerDmg; } private set { _playerDmg = value; } }

    [SerializeField]private PlayerAttribute _strength;
    public PlayerAttribute strength { get { return _strength; } private set { _strength = value; } }

    [SerializeField]private PlayerAttribute _defense;
    public PlayerAttribute defense { get { return _defense; } private set { _defense = value; } }

    [SerializeField]private PlayerAttribute _attackSpeed;
    public PlayerAttribute attackSpeed { get { return _attackSpeed; } private set { _attackSpeed = value; } }

    private HealthBar healthBar;
    private ManaBar manaBar;

    [SerializeField]private float manaRegenWait;
    private float manaRegenWaitTimer;
    [SerializeField]private float manaRegenSpeed;

    private void Awake()
    {
	healthBar = FindObjectOfType<HealthBar>();
	manaBar = FindObjectOfType<ManaBar>();
	healthBar.SetHealth(maxHP, currentHP);
	manaBar.SetMana(maxMP, currentMP);
    }

    private void Update()
    {
	NaturalManaRegen();
    }

    public void SetStats(PlayerData data)
    {
	level = data.playerLevel;
	xp = data.playerXP;
	baseXP = data.playerBaseXP;
	attributePoints = data.playerAttributePoints;
	maxHP = data.playerMaxHP;
	maxMP = data.playerMaxMP;
	currentHP = data.playerCurrentHP;
	currentMP = data.playerCurrentMP;
	playerDmg = new PlayerAttribute(playerDmg.type, data.playerDmg);
	strength = new PlayerAttribute(strength.type, data.playerStrength);
	defense = new PlayerAttribute(defense.type, data.playerDefense);
	attackSpeed = new PlayerAttribute(attackSpeed.type, data.playerAttackSpeed);
    }

    [ContextMenu("GetNeededXP")]
    public int GetNeededXP()
    {
	int xpTemp = 0;
	for (int i = 1; i <= level; i++)
        {
	    xpTemp = ((baseXP * i) + xpTemp);
	}
	Debug.Log("XP needed: " + xpTemp);
        return xpTemp;
    }

    [ContextMenu("TryUpLevel")]
    public void TryUpLevel()
    {
	if (xp >= GetNeededXP())
	{
	    UpLevel();
	}
    }

    private void UpLevel()
    {
	while (xp >= GetNeededXP())
	{
	    this.level++;
	    FindObjectOfType<LevelUpRewardInput>().ShowLevelUp();
	    Debug.Log("Level UP to " + this.level);
	    attributePoints += 5;
	}
    }

    public void GiveXP(int xp_)
    {
	xp += xp_;
	FindObjectOfType<XPRewardInput>().ShowXP(xp_);
	TryUpLevel();
    }

    public void UpMana()
    {
	if (attributePoints > 0)
	{
	    maxMP += 2;
	    currentMP += 2;
	    attributePoints--;
	}
    }

    public void UpDamage()
    {
	if (attributePoints > 0)
	{
	    playerDmg.value.UpBaseValue();
	    attributePoints--;
	}
    }

    public void UpDefense()
    {
	if (attributePoints > 0)
	{
	    defense.value.UpBaseValue();
	    attributePoints--;
	}
    }

    public void UpHP()
    {
	if (attributePoints > 0)
	{
	    maxHP++;
	    attributePoints--;
	}
    }

    public void UpMP()
    {
	if (attributePoints > 0)
	{
	    maxMP++;
	    attributePoints--;
	}
    }

    public void DamageHP(int dmg)
    {
		int finalDmg = (4 * dmg) - (2 * defense.value.modifiedValue);

		if (finalDmg <= 0)
		{
			finalDmg = 2;
		}
		if (currentHP - finalDmg <= 0)
		{
	    	currentHP = 0;
	    	DeadAction();
		}
		else
		{
	    	currentHP -= finalDmg;
		}
		healthBar.SetHealth(maxHP, currentHP);
	}	

    public void RecoverHP(int recover)
    {
	if (currentHP + recover >= maxHP)
	{
	    currentHP = maxHP;
	}
	else
	{
	    currentHP += recover;
	}
	healthBar.SetHealth(maxHP, currentHP);
    }

    public void DamageMP(int dmg)
    {
	if (currentMP - dmg <= 0)
	{
	    currentMP = 0;
	}
	else
	{
	    currentMP -= dmg;
	}
	manaBar.SetMana(maxMP, currentMP);
    }

    public void RecoverMP(int recover)
    {
	if (currentMP + recover >= maxMP)
	{
	    currentMP = maxMP;
	}
	else
	{
	    currentMP += recover;
	}
	manaBar.SetMana(maxMP, currentMP);
    }

    private void NaturalManaRegen()
    {
	if (currentMP < maxMP)
	{
	    if (manaRegenWaitTimer > 0)
	    {
		manaRegenWaitTimer -= Time.deltaTime;
	    }
	    else
	    {
		float regen = Time.deltaTime * manaRegenSpeed;
		RecoverMP(Mathf.CeilToInt(regen));
		manaBar.SetMana(maxMP, currentMP);
	    }
	}
    }

    public void ResetManaRegen()
    {
	manaRegenWaitTimer = manaRegenWait;
    }

    private void DeadAction()
    {
	FindObjectOfType<GameOverMenuManager>().OpenMenu();
    }
}
