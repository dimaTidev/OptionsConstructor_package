using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OptionsConstructor
{
    public abstract class ASlider : ASFloat
    {
        [SerializeField] Vector2 minMaxValues = new Vector2(0, 1);

        public float MinValue => minMaxValues.x;
        public float MaxValue => minMaxValues.y;

        private void OnValidate()
        {
            if (defaultValue < minMaxValues.x)
                defaultValue = minMaxValues.x;
            if (defaultValue > minMaxValues.y)
                defaultValue = minMaxValues.y;
            
            //defaultValue = Mathf.Clamp(defaultValue, minMaxValues.x, minMaxValues.y);
        }
    }
}
