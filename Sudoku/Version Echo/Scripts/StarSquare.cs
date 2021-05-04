using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StarSquare : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private float waitTime = 3.0f;
    private float timer = 0.0f;
    private float visualTime = 0.0f;
    public Vector3 scaleSize = new Vector3(2, 2 ,1);
    public Vector3 originalSize = new Vector3(1, 1, 1);
    public Text starText;
    private int notebuttonstate;
    public Image gridSprite;
    public Text previousText;

    private void Start()
    {
        previousText.text = " ";
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
        if (timer > waitTime)
        {
            visualTime = timer;

            // Remove the recorded 2 seconds.
            timer = timer - waitTime;
            if(starText.text == "X")
            {
                starText.color = Color.black;
                //starText.rectTransform.localScale = originalSize;
                //starText.text = " ";
                starText.text = previousText.text;
            }
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("StarSquare.Clicked!!!!!");
        
        //notebuttonstate = PlayerPrefs.GetInt("notebuttonstate");

        //Debug.Log("StarSquare.Notebutton state is : " + notebuttonstate);
        //if(notebuttonstate != 1)
        /*if (notebuttonstate != 1 && starText.text == " ")
        {
            starText.color = Color.white;
            starText.rectTransform.localScale = scaleSize;
            starText.text = "O";
        }*/

        previousText.text = starText.text;
        if (starText.text == " " || gridSprite.color == Color.red)
        {
            starText.color = Color.white;
            //starText.rectTransform.localScale = scaleSize;
            starText.text = "X";
        }
        
    }

   
}
