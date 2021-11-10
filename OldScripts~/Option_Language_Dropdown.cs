using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_Language_Dropdown : MonoBehaviour
    {
        /*[PlayerPrefsSelector]*/[SerializeField] string save_key = "language";
        [SerializeField] string[] values = null;
        [SerializeField] string[] valuesToShow = null;
        [SerializeField] int ID_deffaultValue = 0;
        [SerializeField] Text text_warning = null;
        Dropdown dropDown;

        void Awake() => dropDown = GetComponent<Dropdown>();

        public void Start()
        {
            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < valuesToShow.Length; i++)
                data.Add(new Dropdown.OptionData(valuesToShow[i]));
            dropDown.options = data;

            dropDown.value = PlayerPrefs.GetInt(save_key, Mathf.Clamp(ID_deffaultValue, 0, values.Length - 1));

            dropDown.onValueChanged.AddListener(delegate { DropdownSelect(dropDown); });
        }

        public void DropdownSelect(Dropdown change) => SetDropDownValue(change.value);

        void SetDropDownValue(int value)
        {
            value = Mathf.Clamp(value, 0, values.Length - 1);
            PlayerPrefs.SetInt(save_key, value);
            //TODO: Set id to languageManager!!
           // SmartLocalization.LanguageManager.Instance.ChangeLanguage(values[PlayerPrefs.GetInt(save_key)]);
            if (text_warning)
                text_warning.gameObject.SetActive(true);
        }
    }
}
