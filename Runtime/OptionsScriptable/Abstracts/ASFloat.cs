using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    public class ASFloat : AOptionsScriptable
    {
        [SerializeField] protected float defaultValue = 0;

        public float Value => Parse(LoadID());

        public override IConvertible Get_GenValue() => Value;

        float Parse(string data)
        {
            if (data == "")
                return defaultValue;

            if (float.TryParse(data, out float id))
                return id;
            else
            {
                Debug.LogWarning($"Can't parse [{data}] to float");
                return 0;
            }
        }
    }
}
