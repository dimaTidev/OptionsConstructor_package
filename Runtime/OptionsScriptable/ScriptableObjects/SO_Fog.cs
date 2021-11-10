using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    [CreateAssetMenu(fileName = "fog", menuName = "ScriptableObjects/OptionsConstructor/Standard/Fog")]
    public class SO_Fog : AToggle
    {
        public override void SetValue(IConvertible id)
        {

            base.SetValue(id);
        }
    }
}
