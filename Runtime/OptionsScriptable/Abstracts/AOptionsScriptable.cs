using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OptionsConstructor
{
    public abstract class AOptionsScriptable : ScriptableObject
    {
        [SerializeField, UID_Random(UIDGeneratorType._byte)] int uid = 0;
        [SerializeField] string label = ""; //TODO: add Localization attribute

        public Action onChange; //Подписываются скрипты

        public abstract IConvertible Get_GenValue();

        public string Label => label;

        protected string LoadID() => PlayerPrefs.GetString(uid.ToString());

        protected void SaveID(string id)
        {
            PlayerPrefs.SetString(uid.ToString(), id);  //Debug.Log($"save to {uid.ToString()}:{id}");
            onChange?.Invoke(); //OnChangeInvoke
        }

        public virtual void SetValue(IConvertible id) => SaveID(id.ToString());
    }
}
