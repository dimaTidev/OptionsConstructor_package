using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace OptionsConstructor
{
    public class Option_Runtime_LUT : Option_Runtime_Toggle
    {
        [SerializeField] UnityEvent onNotSupport = null;

        protected override void OnRefresh()
        {
            if (!SystemInfo.supports3DTextures)
            {
                Debug.Log("System Not Support 3d Textures");
                onNotSupport.Invoke();
            } else
                base.OnRefresh();
        }
    }
}
