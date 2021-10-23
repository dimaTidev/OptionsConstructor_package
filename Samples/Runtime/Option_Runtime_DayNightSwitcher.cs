using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Option_Runtime_DayNightSwitcher : MonoBehaviour
{
    static Action<int> onRefreshCallbacks;
    public static void Subscribe(Action<int> callback) => onRefreshCallbacks += callback;
    public static void Unsubscribe(Action<int> callback) => onRefreshCallbacks -= callback;

    public void SetDayNight(int id) => onRefreshCallbacks?.Invoke(id);
}
