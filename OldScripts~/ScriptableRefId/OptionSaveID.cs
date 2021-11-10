using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SaveID", menuName = "ScriptableObjects/OptionsConstructor/SaveID")]
public class OptionSaveID : ScriptableObject
{
    [SerializeField] ushort uid = 0;
    public ushort UID => uid;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (uid == 0)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(OptionSaveID).Name);
            OptionSaveID[] a = new OptionSaveID[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<OptionSaveID>(path);
            }

            bool isMatch = true;

            while (isMatch)
            {
                uid = (ushort)Random.Range(1, ushort.MaxValue);
                isMatch = false;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].GetInstanceID() == this.GetInstanceID())
                        continue;

                    if (a[i].UID == uid)
                    {
                        isMatch = true;
                        break;
                    }
                }
            }
        }
    }
#endif
}
