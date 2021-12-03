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
        [SerializeField] bool isSaveValueInsteadID = false;

        [SerializeField] protected T[] values = new T[0];
        /// <summary>
        /// Return -999 when value not Exist
        /// </summary>
        public T Value //TODO: написать более безопасно! а то вдруг фпс или screen resolution станут 0 !!!
        {
            get
            {
                if(values == null || values.Length == 0)
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

        /// <summary>
        /// data All Time come in - int
        /// </summary>
        /// <param name="data"></param>
        public override void SetValue(IConvertible data)
        {
            if (values == null)
                return;

            if (data is int id)
            {
                if (id < 0 || id >= values.Length)
                    return;

                if (isSaveValueInsteadID)
                {
                    base.SetValue(values[id]);
                    return;
                }
            }
            base.SetValue(data); //Save id
        }

        protected override int ParseLoadData(string data)
        {
            if (!isSaveValueInsteadID)
                return base.ParseLoadData(data);
            else if (data is T value)
            {
                for (int i = 0; i < values.Length; i++)
                    if (values[i].Equals(value))
                        return i;
            }
            return defaultValue;
        }

        private void OnValidate()
        {
            if(values != null)
            {
                if (defaultValue < 0)
                    defaultValue = 0;
                if (defaultValue >= values.Length)
                    defaultValue = values.Length - 1;
                //defaultValue = Mathf.Clamp(defaultValue, 0, values.Length);
            }
                
        }
    }
}
