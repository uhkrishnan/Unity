using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{

    
    //[SerializeField] TMP_Text playerNameText;

    private float playerSpeed = 0f;
    private Rigidbody2D rb;
    public GameObject gO;
    private float mapWidth = 2f;
    private float remainingLives = 0;
    private float ScreenWidth, x;

    private void Start()
    {
        remainingLives = 0;
        x = 0;
        ScreenWidth = Screen.width;
        rb = gO.GetComponent<Rigidbody2D>();
        //playerSpeed = 15f;
        playerSpeed = GameDataManager.GetSelectedCharacter().speed;

    }

    private void FixedUpdate()
    {
        int i = 0;
        //loop over every touch found
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                //move right
                x = 1f * Time.deltaTime * playerSpeed;
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                //move left
                x = -1f * Time.deltaTime * playerSpeed;
            }
            ++i;

            Vector2 newPosition = rb.position + Vector2.right * x;
            newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

            rb.MovePosition(newPosition);
        }
    }

}
