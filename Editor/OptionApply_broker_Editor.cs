using OptionsConstructor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(OptionApply_broker))]
[CanEditMultipleObjects]
public class OptionApply_brokerEditor : Editor
{
     SerializedProperty option;

    string[] eventsNames =
    {
        "onChange_int",
        "onChange_bool",
        "onChange_float",
        "onChange_string"
    };

    int idSelected = -1;
    SerializedProperty[] events;

    void OnEnable()
    {
       
        option = serializedObject.FindProperty("option");

        events = new SerializedProperty[eventsNames.Length];
        for (int i = 0; i < events.Length; i++)
            events[i] = serializedObject.FindProperty(eventsNames[i]);

        Refresh(option);
    }

    public override void OnInspectorGUI()
    {
       // DrawDefaultInspector();

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(option); // new GUIContent("Vector Object")
        if (EditorGUI.EndChangeCheck())
            Refresh(option);
        

        for (int i = 0; i < events.Length; i++)
        {
            GUI.color = idSelected == i || idSelected == -1 ? Color.white : Color.grey;
            GUI.enabled = idSelected == i || idSelected == -1;
            EditorGUILayout.PropertyField(events[i]);
        }
        GUI.enabled = true;
        GUI.color = Color.white;
        //
        // EditorGUILayout.PropertyField(damageProp, new GUIContent("Property"));
        //


        serializedObject.ApplyModifiedProperties();
    }

    void Refresh(SerializedProperty option)
    {
        if (option.objectReferenceValue is AOptionsScriptable opt)
        {
            Type returnType = opt.Get_GenValue().GetType();

            if (returnType == typeof(int))
            {
                if (opt is AToggle)
                    idSelected = 0;
                else
                    idSelected = 1;
            }
            else if (returnType == typeof(float))
                idSelected = 2;
            else if (returnType == typeof(string))
                idSelected = 3;
        }
        else
            idSelected = -1;
    }
}
