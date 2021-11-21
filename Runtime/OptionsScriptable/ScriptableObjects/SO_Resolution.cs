using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsConstructor
{
    [CreateAssetMenu(fileName = "Bool", menuName = "ScriptableObjects/OptionsConstructor/Standard/Resolution")]
    public class SO_Resolution : ADropdown
    {
        List<Vector2Int> values = null;

        int lastId = 0;
        public bool IsNewChange
        {
            get
            {
               // Debug.Log($"lastId:{lastId} != CurId:{CurId}");
                return lastId != CurId;
            }
        }

        public void Refresh_LastIdResolution() => lastId = CurId;
        public Vector2Int Value
        {
            get
            {
                //Debug.Log($"Value   lastId:{lastId} != CurId:{CurId}");
                GenerateResolutions();
               // Debug.Log($"Value  gen lastId:{lastId} != CurId:{CurId}");

                if (values == null || values.Count == 0)
                    return new Vector2Int(Screen.width, Screen.height);

                if (CurId < 0 || CurId >= values.Count)
                {
                    if (CurId < 0 || CurId >= values.Count)
                        return values[defaultValue];
                    else
                    {
                        //Debug.LogError("defaultValue is out of Range of values");
                        return new Vector2Int(Screen.width, Screen.height);
                    }
                }

                return values[CurId];
            }
        }

        public Vector2Int Value_default
        {
            get
            {
                if (values == null || defaultValue < 0 || defaultValue >= values.Count)
                    return new Vector2Int(Screen.width, Screen.height);
                return values[defaultValue];
            }
        }
        public override string[] Labels()
        {
            GenerateResolutions();
            if (values == null || values.Count == 0)
                return new string[0];

            string[] labels = new string[values.Count];
            for (int i = 0; i < labels.Length; i++)
                labels[i] = values[i].x + "x" + values[i].y;
            return labels;
        }

        void GenerateResolutions()
        {
            if(values == null || values.Count == 0)
            {
                values = new List<Vector2Int>
                {
                    new Vector2Int(Screen.width, Screen.height),
                    new Vector2Int((int)(Screen.width / 1.5f), (int)(Screen.height / 1.5f)),
                    new Vector2Int(Screen.width / 2, Screen.height / 2),
                    new Vector2Int((int)(Screen.width / 2.5f), (int)(Screen.height / 2.5f))
                };

                values.Reverse();

                defaultValue = values.Count - 1;
                Refresh_LastIdResolution();
            }
        }

        public void Reset_Back() => base.SetValue(lastId);

        public override void SetValue(IConvertible id)
        {
            Refresh_LastIdResolution();
            base.SetValue(id);
        }
       
    }
}
