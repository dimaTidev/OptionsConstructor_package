using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OptionsConstructor
{ 
    public abstract class ASInt : AOptionsScriptable
    {
     //  public enum SaveType 
     //  { 
     //      @int = 0, 
     //      @float = 1,
     //      @string = 2
     //  };
     //
     //  [SerializeField] SaveType saveType;

        [SerializeField] protected int defaultValue = 0;

        public int CurId => Parse(LoadID());

        public override IConvertible Get_GenValue() => CurId;

        int Parse(string data)
        {
            if (data == "")
                return defaultValue;

            if (int.TryParse(data, out int id))
            {
                return id;
                //SetValue(id);
            }
            else
            {
                Debug.LogWarning($"Can't parse [{data}] to int");
                return 0;
            }
        }

      //  protected override void OnLoad(string data)
      //  {
      //      base.OnLoad(data);
      //     
      //  }

      // public virtual void SetValue(int id)
      // {
      //     SaveID(id.ToString());
      // }
    }
}
