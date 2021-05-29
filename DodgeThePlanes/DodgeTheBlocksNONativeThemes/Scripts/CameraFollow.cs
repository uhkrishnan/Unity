using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;

    void Start()
    {
        target = Movement.Instance.transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(0f, target.position.y + 2f, target.position.z);
    }
}
