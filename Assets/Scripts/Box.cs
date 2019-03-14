using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(10, 10, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
