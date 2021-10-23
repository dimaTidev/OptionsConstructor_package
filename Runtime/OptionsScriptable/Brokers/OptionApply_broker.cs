using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OptionsConstructor
{
    public class OptionApply_broker : AOptionApply
    {
        [SerializeField] UnityEvent_int onChange_int = null;
        [SerializeField] UnityEvent_float onChange_float = null;
        [SerializeField] UnityEvent_string onChange_string = null;

        [System.Serializable] public class UnityEvent_int : UnityEvent<int>{ }
        [System.Serializable] public class UnityEvent_float : UnityEvent<float>{ }
        [System.Serializable] public class UnityEvent_string : UnityEvent<string>{ }


        public override void OnChange()
        {
            if (!option)
                return;

            IConvertible value = option.Get_GenValue();
            
            if (value.GetType() == typeof(int))
                onChange_int?.Invoke((int)value);
            else if (value.GetType() == typeof(float))
                onChange_float?.Invoke((float)value);
            else if (value.GetType() == typeof(string))
                onChange_string?.Invoke((string)value);
        }


        #region Test
        //------------------------------------------------------------------------------------------------------------
        public void TEST_SetValueInt(int value) => Debug.Log("Set int value: " + value);
        public void TEST_SetValueFloat(float value) => Debug.Log("Set float value: " + value);
        public void TEST_SetValueString(string value) => Debug.Log("Set string value: " + value);
        //------------------------------------------------------------------------------------------------------------
        #endregion
    }
}
