using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OptionsConstructor
{
    public class Option_Runtime_Fog : MonoBehaviour
    {
        /*[PlayerPrefsSelector]*/ [SerializeField] string save_key = "";

        void Awake() => OptionRuntimeManager.Subscribe(OnRefresh);

        void OnRefresh()
        {
            int value = PlayerPrefs.GetInt(save_key);
            RenderSettings.fog = value == 0 ? false : true;
        }
    }
}
