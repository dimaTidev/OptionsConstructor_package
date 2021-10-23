using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class OptionScriptable : MonoBehaviour
{
    public static OptionScriptable singleton;

    void Awake() => singleton = this;

    public void Subscribe(string name)
    {
        Debug.Log("Subscribe OptionScriptable is completed by " + name);
    }
}
