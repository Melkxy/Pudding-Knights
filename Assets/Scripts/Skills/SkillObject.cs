using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Player/SkillSystem/Skill")]
public class SkillObject : ScriptableObject
{
    [SerializeField]private Skill _data = new Skill();
    public Skill data { get { return _data; } private set { _data = value; } }
    [SerializeField]private Sprite _icon;
    public Sprite icon { get { return _icon; } private set { _icon = value; } }

    [TextArea(5,20)]
    public string description;

    public Skill CreateSkill(string _name, int _id, int _manaCost, SkillEffect[] _effects)
    {
        Skill newSkill = new Skill(_name, _id, _manaCost, _effects);
	data = newSkill;
        return newSkill;
    }

    public bool CompareID(int _id)
    {
	bool compare = data.id == _id;
	return data.id == _id;
    }
}
