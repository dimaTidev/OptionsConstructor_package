using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Toggle))]
    public class UI_Option_Toggle_sliderBased : UI_Option_Toggle
    {
      //  Toggle toggle;
      //
      //  protected override void Awake() 
      //  {
      //      base.Awake();
      //      toggle = GetComponent<Toggle>();
      //      toggle.onValueChanged.AddListener(delegate {
      //          OnChangeToggle(toggle);
      //      });
      //  }
      //
      //  public void OnChangeToggle(Toggle toggle)
      //  {
      //      if (!toggle.interactable)
      //          return;
      //
      //      if(option)
      //          option.SetValue(toggle.isOn);
      //  }
      //
      //
      //  protected override void OnChangeOptionRefresh()
      //  {
      //      if (option && toggle)
      //          SetStatus(toggle, option.Value);
      //  }
      //
      //  void SetStatus(Toggle toggle, bool id)
      //  {
      //      if (!toggle)
      //      {
      //          Debug.LogError("Slider not exist! " + name);
      //          return;
      //      }
      //
      //      toggle.interactable = false; //-----------------
      //      toggle.isOn = id;
      //      toggle.interactable = true; //-----------------
      //  }
    }
}
