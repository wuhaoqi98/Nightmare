using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Weapon {

    public Transform fireTransform;
    public float timeBetweenFires;
    public int ammo = 15;
    public bool fullAuto;

    GameObject hitPoint;
    Text text;
    private Light gunFlash;
    private float fireTimer = 0;

    // Use this for initialization
    protected override void Start() {
        gunFlash = fireTransform.GetComponent<Light>();
        gunFlash.enabled = false;
        hitPoint = GameObject.Find("Hit");
        text = GetComponentInChildren<Text>();
        text.text = ammo.ToString();
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        gunFlash.enabled = false;
        RaycastHit hit;
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit))
        {
            hitPoint.transform.position = hit.point - fireTransform.forward * 0.1f;
        }
        fireTimer += Time.deltaTime;

        // fire

        if ((Input.GetMouseButtonDown(0) 
            || (!fullAuto && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) 
            || (fullAuto && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))) 
            && fireTimer >= timeBetweenFires && ammo > 0)
        {

            gunFlash.enabled = true;
            audioSource.Play();
            fireTimer = 0;
            ammo--;
            text.text = ammo.ToString();

            RaycastHit hit2;
            if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit2))
            {
                GameObject obj = hit2.collider.transform.root.gameObject;
                if (obj.tag == "Zombie")
                {
                    blood.transform.position = hit2.point - fireTransform.forward;
                    blood.transform.LookAt(transform.position);
                    blood.Play();
                    obj.GetComponent<Zombie>().receiveDamage(damage);
                }
            }
        }
    }
}
