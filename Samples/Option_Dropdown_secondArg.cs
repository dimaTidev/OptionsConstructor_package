using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_Dropdown_secondArg : OptionAbstract
    {
       // /*[PlayerPrefsSelector]*/
       // [SerializeField] string save_key;
        [SerializeField] string[] names = null;
        [SerializeField] int[] values = null;
        [SerializeField] int ID_deffaultValue = 0;
        Dropdown dropDown;
        public override void Awake() => dropDown = GetComponent<Dropdown>();

        public override void Start()
        {
            HasKey(ID_deffaultValue);

            isInteractable = false;
            
            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < names.Length; i++)
                data.Add(new Dropdown.OptionData(names[i]));
            dropDown.options = data;

            int value = PlayerPrefs.GetInt(save_key, Mathf.Clamp(ID_deffaultValue, 0, names.Length - 1));

            dropDown.value = FromValue_To_ID(value);

            dropDown.onValueChanged.AddListener(delegate { DropdownSelect(dropDown); });
            isInteractable = true;
        }

        public override int GetValue() => PlayerPrefs.GetInt(save_key, Mathf.Clamp(ID_deffaultValue, 0, names.Length - 1));

        public override void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, names.Length - 1);
            isInteractable = false;
            SetDropDownValue(value);
            dropDown.value = value;
            isInteractable = true;
        }

        public void DropdownSelect(Dropdown change)
        {
            if (!isInteractable) return;

            SetDropDownValue(change.value);
            onChangeCallback?.Invoke();
        }

        int FromValue_To_ID(int value)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == value)
                    return i;
            }
            return values.Length - 1;
        }

        void SetDropDownValue(int value)
        {
            value = Mathf.Clamp(value, 0, values.Length - 1);
            value = values[value]; //конвертируем в значение
            PlayerPrefs.SetInt(save_key, value);
            OptionRuntimeManager.singleton?.Refresh(); //обновит значения у отдельных realtime скриптов
            Debug.Log("Invoke Changes");
        }
    }
}
