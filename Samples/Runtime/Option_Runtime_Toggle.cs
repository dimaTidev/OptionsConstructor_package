using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OptionsConstructor
{
    public class Option_Runtime_Toggle : MonoBehaviour
    {
        [SerializeField] bool isEnable = true;

#if UNITY_EDITOR
#pragma warning disable 414  // CS0414 Unused variable.
        [SerializeField, TextArea] string ONLYEDITOR_notes = "инструкция на всякий случай";
#pragma warning restore 414
#endif

        /*[PlayerPrefsSelector]*/
        [SerializeField] string save_key = "";

        [SerializeField] UnityEvent_int OnRefreshValue = null;

        [SerializeField] UnityEvent OnTrue = null;
        [SerializeField] UnityEvent OnFalse = null;

        [System.Serializable]
        public class UnityEvent_int : UnityEvent<int>
        {
        }

        void Awake() => OptionRuntimeManager.Subscribe(OnRefresh);

        protected virtual void OnRefresh() 
        {
            if (!isEnable) return;

            //Debug.Log("OnRefresh " + name);
            int value = PlayerPrefs.GetInt(save_key);
            OnRefreshValue.Invoke(value);
            if (value == 0)
                OnFalse.Invoke();
            else
                OnTrue.Invoke();
        }
    }
}
