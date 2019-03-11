using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform rHand;
    public Transform lHand;
    public float speed = 0.1f;
    public int health = 100;

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

    public void receiveDamage(int damage)
    {
        health -= damage;
    }
}
