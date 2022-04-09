using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static bool isBusy;

    public static void Busy(bool _isBusy)
    {
	isBusy = _isBusy;
    }

    public static bool IsBusy()
    {
	return isBusy;
    }
}
