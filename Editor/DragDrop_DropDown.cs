using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OptionsConstructor
{
    [CustomEditor(typeof(ADropdown), true)]
    [CanEditMultipleObjects]
    public class DragDrop_DropDown : Editor
    {
        // public override void OnInspectorGUI()
        // {
        //     EditorGUILayout.LabelField("it works");
        //     DrawDefaultInspector();
        // }

      //  private void OnEnable()
      //  {
      //      EditorApplication.hierarchyWindowChanged -= OnHierarchyChanged;
      //      EditorApplication.hierarchyWindowChanged += OnHierarchyChanged;
      //
      //  }
      //
      //
      //  private void OnDisable()
      //  {
      //      EditorApplication.hierarchyWindowChanged -= OnHierarchyChanged;
      //  }
      //
      //  static void OnHierarchyChanged()
      //  {
      //      if (Event.current.type == EventType.MouseUp)
      //      {
      //        Debug.Log("Hierarhy changed done");
      //      }
      //      
      //  }


        internal void OnSceneDrag(SceneView sceneView)
        {

            Event e = Event.current;
            GameObject go = HandleUtility.PickGameObject(e.mousePosition, false);

            if (e.type == EventType.DragUpdated)
            {
                Debug.Log("DragUpdated");
                if (go)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                }
                else
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                }
                e.Use();
            }
            else if (e.type == EventType.DragPerform)
            {
                Debug.Log("Drag performed");
                DragAndDrop.AcceptDrag();
               

              //  e.Use();
              //
              //  DynamicHair dhcomponent = go ? go.GetComponent<DynamicHair>() : null;
              //  if (!dhcomponent)
              //  {
              //      go = new GameObject(target.name);
              //      Plane floor = new Plane(Vector3.up, Vector3.zero);
              //      Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
              //      Vector3 pos = r.GetPoint(1);
              //      floor.RaycastDoublesided(r, ref pos);
              //      go.transform.position = pos;
              //      Selection.activeGameObject = go;
              //  }
              //  go.AddComponent<DynamicHair>().Data = target as DynamicHairData;
            }
        }
    }
}
