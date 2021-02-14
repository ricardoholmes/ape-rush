using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0, 1)]
    public float smoothSpeed;
    public Transform target;
    float relX;

    // Start is called before the first frame update
    void Start()
    {
        relX = target.position.x - transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = target.position.x - relX;

        //Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothSpeed);
        //transform.position = smoothPosition;
        transform.position = targetPos;
    }
}
