using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
   private Vector4 origColour;
   public Vector4 hovColour;
   public Text text;

   void Start()
   {
      origColour = GetComponent<Outline>().effectColor;
   }

   public void OnClick()
   {
      GetComponent<Outline>().effectColor = origColour;
      text.GetComponent<Outline>().effectColor = origColour;
   }

   public void OnPointerEnter(PointerEventData eventData)
   {
      GetComponent<Outline>().effectColor = hovColour;
      text.GetComponent<Outline>().effectColor = hovColour;
   }

   public void OnPointerExit(PointerEventData eventData)
   {
      GetComponent<Outline>().effectColor = origColour;
      text.GetComponent<Outline>().effectColor = origColour;
   }
}
