using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasColor : MonoBehaviour
{
    private Color _backgroundColor;
    public Image backgroundImage;
    private int canvasColourValue;
    //List<string> colourList = new List<string>() { "#2D3A49", "#F57F4F", "#32533D", "#E8B447", "#B7386E" };
    List<string> colourList = new List<string>() { "#3E7EC7", "#F57F4F", "#497A5A", "#E8B447", "#DE4587" };

    public void Start()
    {
        canvasColourValue = Random.Range(0, 5);
        ColorUtility.TryParseHtmlString(colourList[canvasColourValue], out _backgroundColor);
        backgroundImage.color = _backgroundColor;
        PlayerPrefs.SetInt("canvasColour", canvasColourValue);
    }
}
