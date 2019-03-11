using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    Rigidbody rb;
    
    public Transform fireTransform;
    public int damage = 30;
    public float timeBetweenFires;
    public int ammo = 15;

    GameObject hitPoint;
    private Vector3 velocity;
    private Vector3 lastPos;
    private Vector3 newPos;
    private Light gunFlash;
    private float fireTimer = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        lastPos = transform.position;
        gunFlash = fireTransform.GetComponent<Light>();
        gunFlash.enabled = false;
        hitPoint = GameObject.Find("Hit");
	}
	
	// Update is called once per frame
	void Update () {
        newPos = transform.position;
        velocity = newPos - lastPos;
        lastPos = newPos;

        gunFlash.enabled = false;
        RaycastHit hit;
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit))
        {
            //GameObject bloodFX = GameObject.Find("BloodFX");
            //GameObject.Instantiate(bloodFX, hit.transform);
            hitPoint.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0) && fireTimer >= timeBetweenFires && ammo > 0)
            {
                gunFlash.enabled = true;
                GameObject obj = hit.collider.transform.root.gameObject;
                if (obj.tag == "Zombie")
                {
                    obj.GetComponent<Zombie>().receiveDamage(damage);
                }
                fireTimer = 0;
                ammo--;
            }
        }
        fireTimer += Time.deltaTime;
        
    }

    public int getHitDamage()
    {
        return (int)(velocity.magnitude * 1000);
    }
}
