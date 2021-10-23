using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OptionsConstructor
{
    [RequireComponent(typeof(Dropdown))]
    public class Option_Resolution : OptionAbstract
    {
        ///*[PlayerPrefsSelector]*/ [SerializeField] string save_key;
        [SerializeField] int deffault = 0;
        Dropdown dropDown;

        Vector2Int[] resolutions;
    //    [SerializeField] Toggle toggle_fullScreen;
        int idResolution;

        public override void Awake()
        {
            dropDown = GetComponent<Dropdown>();
            //create resolution lvls--------------------------------
            List<Vector2Int> resolutions = new List<Vector2Int>();
            resolutions.Add(new Vector2Int(Screen.width, Screen.height));
            resolutions.Add(new Vector2Int((int)(Screen.width / 1.5f), (int)(Screen.height / 1.5f)));
            resolutions.Add(new Vector2Int(Screen.width / 2, Screen.height / 2));
            resolutions.Add(new Vector2Int((int)(Screen.width / 2.5f), (int)(Screen.height / 2.5f)));
            resolutions.Reverse();
            this.resolutions = resolutions.ToArray();
            //---------------------------------
        }

        void OnDestroy() => Screen.SetResolution(resolutions[resolutions.Length - 1].x, resolutions[resolutions.Length - 1].y, true);

        public override void Start()
        {
            isInteractable = false;
            /* if (!toggle_fullScreen)
             {
                 gameObject.SetActive(false);
                 return;
             }*/

            //Setup Toogle--------------------------------
            /*  if (PlayerPrefs.HasKey(save_key_full_screen))
                  toggle_fullScreen.isOn = PlayerPrefs.GetInt(save_key_full_screen) == 1 ? true : false;
              else
                  toggle_fullScreen.isOn = true;

              toggle_fullScreen.onValueChanged.AddListener(delegate { OnToggleClick(toggle_fullScreen);});*/
            //---------------------------------

            //Setup DropDown--------------------------------

            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            for (int i = 0; i < this.resolutions.Length; i++)
                data.Add(new Dropdown.OptionData(this.resolutions[i].x.ToString() + "x" + this.resolutions[i].y));

            dropDown.options = data;

            //idResolution = PPrefs_int.GetInt(PlayerPrefs, Mathf.Clamp(deffault, 0, resolutions.Length - 1));
            idResolution = PlayerPrefs.GetInt(save_key, Mathf.Clamp(deffault, 0, resolutions.Length - 1));

            /*if (PPrefs_int.HasKey(PlayerPrefs))
                idResolution = PPrefs_int.GetInt(PlayerPrefs);
            else
                idResolution = Mathf.Clamp(deffault, 0, resolutions.Length - 1);*/

            ChangeResolution(idResolution);
            dropDown.value = idResolution;

            dropDown.onValueChanged.AddListener(delegate { ChangeResolution(dropDown); });
            //---------------------------------
            isInteractable = true;
        }

        public override int GetValue() => PlayerPrefs.GetInt(save_key, deffault);

        public override void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, resolutions.Length - 1);
            isInteractable = false;
            ChangeResolution(value);
            dropDown.value = value;
            isInteractable = true;
        }

        public void ChangeResolution(Dropdown change){
            if (!isInteractable) return;

            ChangeResolution(change.value);
            onChangeCallback?.Invoke();
        }

        void ChangeResolution(int id)
        {
            id = Mathf.Clamp(id, 0, resolutions.Length - 1);
            Screen.SetResolution(resolutions[id].x, resolutions[id].y, true);
            idResolution = id;
            PlayerPrefs.SetInt(save_key, id);
        }

        /*  public void OnToggleClick(Toggle change)
          {
              Screen.SetResolution(resolutions[idResolution].x, resolutions[idResolution].y, change.isOn);
              PlayerPrefs.SetInt(save_key_full_screen, change.isOn ? 1 : 0);
          }*/
    }
}
