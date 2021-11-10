using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Toggle))]
    public class Option_Toggle_LUT : Option_Toggle
    {
        public override void Start()
        {
            if (!SystemInfo.supports3DTextures)
            {
                Debug.Log("System Not Support 3d Textures");
                gameObject.SetActive(false);
            } else
                base.Start();
        }
    }
}
