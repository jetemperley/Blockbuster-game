using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
     

[RequireComponent(typeof(Slider))]
public class SliderDrag : MonoBehaviour,IPointerUpHandler {

    public string prefName;
    
    public void OnPointerUp(PointerEventData eventData)
    {
        EndDrag(this.GetComponent<Slider>().value);
    }

    public void EndDrag(float val)
    {
        PlayerPrefs.SetFloat(prefName, val);
        PlayerPrefs.Save();
    }

 }