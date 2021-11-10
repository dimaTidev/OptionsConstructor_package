using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_FPS_Dropdown : ADropdownUI
    {
        ///*[PlayerPrefsSelector]*/ [SerializeField] string save_key;
        [SerializeField] int[] values = null;
       // [SerializeField] int ID_deffaultValue;
        Dropdown dropDown;

        public override void Awake() => dropDown = GetComponent<Dropdown>();

        public override void Start()
        {
            HasKey(ID_deffaultValue);

            int idFps = ConvertFPS_to_ID(LoadID()); //конвертация с FPS в id 
            SetDropDownValue(idFps);

            SetupDropdown(dropDown, values, LoadID());
        }

        public override int GetValue() => ConvertFPS_to_ID(LoadID());

        int ConvertFPS_to_ID(int fps)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (fps == values[i])
                {
                    return i;
                }
            }
            return 0;
        }

        public override void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, values.Length - 1);

            isInteractable = false;
            SetDropDownValue(value);
            dropDown.value = value;
            isInteractable = true;
        }


        protected override void SetDropDownValue(int value)
        {
            base.SetDropDownValue(value);

            value = Mathf.Clamp(value, 0, values.Length - 1);
            value = values[value];
            
            Application.targetFrameRate = value;
            PlayerPrefs.SetInt(save_key, value); //Сохраняет полное значение FPS
        }
    }
}
