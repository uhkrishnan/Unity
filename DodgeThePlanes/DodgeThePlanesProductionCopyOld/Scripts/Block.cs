using UnityEngine;
using EZ_Pooling;

public class Block : MonoBehaviour
{
    private Vector2 screenBounds;

    private void OnSpawned()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "block")
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }        
    }

    private void FixedUpdate()
    {
        if (Movement.Instance.transform.position.y > screenBounds.y + 20f)
        {
            EZ_PoolManager.Despawn(transform);
        }
    }
}
