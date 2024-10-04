using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{

    public Rigidbody platform;
    public Transform point1;
    public Transform point2;
    public bool goingTo1;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        platform.position = point1.position;
        goingTo1 = false;
    }

    private void FixedUpdate()
    {
        if (goingTo1)
        {
            platform.MovePosition(Vector3.MoveTowards(platform.position, point1.position, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            platform.MovePosition(Vector3.MoveTowards(platform.position, point2.position, moveSpeed * Time.fixedDeltaTime));
        }

        if(platform.position == point1.position)
        {
            goingTo1 = false;
        }

        if(platform.position == point2.position)
        {
            goingTo1 = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(point1.position, point2.position);
    }
}
