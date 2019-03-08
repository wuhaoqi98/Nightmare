using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform rHand;
    public Transform lHand;
    public Transform head;
    public float speed = 0.1f;
    public float rotateSpeed = 0.01f;

    private Vector3 startPos;
    private Quaternion startRot;
    private int mode = 0; //0 for teleport, 1 for walk

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            transform.position += lHand.transform.forward * speed;
        }
    }
}
