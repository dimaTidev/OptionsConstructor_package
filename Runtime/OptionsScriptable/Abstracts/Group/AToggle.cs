﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    [CreateAssetMenu(fileName = "Bool", menuName = "ScriptableObjects/OptionsConstructor/Bool")]
    public class AToggle : ASInt
    {
        public bool Value => CurId != 0;

        private void OnValidate()
        {
            defaultValue = Mathf.Clamp(defaultValue, 0, 1);
        }

        public override void SetValue(IConvertible id)
        {
            if(id is bool value)
            {
                base.SetValue(value ? 1 : 0);
            }
            else
                base.SetValue(id);
        }
    }
}
