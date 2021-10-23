using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OptionsConstructor
{
    public abstract class ADropdown : ASInt
    {
        public abstract string[] Labels();
    }

    public abstract class ADropdown<T> : ADropdown where T : IConvertible
    {
        [SerializeField] protected T[] values = new T[0];
        /// <summary>
        /// Return -999 when value not Exist
        /// </summary>
        public T Value //TODO: написать более безопасно! а то вдруг фпс или screen resolution станут 0 !!!
        {
            get
            {
                if(values == null)
                {
                    T tempValue = default; //NOTSAFE:
                    Debug.LogError("Option values is Null");
                    return tempValue;
                }

                if (CurId < 0 || CurId >= values.Length)
                {
                    if (CurId < 0 || CurId >= values.Length)
                        return values[defaultValue];
                    else
                    {
                        T tempValue = default; //NOTSAFE:
                        Debug.LogError("defaultValue is out of Range of values");
                        return tempValue;
                    }
                }
                    
                return values[CurId];
            }
        }
        public override IConvertible Get_GenValue() => Value;
        public override string[] Labels()
        {
            string[] labels = new string[values.Length];
                  for (int i = 0; i < labels.Length; i++)
                      labels[i] = values[i].ToString();
            return labels;
        }

        public override void SetValue(IConvertible id)
        {
            if (values == null || (int)id < 0 || (int)id >= values.Length)
                return;

            base.SetValue(id); //Save id
        }

        private void OnValidate()
        {
            if(values != null)
                defaultValue = Mathf.Clamp(defaultValue, 0, values.Length);
        }
    }
}
