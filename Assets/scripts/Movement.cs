using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody2D objectRigidbody;
    public float speed;

    // Use this for initialization
    void OnEnable()
    {
        objectRigidbody = transform.GetComponent<Rigidbody2D>();
        objectRigidbody.velocity = transform.up * speed;
    }
}
