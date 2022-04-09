using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]private Slider slider;

    public void SetMana(int maxMana, int currentMana)
    {
	slider.maxValue = maxMana;
	slider.value = currentMana;

    }
}
