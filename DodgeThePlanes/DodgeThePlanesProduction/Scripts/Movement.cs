using UnityEngine;

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
    private Rigidbody2D rb;
    [SerializeField] RectTransform leftSpawner;
    [SerializeField] RectTransform rightSpawner;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;

        horizontalSpeed = 2f;
        upSpeed = 2f;
        FindObjectOfType<AudioManager>().Play("GameTheme");
        rb = GetComponent<Rigidbody2D>();
        GameManager.ResumeGame();
    }

    private void MoveUP()
    {
        if(Time.timeSinceLevelLoad > 60)
        {
            upSpeed = 2.5f;
        }
        else if (Time.timeSinceLevelLoad > 120)
        {
            upSpeed = 3f;
            horizontalSpeed = 2.25f;
        }
        else if (Time.timeSinceLevelLoad > 180)
        {
            upSpeed = 3.5f;
        }
        rb.velocity = Vector2.up * upSpeed;
    }

    private void MoveLeft()
    {
        rb.AddForce(transform.right * -2000 * horizontalSpeed * Time.deltaTime);
    }

    private void MoveRight()
    {
        rb.AddForce(transform.right * 2000 * horizontalSpeed * Time.deltaTime);
    }
    
    private void FixedUpdate()
    {
        //unity
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }


        MoveUP();
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > Screen.width/2)
            {
                MoveRight();
            }
            if (Input.GetTouch(i).position.x < Screen.width/2)
            {
                MoveLeft();
            }
            ++i;
        }
    }

    private void LateUpdate()
    {
        Vector3 viewPos = rb.transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, leftSpawner.position.x, rightSpawner.position.x);
        rb.transform.position = viewPos;
    }
}