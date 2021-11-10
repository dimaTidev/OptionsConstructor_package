using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    [CreateAssetMenu(fileName = "stringList", menuName = "ScriptableObjects/OptionsConstructor/List/string")]
    public class SO_stringArray : ADropdown<string>
    {
        public void Set_Value(string[] values)
        {
            Debug.Log("values:" + values.Length);
            this.values = values;
        }
    }
}
