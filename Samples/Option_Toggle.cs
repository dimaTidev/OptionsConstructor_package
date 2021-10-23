using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Toggle))]
    public class Option_Toggle : OptionAbstract
    {
       // /*[PlayerPrefsSelector]*/[SerializeField] string save_key;
        [SerializeField] bool deffault = false;
        [SerializeField] UnityEvent_bool OnChangeValue = null;
        Toggle toggle;
        [System.Serializable]
        public class UnityEvent_bool : UnityEvent<bool>
        {
        }

        public override void Awake() => toggle = GetComponent<Toggle>();

        public override void Start()
        {
            HasKey(deffault == true ? 1 : 0);

            isInteractable = false;

            toggle.isOn = PlayerPrefs.GetInt(save_key, deffault ? 1 : 0) == 1 ? true : false;

            SetToggleState(toggle.isOn ? 1 : 0);
            toggle.onValueChanged.AddListener(delegate { OnToggleClick(toggle.isOn); });
            isInteractable = true;
        }

        public override int GetValue() => PlayerPrefs.GetInt(save_key);

        public override void SetValue(int value)
        {
            isInteractable = false;
            SetToggleState(value);
            toggle.isOn = value == 1 ? true : false;
            isInteractable = true;
        }

        public void OnToggleClick(bool isOn)
        {
            if (!isInteractable) return;
            SetToggleState(isOn ? 1 : 0);
            onChangeCallback?.Invoke();
        }

        void SetToggleState(int value)
        {
            OnChangeValue.Invoke(value == 1 ? true : false);
            PlayerPrefs.SetInt(save_key, value);
            OptionRuntimeManager.singleton?.Refresh();
        }
    }
}
