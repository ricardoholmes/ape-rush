using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0, 1)]
    public float smoothSpeed;
    public Transform target;
    Vector3 relPos;

    // Start is called before the first frame update
    void Start()
    {
        relPos = target.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = target.position - relPos;
        Debug.Log((transform.position - target.position).magnitude);L
    }
}
