using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    public abstract class AUIOption<T> : MonoBehaviour where T : AOptionsScriptable
    {
        [SerializeField] protected T option;
        //s
        [SerializeField] Text text_label = null;

        protected virtual void OnEnable()
        {
            if (option)
            {
                option.onChange -= OnChangeOptionRefresh;
                option.onChange += OnChangeOptionRefresh;
            }
            OnChangeOptionRefresh();
        }

        protected virtual void OnDisable()
        {
            if (option)
                option.onChange -= OnChangeOptionRefresh;
        }

        protected abstract void OnChangeOptionRefresh();
        protected virtual void Awake()
        {
            if(!option)
                Debug.LogWarning("Not Exist option in AUIOption on: " + name);

            if(text_label && option)
                text_label.text = option.Label;
        }

        private void OnValidate()
        {
            if (!gameObject.transform.parent) //Awoid a requrcively rename in project View
                return;

            string temp;
            if (option)
                temp = typeof(T).Name + "_" + option.name;
            else
                temp = typeof(T).Name + "_Empty";
            if (name != temp)
                name = temp;
        }
    }
}