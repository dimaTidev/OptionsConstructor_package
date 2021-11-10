using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    public class Option_Runtime_MaterialSwitcherArray : MonoBehaviour
    {
        /*[PlayerPrefsSelector]*/
        [SerializeField] string saveKey = "";
        [SerializeField] bool isDayNightSwitch = false;
        [SerializeField] MeshRenderer[] targets = null;
        int mode; //0-simple, 1-not simple
        int isDayNight;

        [SerializeField] MatData[] data = null; //0-simple, 1- not simple

        [System.Serializable]
        public struct MatData
        {
#if UNITY_EDITOR
            public string name;
            public Material[] EDITOR_materials;
    #endif
            public string[] paths; //0 - day, 1 - night
        }

        void Awake()
        {
            OptionRuntimeManager.Subscribe(OnRefresh);
            if(isDayNightSwitch)
                Option_Runtime_DayNightSwitcher.Subscribe(OnRefresh);
        }

        public void OnRefresh()
        {
            mode = PlayerPrefs.GetInt(saveKey);
            Material mat = Resources.Load<Material>(GetPath(mode, isDayNight));
            if (targets == null || targets.Length == 0) return;
            for (int i = 0; i < targets.Length; i++)
                if(targets[i] != null)
                    targets[i].material = mat;
        }

        public void OnRefresh(int id)
        {
            isDayNight = id;
            OnRefresh();
        }

        string GetPath(int id, int isDay)
        {
            if (id < 0 || id >= data.Length) return "";
            if (isDay < 0 || isDay >= data[id].paths.Length)
                if(data[id].paths.Length > 0) return data[id].paths[0];
            else
                return "";

            return data[id].paths[isDay];
        }

        public void SetDayNight(int isDayNight)
        {
            this.isDayNight = isDayNight;
            OnRefresh();
        }

        #region Editor
#if UNITY_EDITOR

        void OnValidate()
        {
            if (data == null) return;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].EDITOR_materials == null) continue;

                //data[i].EDITOR_paths = new string[data[i].EDITOR_materials.Length];
                data[i].paths = new string[data[i].EDITOR_materials.Length];

                for (int k = 0; k < data[i].EDITOR_materials.Length; k++){
                    //data[i].EDITOR_paths[k] = UnityEditor.AssetDatabase.GetAssetPath(data[i].EDITOR_materials[k]);
                    data[i].paths[k] = Path_toResourcesPath(UnityEditor.AssetDatabase.GetAssetPath(data[i].EDITOR_materials[k]));
                }
                
            }

            if(data != null && 0 < data.Length)
                data[0].name = "Simple";

            if (data != null && 1 < data.Length)
                data[1].name = "Normal";

            if (data != null && 2 < data.Length)
                data[2].name = "High";

        }

        string Path_toResourcesPath(string targetPath)
        {
            string returValue = targetPath;
            string rem = "Resources/";
            if (returValue.Contains(rem))
                returValue = returValue.Remove(0, returValue.IndexOf(rem) + rem.Length);

            rem = ".mat";
            if (returValue.Contains(rem))
                returValue = returValue.Remove(returValue.IndexOf(rem));
            return returValue;
        }
#endif
        #endregion
    }
}
