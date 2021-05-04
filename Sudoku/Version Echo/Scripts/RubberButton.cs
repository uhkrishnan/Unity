using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class RubberButton : MonoBehaviour, IPointerClickHandler
//public class RubberButton : Selectable, IPointerClickHandler
{

    /*public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<CopyCanvasColour>().enabled = false;
        GameEvents.OnClearNumberMethod();
    }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        //gameObject.GetComponent<CopyCanvasColour>().enabled = true;
        //image.sprite = Resources.Load<Sprite>("Delete_pressed_True");
        GameEvents.OnClearNumberMethod();
    }
    
}
