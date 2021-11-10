using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    public class Option_Runtime_ClipPlane : MonoBehaviour
    {
        /*[PlayerPrefsSelector]*/[SerializeField] string save_key = "";
        [SerializeField] int deffaultValue = 5000;
        public static int value;
        Camera cam;

          void Awake() => OptionRuntimeManager.Subscribe(OnRefresh);// Option_ClipPlane_Dropdown.Subscribe(OnRefresh);
        //void OnDestroy() => Option_ClipPlane_Dropdown.Unsubscribe(OnRefresh);

        

      //  void Start() => OptionRuntimeManager.singleton?.Subscribe(OnRefresh);

        void OnRefresh()
        {
            value = PlayerPrefs.GetInt(save_key);
            if (value <= 0) value = deffaultValue;
            if (!cam) cam = Camera.main;
            cam.farClipPlane = value;
          //  Debug.Log("refresh camera plane = " + value + "/" + cam.farClipPlane);
        }
    }
}
