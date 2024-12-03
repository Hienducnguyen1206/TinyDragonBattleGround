using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoMove : MonoBehaviour
{
    [SerializeField] private Rigidbody body; 
    [SerializeField] private float speed = 3f; 

    void FixedUpdate()
    {
        
        body.velocity = Vector3.right * speed;
    }
}
