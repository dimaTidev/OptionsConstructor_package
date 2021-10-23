using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace OptionsConstructor
{
    public abstract class ADropdownUI : OptionAbstract
    {
        [SerializeField] protected int ID_deffaultValue = 0;
        protected void SetupDropdown(Dropdown dropDown, int[] values, int loadedID)
        {
            if (values == null || values.Length == 0)
                return;

            string[] outArray = new string[values.Length];
            for (int i = 0; i < outArray.Length; i++)
                outArray[i] = values[i].ToString();
        }

        protected void SetupDropdown(Dropdown dropDown, string[] values, int loadedID)
        {
            if (values == null || values.Length == 0 || !dropDown)
                return;

            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < values.Length; i++)
                data.Add(new Dropdown.OptionData(values[i]));


            isInteractable = false; //-----------------

            dropDown.AddOptions(data);
            dropDown.value = loadedID;
            dropDown.onValueChanged.AddListener(delegate { DropdownSelect(dropDown); });

            isInteractable = true; //-----------------
        }


        public void DropdownSelect(Dropdown change)
        {
            if (!isInteractable) return;

            SetDropDownValue(change.value);
            onChangeCallback?.Invoke();
        }

        protected virtual void SetDropDownValue(int value)
        {
            SaveID(value);
        }


        protected int LoadID()
        {
            return PlayerPrefs.GetInt(save_key, ID_deffaultValue);
        }

        protected void SaveID(int value)
        {
            PlayerPrefs.SetInt(save_key, value);
        }
    }
}
