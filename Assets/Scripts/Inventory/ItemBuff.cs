using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBuff : IModifier
{
    [SerializeField]private AttributeType _attribute;
    public AttributeType attribute { get { return _attribute; } private set { _attribute = value; } }
    [SerializeField]private int _value;
    public int value { get { return _value; } private set { _value = value; } }

    public ItemBuff(int _value)
    {
        value = _value;
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
}
