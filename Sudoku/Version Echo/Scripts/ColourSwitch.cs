using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourSwitch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image subImage;
    private Color _backgroundColor;
    private int prevCanvasColourValue;

    //List<string> colourList = new List<string>() { "#2D3A49", "#F57F4F", "#32533D", "#E8B447", "#B7386E" };
    List<string> colourList = new List<string>() { "#3E7EC7", "#F57F4F", "#497A5A", "#E8B447", "#DE4587" };

    void Start()
    {
        prevCanvasColourValue = PlayerPrefs.GetInt("canvasColour");
        ColorUtility.TryParseHtmlString(colourList[prevCanvasColourValue], out _backgroundColor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        subImage.color = _backgroundColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        subImage.color = Color.white;

    }


}
