using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    public class OptionApply_Singleton : Singleton<OptionApply_Singleton>
    {
        List<ABase> list = new List<ABase>();

        [SerializeField] protected SO_intArray fpsOption = null;
        [SerializeField] protected AToggle fogOption = null;
        [SerializeField] protected SO_Resolution resolutionOption = null;

        #region Classes
        public abstract class ABase
        {
            public abstract void Awake();
            public abstract void OnDestroy();
        }
        public abstract class ABase<T> : ABase where T : AOptionsScriptable
        {
            [SerializeField] protected T option;

            public override void Awake()
            {
                if (!option)
                    return;

                option.onChange -= OnChange;
                option.onChange += OnChange;
                OnChange();
            }

            public override void OnDestroy()
            {
                if (option)
                    option.onChange -= OnChange;
            }
            public abstract void OnChange();
        }
        public class FPS : ABase<ADropdown<int>>
        {
            public FPS(ADropdown<int> option) => this.option = option;
            public override void OnChange()
            {
                int value = option.Value;
                if (value == 0)
                {
                    Debug.LogError("impossible value in FPS option");
                    return;
                }


                Debug.Log("FrameRate set to: " + value);
                Application.targetFrameRate = value;
            }
        }
        public class FOG : ABase<AToggle>
        {
            public FOG(AToggle option) => this.option = option;
            public override void OnChange()
            {
                RenderSettings.fog = option.Value;
            }
        }

       public class Resolution : ABase<SO_Resolution>
       {
            public Resolution(SO_Resolution option) => this.option = option;
            public override void OnChange()
            {
                Vector2Int resolution = option.Value;

                if (option.IsNewChange)
                {

                    GameObject prefab_ResApplyTimer = Resources.Load<GameObject>("OptionConstructor/panel_ResolutionApplyTimer");
                    if (prefab_ResApplyTimer)
                    {
                        GameObject go = Instantiate(prefab_ResApplyTimer);
                        UI_ResolutionTimerApply scr = go.GetComponent<UI_ResolutionTimerApply>();
                        if (!scr)
                            Destroy(go);
                        else
                            scr.Setup(OnApply_Result);
                    }
                }
                Screen.SetResolution(resolution.x, resolution.y, true);
            }

            /// <summary>
            /// Is apply Changes
            /// </summary>
            public void OnApply_Result(bool result)
            {
                if (result)
                    option.Refresh_LastIdResolution();
                else
                    option.Reset_Back();
            }

            public override void OnDestroy()
            {
                base.OnDestroy();
                if(option)
                    Screen.SetResolution(option.Value_default.x, option.Value_default.y, true);
            }
        }

        [ContextMenu("Read data")]
        public void Read()
        {
            Debug.Log("List: " + list.Count);
        }
        #endregion


        protected override void Awake()
        {
            list = Construct();
            for (int i = 0; i < list.Count; i++)
                list[i].Awake();
        }

        protected override void OnDestroy()
        {
            if(list != null)
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null)
                        list[i].OnDestroy();
                }   
        }

        List<ABase> Construct()
        {
            List<ABase> list = new List<ABase>();
            if (fpsOption)
                list.Add(new FPS(fpsOption));
            if (fogOption)
                list.Add(new FOG(fogOption));
            if (resolutionOption)
                list.Add(new Resolution(resolutionOption));
            return list;
        }
    }
}
