using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Database", menuName = "Player/SkillSystem/Database")]
public class SkillDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]private SkillObject[] _skillObjects;
    public SkillObject[] skillObjects { get { return _skillObjects; } private set { _skillObjects = value; } }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < skillObjects.Length; i++)
        {
            if (!skillObjects[i].CompareID(i))
            {
                skillObjects[i].CreateSkill(skillObjects[i].data.name, i, skillObjects[i].data.manaCost,  skillObjects[i].data.effects);
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
