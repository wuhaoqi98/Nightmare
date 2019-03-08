using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    Rigidbody rb;
    public Vector3 velocity;

    private Vector3 lastPos;
    private Vector3 newPos;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        newPos = transform.position;
        velocity = newPos - lastPos;
        lastPos = newPos;
        
	}
}
