using UnityEngine;

namespace OptionsConstructor
{
    public class Option_Runtime_FPS : MonoBehaviour
    {
        /*[PlayerPrefsSelector]*/ [SerializeField] string save_key = "";
        [SerializeField] int ID_deffaultValue = 0;

        void Awake() => Application.targetFrameRate = PlayerPrefs.GetInt(save_key, ID_deffaultValue);
    }
}
