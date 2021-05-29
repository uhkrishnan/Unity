using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] TrailRenderer LTrail;
    [SerializeField] TrailRenderer RTrail;
    private Rigidbody2D rb;
    private int multiplier;

    private void Start()
    {
        multiplier = 1;
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //create explosion
        Destroy(collision.gameObject);
        gameManager.GetComponent<GameManager>().ExplodeGameOver();
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > 30 * multiplier)
        {
            LTrail.time += 0.3f;
            RTrail.time += 0.3f;
            multiplier++;
        }
    }


}
