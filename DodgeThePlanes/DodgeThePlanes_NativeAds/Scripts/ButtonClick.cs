using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public void ClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
    }

}
