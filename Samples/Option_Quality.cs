using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_Quality : OptionAbstract
    {
      //  /*[PlayerPrefsSelector]*/ [SerializeField] string save_key;
        Dropdown dropDown;
        [SerializeField] int ID_deffaultValue = 0;

        public override void Awake() => dropDown = GetComponent<Dropdown>();
        public override void Start()
        {
            HasKey(ID_deffaultValue);

            isInteractable = false;
            string[] keys = QualitySettings.names;

            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(save_key, Mathf.Clamp(keys.Length - 2, 0, keys.Length - 1)));

            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < keys.Length; i++)
                data.Add(new Dropdown.OptionData(keys[i]));
            dropDown.options = data;
            dropDown.value = QualitySettings.GetQualityLevel();
            dropDown.onValueChanged.AddListener(delegate { DropdownSelect_Quality(dropDown); });
            isInteractable = true;
        }

        public override int GetValue() => PlayerPrefs.GetInt(save_key, Mathf.Clamp(QualitySettings.names.Length - 2, 0, QualitySettings.names.Length - 1));

        public override void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, QualitySettings.names.Length - 1);
            isInteractable = false;
            Set_Quality(value);
            dropDown.value = value;
            isInteractable = true;
        }

        public void DropdownSelect_Quality(Dropdown change)
        {
            if (!isInteractable) return;

            Set_Quality(Mathf.Clamp(change.value, 0, QualitySettings.names.Length - 1));
            onChangeCallback?.Invoke();
        }

        public void Set_Quality(int id)
        {
            id = Mathf.Clamp(id, 0, QualitySettings.names.Length - 1);
            QualitySettings.SetQualityLevel(id);
            PlayerPrefs.SetInt(save_key, QualitySettings.GetQualityLevel());
            if(dropDown)
                dropDown.value = id;
        }
    }
}
