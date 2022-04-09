using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttribute
{
    private Player parent;

    [SerializeField]private AttributeType _type;
    public AttributeType type { get { return _type; } private set { _type = value; } }
    [SerializeField]private ModifiableInt _value;
    public ModifiableInt value { get { return _value; } private set { _value = value; } }

    public PlayerAttribute(AttributeType type_, int value_)
    {
	type = type_;
	value = new ModifiableInt(AttributeModified, value_);
    }

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified, value.baseValue);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
