using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject chair_prefab;
    public GameObject desk_prefab;
    public Transform rHand;
    public Transform lHand;
    public Transform spawn;
    public GameObject gameManager;

    public float spawnDistance = 6;

    private GameObject temp;
    private bool button1Down;
    private bool button2Down;
    private bool trigger1Down;
    private List<GameObject> furnitures;
    private float fixedDist;
    private Vector3 translation;
    private Quaternion objRotation;
    private Quaternion handRotation;
    private float handsDist;
    private float timer;
    private float scale;

    // Use this for initialization
    void Start() {
        furnitures = new List<GameObject>();
        handsDist = Vector3.Magnitude(rHand.position - lHand.position);
        scale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        spawnChair();
        spawnDesk();
        
        for (int i = 0; i < furnitures.Count; i++)
        {
            if (furnitures[i].GetComponent<Light>())
            {
                furnitures[i].GetComponent<Light>().enabled = false;
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(rHand.transform.position, rHand.transform.forward, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.tag == "Furniture")
            {
                selectItem(obj);
            }
            
        }

        grabItem();

        resize();
    }

    private void spawnChair()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GameObject chair = GameObject.Instantiate(chair_prefab, spawn);
            chair.transform.localScale = new Vector3(scale, scale, scale);
            temp = chair;
            chair.GetComponent<Rigidbody>().isKinematic = true;
            button1Down = true;

        }
        else if (button1Down && OVRInput.Get(OVRInput.Button.One))
        {
            temp.transform.position = rHand.transform.position + rHand.transform.forward * spawnDistance * scale;
            temp.transform.localRotation = rHand.transform.localRotation;

        }
        else if (button1Down)
        {
            temp.GetComponent<Rigidbody>().isKinematic = false;
            button1Down = false;
            temp.tag = "Furniture";
            furnitures.Add(temp);
            temp.transform.parent = gameManager.transform;
        }
    }

    private void spawnDesk()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            GameObject desk = GameObject.Instantiate(desk_prefab, spawn);
            temp = desk;
            desk.transform.localScale = new Vector3(scale, scale, scale);
            desk.GetComponent<Rigidbody>().isKinematic = true;
            button2Down = true;

        }
        else if (button2Down && OVRInput.Get(OVRInput.Button.Two))
        {
            temp.transform.position = rHand.transform.position + rHand.transform.forward * spawnDistance * scale;
            temp.transform.localRotation = rHand.transform.localRotation;
        }
        else if (button2Down)
        {
            temp.GetComponent<Rigidbody>().isKinematic = false;
            button2Down = false;
            temp.tag = "Furniture";
            furnitures.Add(temp);
            temp.transform.parent = gameManager.transform;
        }
    }

    private void selectItem(GameObject obj)
    {
        obj.GetComponent<Light>().enabled = true;
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            temp = obj;
            temp.GetComponent<Rigidbody>().isKinematic = true;
            fixedDist = Vector3.Magnitude(temp.transform.position - rHand.transform.position);
            translation = temp.transform.position - rHand.transform.position;
            trigger1Down = true;
            objRotation = temp.transform.localRotation;
            handRotation = Quaternion.Inverse(rHand.transform.localRotation);
        }
    }

    private void grabItem()
    {
        if (trigger1Down && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            temp.transform.position = rHand.transform.position + rHand.transform.forward * fixedDist;
            temp.transform.localRotation = rHand.transform.localRotation * handRotation * objRotation;

        }
        else if (trigger1Down)
        {
            temp.GetComponent<Rigidbody>().isKinematic = false;
            trigger1Down = false;
        }
    }

    private void resize()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            float dist = Vector3.Magnitude(rHand.position - lHand.position);
            float diff = dist - handsDist;
            diff *= 2;
            gameManager.transform.localScale += new Vector3(diff, diff, diff);
            scale = gameManager.transform.localScale.x;
            handsDist = dist;
        }
        else
        {
            handsDist = Vector3.Magnitude(rHand.position - lHand.position);
        }

        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            gameManager.transform.localScale = new Vector3(1, 1, 1);
            scale = 1;
        }
    }
}
