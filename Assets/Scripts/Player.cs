using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform rHand;
    public Transform lHand;
    public GameObject indicator;
    public GameObject walkSign;
    public GameObject teleportSign;
    public float speed = 0.1f;
    public float rotateSpeed = 0.01f;

    private Vector3 startPos;
    private Quaternion startRot;
    private int mode = 0; //0 for teleport, 1 for walk

    // Use this for initialization
    void Start () {
        teleportSign.SetActive(true);
        walkSign.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        indicator.SetActive(false);

        LineRenderer lr = rHand.GetComponent<LineRenderer>();
        lr.SetPosition(0, rHand.transform.position);
        lr.SetPosition(1, rHand.transform.position + rHand.transform.forward * 10);


        if (mode == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(rHand.transform.position, rHand.transform.forward, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.tag == "Floor")
                {
                    indicator.SetActive(true);
                    indicator.transform.position = hit.point;
                    if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
                    {
                        transform.position = hit.point;

                    }

                }

            }
        }

        if (mode == 1)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            {
                startPos = rHand.transform.position;
                startRot = rHand.transform.localRotation;
            }
            else if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
            {
                Vector3 v = rHand.transform.position - startPos;
                v *= speed;
                transform.position += new Vector3(v.x, 0, v.z);
                startPos += new Vector3(v.x, 0, v.z);

                Quaternion r = rHand.transform.localRotation * Quaternion.Inverse(startRot);
                float angle = r.eulerAngles.y;
                if(angle > 180)
                {
                    angle = -(360 - angle);
                }
                angle *= rotateSpeed;
                transform.eulerAngles += new Vector3(0, angle, 0);
            }
        }

        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            
            if (mode == 0)
            {
                mode = 1;
                teleportSign.SetActive(false);
                walkSign.SetActive(true);
            }
            else if (mode == 1)
            {
                mode = 0;
                teleportSign.SetActive(true);
                walkSign.SetActive(false);
            }
        }
    }
}
