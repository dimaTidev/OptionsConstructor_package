using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    public abstract class AOptionApply : MonoBehaviour
    {
        [SerializeField] protected AOptionsScriptable option = null;

        protected virtual void Awake()
        {
            if (!option)
                return;

            option.onChange -= OnChange;
            option.onChange += OnChange;
            OnChange();
        }

        protected virtual void OnDestroy()
        {
            if (option)
                option.onChange -= OnChange;
        }

        public abstract void OnChange();
    }
}
