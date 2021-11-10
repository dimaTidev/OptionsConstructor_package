using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OptionsConstructor{
    public abstract class OptionAbstract : MonoBehaviour
    {

       /*[PlayerPrefsSelector]*/ [SerializeField] protected string save_key;

        protected bool isInteractable = true;

        protected Action onChangeCallback;

        public void Subscribe(Action callback) => onChangeCallback += callback;
        public void Unsubscribe(Action callback) => onChangeCallback -= callback;

        public abstract void Awake();
        public abstract void Start();
        public abstract int GetValue();
        public abstract void SetValue(int value);

        protected void HasKey(int deffaultValue) 
        {
            if (!PlayerPrefs.HasKey(save_key))
                SetValue(deffaultValue);
        }
    }
}
