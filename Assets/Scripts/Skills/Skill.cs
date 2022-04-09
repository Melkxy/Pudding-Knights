using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    [SerializeField]private string _name;
    public string name { get { return _name; } private set { _name = value; } }
    [SerializeField]private int _id = -1;
    public int id { get { return _id; } private set { _id = value; } }
    [SerializeField]private int _manaCost;
    public int manaCost { get { return _manaCost; } private set { _manaCost = value; } }

    [SerializeField]private SkillEffect[] _effects;
    public SkillEffect[] effects { get { return _effects; } private set { _effects = value; } }

    public Skill(string name_, int id_, int manaCost_, SkillEffect[] effects_)
    {
	name = name_;
	id = id_;
	manaCost = manaCost_;
	effects = effects_;
    }

    public Skill()
    {
	name = "";
	id = -1;
	manaCost = 0;
	effects = new SkillEffect[0];
    }
}
