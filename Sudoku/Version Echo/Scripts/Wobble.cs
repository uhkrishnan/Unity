using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Wobble : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    private Vector3 scaleOffset = new Vector3(1, 1, 0);
    private Vector3 defaultOffset = new Vector3(1, 1, 1);
    private Vector3 scaledOffset = new Vector3(2, 2, 1);
    private float wobbleDisplaycement = 0.3f;
    public GameObject menuButton1;
    public GameObject menuButton2;


    /*public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("inside click");
        this.gameObject.transform.localScale = defaultOffset;
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if(this.gameObject.transform.localScale == defaultOffset)
        //{
            //gameObject.GetComponent<Image>().enabled = true;

            switch (gameObject.tag)
            {
                case "playButton":
                    menuButton1.GetComponent<Transform>().position += new Vector3(0, -wobbleDisplaycement, 0);
                    menuButton2.GetComponent<Transform>().position += new Vector3(0, -wobbleDisplaycement, 0);
                    break;
                case "continueButton":
                    menuButton1.GetComponent<Transform>().position += new Vector3(0, wobbleDisplaycement, 0);
                    menuButton2.GetComponent<Transform>().position += new Vector3(0, -wobbleDisplaycement, 0);
                    break;
                case "highScoresButton":
                    menuButton1.GetComponent<Transform>().position += new Vector3(0, wobbleDisplaycement, 0);
                    menuButton2.GetComponent<Transform>().position += new Vector3(0, wobbleDisplaycement, 0);
                    break;
            }
            
            //this.gameObject.transform.localScale += scaleOffset;
        //}
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if(this.gameObject.transform.localScale == scaledOffset)
        //{
            //gameObject.GetComponent<Image>().enabled = false;
            
            switch (gameObject.tag)
            {
                case "playButton":
                    menuButton1.GetComponent<Transform>().position -= new Vector3(0, -wobbleDisplaycement, 0);
                    menuButton2.GetComponent<Transform>().position -= new Vector3(0, -wobbleDisplaycement, 0);
                    break;
                case "continueButton":
                    menuButton1.GetComponent<Transform>().position -= new Vector3(0, wobbleDisplaycement, 0);
                    menuButton2.GetComponent<Transform>().position -= new Vector3(0, -wobbleDisplaycement, 0);
                    break;
                case "highScoresButton":
                    menuButton1.GetComponent<Transform>().position -= new Vector3(0, wobbleDisplaycement, 0);
                    menuButton2.GetComponent<Transform>().position -= new Vector3(0, wobbleDisplaycement, 0);
                    break;
            }

            //this.gameObject.transform.localScale -= scaleOffset;
        //}
        
    }

    

}
