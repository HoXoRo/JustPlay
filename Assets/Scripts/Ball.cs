using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    private Vector3 lastDir;
    private float speed = 30;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(1, 1, 0) * speed;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        lastDir = rigidbody.velocity;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Vector3 reflexAngle = Vector3.Reflect(lastDir, collision.contacts[0].normal);
            rigidbody.velocity = reflexAngle.normalized * lastDir.magnitude;
        }
    }
}
