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


        //TODO: onChange
        public Action onChange; //Подписываются скрипты

        public abstract IConvertible Get_GenValue();

        //TODO: load
        //   void Load()
        //   {
        //       string data = LoadID();
        //       Debug.Log($"load: {data}");
        //       OnLoad(data);
        //   }

        public string Label => label;

        protected string LoadID() => PlayerPrefs.GetString(uid.ToString());

    //    protected virtual void OnLoad(string data){}

        //TODO: save
        protected void SaveID(string id)
        {
            PlayerPrefs.SetString(uid.ToString(), id);

            Debug.Log($"save to {uid.ToString()}:{id}");

            onChange?.Invoke(); //OnChangeInvoke
        }

        public virtual void SetValue(IConvertible id)
        {
            SaveID(id.ToString());
        }
    }
}
