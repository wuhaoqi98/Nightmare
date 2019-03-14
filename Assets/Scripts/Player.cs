using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Transform rHand;
    public Transform lHand;
    public Slider healthBar;
    public GameObject minimapCam;
    public float speed = 0.1f;
    public int health = 100;

    CharacterController controller;

    // Use this for initialization
    void Start () {
        healthBar.maxValue = health;
        healthBar.value = health;
        controller = GetComponent<CharacterController>();
        GameObject.Find("OVRCameraRig").transform.localPosition += new Vector3(0, 0, -1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            //transform.position += lHand.transform.forward * speed;
            controller.Move(lHand.transform.forward * speed);
        }
    }

    private void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.y = minimapCam.transform.position.y;
        minimapCam.transform.position = newPos;
        minimapCam.transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
    }

    public void receiveDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
    }
}
