using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Option_InfoHit : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Range(0, 1)] float hitValue = 0;
    [SerializeField] Image image_hit = null;
    [SerializeField] GameObject panel_info = null;
 //   [SerializeField] Image image_buttonInfo = null;

    void OnDisable()
    {
        if (panel_info)
            panel_info.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panel_info)
            panel_info.SetActive(true);
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (image_hit)
            image_hit.fillAmount = hitValue;
     //   if (image_buttonInfo)
    //        image_buttonInfo.enabled = panel_info;
    }
#endif

}
