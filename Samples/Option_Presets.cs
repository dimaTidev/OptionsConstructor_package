using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_Presets : MonoBehaviour
    {
        [SerializeField] string customName = "";
        [SerializeField] PresetData[] presets = null;

        Dropdown dropDown;

        [System.Serializable]
        public struct PresetData
        {
            public string name;
            public OptionAbstract[] options;
            public int[] values;
        }

        void Awake()
        {
            dropDown = GetComponent<Dropdown>();
            if (!dropDown) return;

            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < presets.Length; i++)
                data.Add(new Dropdown.OptionData(presets[i].name));
            data.Add(new Dropdown.OptionData(customName));

            dropDown.options = data;
            dropDown.onValueChanged.AddListener(delegate { DropdownSelect(dropDown); });

            if(presets != null && presets.Length > 0)
          //  for (int i = 0; i < presets.Length; i++)
            for (int k = 0; k < presets[0].options.Length; k++)
                presets[0].options[k].Subscribe(Refresh);
        }

        void Start() => Refresh();//StartCoroutine(Initialize());

       /* IEnumerator Initialize()
        {
            yield return new WaitForEndOfFrame();
            Refresh();
        }*/


        void OnDestroy()
        {
            for (int i = 0; i < presets.Length; i++)
                for (int k = 0; k < presets[i].options.Length; k++)
                    if(presets[i].options[k])
                        presets[i].options[k].Unsubscribe(Refresh);
        }

        void DropdownSelect(Dropdown change)
        {
            if (change.value == change.options.Count - 1) return;
            int id = Mathf.Clamp(change.value, 0, presets.Length - 1);
            for (int i = 0; i < presets[id].options.Length; i++)
                if(presets[id].options[i] != null && presets[id].options[i].gameObject.activeSelf)
                    presets[id].options[i].SetValue(presets[id].values[i]);
            //Refresh();
        }

        void Refresh()
        {
            int id = GetIdPreset();
            dropDown.value = id >= 0 ? id : dropDown.options.Count - 1;
        }

        int GetIdPreset()
        {
            int id = -1;
            for (int i = 0; i < presets.Length; i++)
            {
                id = i;
                for (int k = 0; k < presets[i].options.Length; k++)
                    if(presets[i].options[k].GetValue() != presets[i].values[k])
                        id = -1;
                if (id >= 0)
                    break;
            }
            return id;
        }
    }
}
