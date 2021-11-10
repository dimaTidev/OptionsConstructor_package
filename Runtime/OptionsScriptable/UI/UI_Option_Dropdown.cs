using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class UI_Option_Dropdown : AUIOption<ADropdown>
    {
        bool isInteractable;
        Dropdown dropdown;

        protected override void Awake()
        {
            base.Awake();
            dropdown = GetComponent<Dropdown>();

           // Debug.Log("UI_Option_Dropdown: Awake " + name + " opt:" + option.name);

            if (option)
                SetupDropdown(dropdown, option.Labels(), option.CurId);
            else
                Debug.LogError("No option:" + gameObject.name);      
        }

        void SetupDropdown(Dropdown dropDown, string[] values, int loadedID)
        {
            Debug.Log("UI_Option_Dropdown: SetupDropdown " + name);
            if (dropDown == null || values == null || values.Length == 0)
                return;

            Debug.Log("UI_Option_Dropdown: SetupDropdown Continue " + name);

            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < values.Length; i++)
                data.Add(new Dropdown.OptionData(values[i]));

           
            dropDown.ClearOptions();
            dropDown.AddOptions(data);
            SetStatus(dropDown, loadedID);
            dropDown.onValueChanged.AddListener(delegate { OnChange(dropDown); });
        }

        public void OnChange(Dropdown change)
        {
            if (!isInteractable || !option)
                return;

            if (option)
                option.SetValue(change.value);
        }

        protected override void OnChangeOptionRefresh()
        {
            if(option && dropdown)
                SetStatus(dropdown, option.CurId);
        }

        void SetStatus(Dropdown dropDown, int id)
        {
            if (!dropdown)
            {
                Debug.LogError("Dropdown not exist! " + name);
                return;
            }
                
            isInteractable = false; //-----------------
            dropDown.value = id;
            isInteractable = true; //-----------------
        }
    }
}
