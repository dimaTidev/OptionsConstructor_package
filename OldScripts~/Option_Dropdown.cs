using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_Dropdown : OptionAbstract
    {
       // /*[PlayerPrefsSelector]*/
       // [SerializeField] string save_key;
        [SerializeField] string[] values = null;
        [SerializeField] int ID_deffaultValue = 0;
        [SerializeField] UnityEvent_int OnChangeValue = null;
        Dropdown dropDown;

        [System.Serializable] public class UnityEvent_int : UnityEvent<int>{ }

        public override void Awake() => dropDown = GetComponent<Dropdown>();

        public override void Start()
        {
            isInteractable = false;
            
            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < values.Length; i++)
                data.Add(new Dropdown.OptionData(values[i]));
            dropDown.AddOptions(data);
           
            dropDown.value = PlayerPrefs.GetInt(save_key, Mathf.Clamp(ID_deffaultValue, 0, values.Length - 1));

            dropDown.onValueChanged.AddListener(delegate { DropdownSelect(dropDown); });
            isInteractable = true;
        }

      //  protected void SetupDropdown

        public override int GetValue() => PlayerPrefs.GetInt(save_key);

        public override void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, values.Length - 1);
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
            onChangeCallback?.Invoke();
        }


        void SetDropDownValue(int value)
        {
            value = Mathf.Clamp(value, 0, values.Length - 1);
            OnChangeValue.Invoke(value);
            PlayerPrefs.SetInt(save_key, value);
        }
    }
}
