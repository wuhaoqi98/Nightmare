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
    public float healthRegenerateTime;
    public GameObject[] weapons;
    

    CharacterController controller;
    GameObject indicator;
    GameObject weaponObj;
    private Vector3 handPos;
    private int weaponId = 5;
    private float timer = 0;

    // Use this for initialization
    void Start () {
        healthBar.maxValue = health;
        healthBar.value = health;
        controller = GetComponent<CharacterController>();
        GameObject.Find("OVRCameraRig").transform.localPosition += new Vector3(0, 0, -1);
        indicator = GameObject.Find("Indicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            //transform.position += lHand.transform.forward * speed;
            controller.Move(lHand.transform.forward * speed);
        }

        // on hands
        if(weaponId == 5)
        {
            handPos = rHand.transform.position;
            handPos.y = 0;
            indicator.SetActive(true);
            indicator.transform.position = handPos;
            // grab weapon
            //if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            //{
                Collider[] colliders = Physics.OverlapSphere(handPos, 0.5f);
                foreach (Collider c in colliders)
                {
                    if (c.tag == "Weapon")
                    {
                        switchWeapon(c.GetComponent<Grabbable>());
                    break;
                    }
                }
            //}
            
        }
        else // using weapons
        {
            indicator.SetActive(false);
            // release weapon
            if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) || Input.GetKeyDown(KeyCode.R))
            {
                throwWeapon();
            }
        }

        timer += Time.deltaTime;
        if(timer >= healthRegenerateTime && health < 100)
        {
            health += 1;
            healthBar.value = health;
        }
        if(health == 100)
        {
            timer = 0;
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
        timer = 0;
    }

    private void switchWeapon(Grabbable weapon)
    {
        int id = weapon.id;
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        if(id < 5)
        {
            weapons[id].SetActive(true);
            if(id != 0) // guns
            {
                weapons[id].GetComponent<Gun>().ammo = weapon.ammo;
            }
        }
        weaponId = id;
        weaponObj = weapon.gameObject;
        weaponObj.SetActive(false);
    }

    private void throwWeapon()
    {
        weaponObj.SetActive(true);
        weaponObj.transform.position = weapons[weaponId].transform.position;
        if(weaponId != 0)
        {
            weaponObj.GetComponent<Grabbable>().ammo = weapons[weaponId].GetComponent<Gun>().ammo;
        }
        weapons[weaponId].SetActive(false);
        weaponId = 5;
    }
}
