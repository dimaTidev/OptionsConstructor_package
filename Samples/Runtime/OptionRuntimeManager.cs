using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OptionsConstructor
{
    [DefaultExecutionOrder(-100)]
    public class OptionRuntimeManager : MonoBehaviour
    {
        public static OptionRuntimeManager singleton;
        static Action onRefreshCallbacks;

        [Tooltip("Указать что бы в Start инициализировало все настройки")]
        [SerializeField] Transform parentAllOptions = null;
        [SerializeField] OptionAbstract[] options = null;

        /*   [ContextMenu("delete all")]
           public void Cleadr()
           {
               PlayerPrefs.DeleteAll();
           }*/

        void Awake()
        {
            singleton = this;
            transform.SetParent(null); //потому что нельзя делать DontDestroyOnLoad если дочерний
            DontDestroyOnLoad(this.gameObject);

            GetAllOptions();

            if (options != null)
                for (int i = 0; i < options.Length; i++)
                    options[i].Awake();
        }

        void Start() 
        {
           /* Debug.Log("options: " + options.Length);

            for (int i = 0; i < options.Length; i++)
                Debug.Log("options.name: " + options[i].name);
            */
            if(options != null)
                for (int i = 0; i < options.Length; i++)
                    options[i].Start();

            if(options == null || options.Length == 0)
                onRefreshCallbacks?.Invoke();
        }


        [ContextMenu("Get All Options")]
        void GetAllOptions()
        {
            if (parentAllOptions && options.Length == 0)
                options = parentAllOptions.GetComponentsInChildren<OptionAbstract>(true);
        }

        public static void Subscribe(Action callback) => onRefreshCallbacks += callback;

        public static void Unsubscribe(Action callback) => onRefreshCallbacks -= callback;

        public void Refresh()
        {
            Debug.Log("Take Changes");
            if (coroutine == null)
                coroutine = StartCoroutine(RefreshDelay());
        }

        static Coroutine coroutine;

        public static IEnumerator RefreshDelay()
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("Apply Changes");
            coroutine = null;
            onRefreshCallbacks?.Invoke();
        }
    }
}
