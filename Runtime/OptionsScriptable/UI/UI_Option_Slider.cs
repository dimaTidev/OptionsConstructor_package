using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Slider))]
    public class UI_Option_Slider : AUIOption<ASlider>
    {
        bool isInteractable;
        Slider slider;

        protected override void Awake()
        {
            base.Awake();
            slider = GetComponent<Slider>();

            if (option)
                SetupSlider(slider, option.Value, option.MinValue, option.MaxValue);
        }
        void SetupSlider(Slider slider, float value, float minValue, float maxValue)
        {
            if (slider == null)
                return;

            isInteractable = false; //-----------------

            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = value;
            slider.onValueChanged.AddListener(delegate { OnChange(slider); });

            isInteractable = true; //-----------------
        }

        //TODO: optimization OnChange value in end of touch or click! right now it invokes every drag Slider frame!!
        public void OnChange(Slider change)
        {
            if (!isInteractable || !option)
                return;

            if (option)
                option.SetValue(change.value);
        }

        protected override void OnChangeOptionRefresh()
        {
            if (option && slider)
                SetStatus(slider, option.Value);
        }

        void SetStatus(Slider slider, float id)
        {
            if (!slider)
            {
                Debug.LogError("Slider not exist! " + name);
                return;
            }

            isInteractable = false; //-----------------
            slider.value = id;
            isInteractable = true; //-----------------
        }
    }
}
