using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMove : MonoBehaviour
{
    [HideInInspector]
    public Vector3 localPos;

    //public bool ignoreX;
    //public bool ignoreY;
    //public bool ignoreZ;

    void Awake()
    {
        localPos = transform.localPosition;
    }

    void Update()
    {
        //Vector3 pos = new Vector3
        //{
        //    x = ignoreX ? transform.localPosition.x : localPos.x,
        //    y = ignoreY ? transform.localPosition.y : localPos.y,
        //    z = ignoreZ ? transform.localPosition.z : localPos.z
        //};
        //transform.localPosition = pos;
        transform.localPosition = localPos;
    }
}
