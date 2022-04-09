using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{
    Player,
    Enemy,
}

public enum SkillAttribute
{
    Health,
    Mana,
}

[System.Serializable]
public class SkillEffect
{
    [SerializeField]private SkillAttribute attribute;
    [SerializeField]private int value;
    [SerializeField]private Target target;
}
