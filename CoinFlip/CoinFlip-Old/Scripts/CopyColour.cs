﻿using UnityEngine;
using UnityEngine.UI;

public class CopyColour : MonoBehaviour
{
    public Canvas canvas;
    public Rigidbody coin;
    public Color themeButtonColor;
    public Button headsButton;
    public Button tailsButton;
    public Button resetButton;
    public Button flipButton;
    public Button flipButtonThree;
    public Button flipButtonFive;
    public Image selectorDot;

    private Vector3 buttonPosition = new Vector3(0, 0, 0);
    private Vector3 defaultPosition = new Vector3(0, -40, 0);

    private void Start()
    {
        Vector3 startVector = GetSelectorDotPos("selectorDotPos");
        if (startVector.y == 0)
        {
            selectorDot.transform.localPosition += defaultPosition;
        }
        selectorDot.transform.localPosition += GetSelectorDotPos("selectorDotPos");
    }

    public void ChangeCanvasColour(Button themeButton)
    {
        themeButtonColor = themeButton.GetComponent<Image>().color;
        
        canvas.GetComponent<Image>().color = themeButtonColor;
        
        flipButton.image.color = themeButtonColor;
        flipButtonThree.image.color = themeButtonColor;
        flipButtonFive.image.color = themeButtonColor;
        headsButton.image.color = themeButtonColor;
        tailsButton.image.color = themeButtonColor;
        resetButton.image.color = themeButtonColor;
        coin.GetComponent<MeshRenderer>().material.SetColor("_Color", themeButtonColor);

        buttonPosition.x = themeButton.transform.localPosition.x;

        selectorDot.transform.localPosition = new Vector3(0f, -40f, 0f);
        selectorDot.transform.localPosition += buttonPosition;

        SaveSelectorDotPos(selectorDot.transform.localPosition, "selectorDotPos");
        SaveColor(themeButtonColor, "canvasColor");
    }

    public static void SaveSelectorDotPos(Vector3 localPosition, string key)
    {
        PlayerPrefs.SetFloat(key + "X", localPosition.x);
        PlayerPrefs.SetFloat(key + "Y", localPosition.y);
        PlayerPrefs.SetFloat(key + "Z", localPosition.z);
    }


    public static Vector3 GetSelectorDotPos(string key)
    {
        float X = PlayerPrefs.GetFloat(key + "X");
        float Y = PlayerPrefs.GetFloat(key + "Y");
        float Z = PlayerPrefs.GetFloat(key + "Z");
        return new Vector3(X, Y, Z);
    }

    public static void SaveColor(Color color, string key)
    {
        PlayerPrefs.SetFloat(key + "R", color.r);
        PlayerPrefs.SetFloat(key + "G", color.g);
        PlayerPrefs.SetFloat(key + "B", color.b);
    }

    

}
