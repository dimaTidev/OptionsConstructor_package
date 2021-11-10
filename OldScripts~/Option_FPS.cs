using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(InputField))]
    public class Option_FPS : OptionAbstract
    {
       // /*[PlayerPrefsSelector]*/
       // [SerializeField] string save_key;
        [SerializeField] int deffault = 0;
        InputField inputField;

        public override void Awake() => inputField = GetComponent<InputField>();

        public override void Start()
        {
            isInteractable = false;

            inputField.text = PlayerPrefs.GetInt(save_key, deffault).ToString();

            if (int.TryParse(inputField.text, out int fps))
                SetState(fps);
            inputField.onValueChanged.AddListener(delegate { OnInputField(inputField); });
            isInteractable = true;
        }

        public override int GetValue() => PlayerPrefs.GetInt(save_key);

        public override void SetValue(int value)
        {
            value = Mathf.Clamp(value, 30, 60);

            isInteractable = false;
            SetState(value);
            inputField.text = value.ToString();
            isInteractable = true;
        }

        public void OnInputField(InputField change)
        {
            if (!isInteractable) return;
            if(int.TryParse(change.text, out int fps))
                SetState(fps);
        }
        
        void SetState(int value)
        {
            value = Mathf.Clamp(value, 30, 60);
            Application.targetFrameRate = value;
            PlayerPrefs.SetInt(save_key, value);
        }
    }
}
