using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Vector3 direction;
    public float angle;
    public Transform target;
    void Update()
    {

        direction = transform.position - target.position;
        angle = Vector3.Angle(transform.position, direction);
        if (angle < 5)
            Debug.Log("Close");

        
    }
}
