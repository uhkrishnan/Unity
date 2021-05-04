using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject[] skins;
    //[SerializeField] TMP_Text playerNameText;

    public float playerSpeed = 0f;
    public float playerPower = 0;
    private Rigidbody2D rb;
    private float mapWidth = 2f;

    static Character selectedCharacter;
    private float ScreenWidth, x;

    //Life Handling
    public GameObject[] lifeSpawnPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerSpeed = 15f;
        playerSpeed = GameDataManager.GetSelectedCharacter().speed;
        playerPower = GameDataManager.GetSelectedCharacter().power;

        ChangePlayerSkin();
        for (int i = 0; i < playerPower; i++)
        {
            lifeSpawnPoints[i].SetActive(true);
        }

    }

    void ChangePlayerSkin()
    {
        Character character = GameDataManager.GetSelectedCharacter();
        if (character.image != null)
        {

            // Get selected character's index:
            int selectedSkin = GameDataManager.GetSelectedCharacterIndex();

            // show selected skin's gameobject:
            skins[selectedSkin].SetActive(true);
            
            playerSpeed = GameDataManager.GetSelectedCharacter().speed;
            playerPower = GameDataManager.GetSelectedCharacter().power;


            // hide other skins (except selectedSkin) :
            for (int i = 0; i < skins.Length; i++)
                if (i != selectedSkin)
                    skins[i].SetActive(false);


            //playerNameText.text = character.name;
        }
    }

    /*
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;

        Vector2 newPosition = rb.position + Vector2.right * x;
        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

        rb.MovePosition(newPosition);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;
        
        if(tag.Equals("coin"))
        {
            GameDataManager.AddCoins(15);
            GameSharedUI.Instance.UpdateCoinsUIText();

            Destroy(collision.gameObject);
        }
        else
        {
            //code to decrease lives 
            playerPower--;
            lifeSpawnPoints[(int)playerPower].SetActive(false);

            if (playerPower == 0)
            {
                FindObjectOfType<GameManager>().GameOver();

            }
            else
            {
                FindObjectOfType<GameManager>().EndGame();
                Destroy(collision.gameObject, 0.5f);
            }

            //FindObjectOfType<GameManager>().EndGame();

        }
        
    }





}
