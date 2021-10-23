using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Base : ScriptableObject
{  
}


public class SValue<T> : Base where T: IConvertible
{
    public T value;
}

public class BaseDropdown<T> : SValue<T> where T : IConvertible
{
    public List<string> labels = new List<string> { "First", "Second", "Third" };
}



[CreateAssetMenu(fileName = "DropD_intBased", menuName = "ScriptableObjects/Test/DropD_intBased")]
public class DropD_intBased : BaseDropdown<int>
{
    public int idLabel => value;
}

[CreateAssetMenu(fileName = "DropD_stringBased", menuName = "ScriptableObjects/Test/DropD_stringBased")]
public class DropD_stringBased : BaseDropdown<string>
{
    public string Value => value;

    public int idLabel 
    {
        get
        {
            return labels.IndexOf(Value);
        }
    }

}

public class UIScr : MonoBehaviour
{
    public DropD_intBased dropInt;
    public DropD_stringBased dropString;

    void Start()
    {
        Debug.Log("dropInt: " + dropInt.labels[dropInt.idLabel]);
        Debug.Log("dropString: " + dropString.labels[dropString.idLabel]);
    }
}
