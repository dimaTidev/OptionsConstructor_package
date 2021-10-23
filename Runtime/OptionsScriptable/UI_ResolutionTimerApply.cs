using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace OptionsConstructor
{
    public class UI_ResolutionTimerApply : MonoBehaviour
    {
        [SerializeField] Text text_header;
        [SerializeField] float timer = 12;
        float sec;

        Action<bool> callbackResult;

        [SerializeField] string s_ApplyResolution = "Is apply screen resolution $$?";

        string Get_sText => s_ApplyResolution + "\n$tt$";

        void OnEnable()
        {
            sec = timer;
            text_header.text = sec.ToString("00");
        }

        public void Setup(Action<bool> callbackResult)
        {
            this.callbackResult = callbackResult;
        }

        void Update()
        {
            sec -= Time.deltaTime;
            text_header.text = Get_sText.Replace("$$", Screen.width + "x" + Screen.height).Replace("$tt$", sec.ToString("00"));

            if(sec <= 0)
                Button_No();
        }

        public void Button_No()
        {
            callbackResult?.Invoke(false);
            Exit();
        }

        public void Button_Yes()
        {
            callbackResult?.Invoke(true);
            Exit();
        }

        void Exit() => Destroy(gameObject);
    }
}
