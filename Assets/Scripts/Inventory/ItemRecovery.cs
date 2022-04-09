using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemRecovery
{
    [SerializeField]private ItemRecoveryType _type;
    public ItemRecoveryType type { get { return _type; } private set { _type = value; } }
    [SerializeField]private int _recovery;
    public int recovery { get { return _recovery; } private set { _recovery = value; } }
}
