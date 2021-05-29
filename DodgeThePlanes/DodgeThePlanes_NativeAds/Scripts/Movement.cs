using UnityEngine;

// Player Movement Script

public class Movement : MonoBehaviour
{
    #region Singleton class: Movement
    public static Movement Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    private float upSpeed;
    private float horizontalSpeed;
    //[SerializeField] Transform backgroundImage;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private  float timeMultiplier;
    private float leftBound = 0f;
    private float rightBound = 0f;

    private void Start()
    {
        //FRAME RATE LIMIT
        QualitySettings.vSyncCount = 1; // enabled
        Application.targetFrameRate = 30;

        horizontalSpeed = 2f;
        upSpeed = 2f;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        FindObjectOfType<AudioManager>().Play("GameTheme");
        rb = GetComponent<Rigidbody2D>();
        
        
        timeMultiplier = 1f;
        
        leftBound = -screenBounds.x + 0.5f;
        rightBound = screenBounds.x - 0.5f;

        GameManager.ResumeGame(); // try to bring time scale to 1 when playing again after quit.
        

    }

    private void MoveUP()
    {
        rb.velocity = Vector2.up * upSpeed;
    }

    private void MoveLeft()
    {
        rb.AddForce(transform.right * -2000 * horizontalSpeed * Time.deltaTime);
        //backgroundImage.position += new Vector3(0.003f, 0f, 0f);
    }

    private void MoveRight()
    {
        rb.AddForce(transform.right * 2000 * horizontalSpeed * Time.deltaTime);
        //backgroundImage.position += new Vector3(-0.003f, 0f, 0f);
    }


    //private void Update()
    private void FixedUpdate()
    {
        MoveUP();
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        /*
        //if(Time.time > timeMultiplier * 60)
        if (Time.time > timeMultiplier * 30)
        {
            //upSpeed += 0.1f;
            upSpeed += 0.2f;
            timeMultiplier++;
        }
        */

        // TOUCH COUNTROLS
        int i = 0;
        //loop over every touch found
        while (i < Input.touchCount)
        {

            //Debug.Log(Input.GetTouch(i).position);
            if (Input.GetTouch(i).position.x > Screen.width/2)
            {
                //move right
                MoveRight();
            }
            if (Input.GetTouch(i).position.x < Screen.width/2)
            {
                //move left
                MoveLeft();
            }
            ++i;
        }
    }

    private void LateUpdate()
    {
        Vector3 viewPos = rb.transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + 0.5f, screenBounds.x - 0.5f);        
        viewPos.x = Mathf.Clamp(viewPos.x, leftBound, rightBound);
        rb.transform.position = viewPos;
    }


}
