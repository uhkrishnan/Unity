using UnityEngine;
using EZ_Pooling;
public class Block : MonoBehaviour
{
    private float gravityScaleFactor = 0.2f;
    private Rigidbody2D rb;
    private int speedCounter; // affects the speed of the blocks
    private float blockSpeed = 2f;
    private Vector3 targetPosition;
    private bool objectSpawned = false;

    //*Lerp Code
    //[SerializeField] [Range(0f, 4f)] float lerpTime;
    private float lerpTime = 0.1f;
    /*[SerializeField] Vector3[] myPositions;
    int posIndex = 0;
    int length;
    float t = 0f;
    */

    //private void Start()
    private void OnSpawned()
    {
        objectSpawned = true;
        rb = GetComponent<Rigidbody2D>();
        speedCounter = 1;
        //increase gravity
        //rb.gravityScale += Time.timeSinceLevelLoad / gravityScaleFactor;
        //rb.gravityScale += gravityScaleFactor;

        targetPosition = new Vector3(this.transform.position.x, -10f, 1);

        //*Lerp code
        //length = myPositions.Length;


    }

    /*
    private void FixedUpdate()
    {
        
        rb.AddForce(transform.up * gravityScaleFactor * -1);
        //Debug.Log("gravityScaleFactor  : " + gravityScaleFactor);


        if (Time.time > 10 * speedCounter)
        {
            gravityScaleFactor += 0.5f;
            speedCounter++;
            //Debug.Log("speedCounter  : " + speedCounter);
        }
        
    }
    */
    
    private void FixedUpdate()
    {
        //MoveBlock(transform.up * -1);
        if (objectSpawned)
        {
            //MoveBlock();

            //* Lerp code

            //transform.position = Vector3.Lerp(transform.position, myPositions[posIndex], lerpTime * Time.deltaTime);


            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpTime * Time.deltaTime);
            /*t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
            if(t> 0.9f)
            {
                t = 0f;
                posIndex++;
                posIndex = (posIndex >= length) ? 0 : posIndex;
            }*/

        }


        if (Time.time > 60 * speedCounter)
        {
            lerpTime += 0.001f;
            speedCounter++;
        }

        if (transform.position.y < -1.5)
        {
            //Destroy(gameObject);
            objectSpawned = false;
            EZ_PoolManager.Despawn(transform);
        }

        /*
        if (Time.time > 10 * speedCounter)
        {
            blockSpeed += 0.1f;
            speedCounter++;
        }*/

    }

    void MoveBlock( )
    {
        rb.velocity = Vector2.up * -1 * blockSpeed;

        /*
        float step = blockSpeed * Time.deltaTime;
        rb.position = Vector3.MoveTowards(rb.position, new Vector3(targetPosition.x, targetPosition.y, 1), step);
        */
    }

}
