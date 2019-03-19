using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon {

    Rigidbody rb;
    
    private Vector3 velocity;
    private Vector3 lastPos;
    private Vector3 newPos;


    // Use this for initialization
    protected override void Start () {
        rb = GetComponent<Rigidbody>();
        lastPos = transform.position;
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
        newPos = transform.position;
        velocity = newPos - lastPos;
        lastPos = newPos;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            other.GetComponent<Zombie>().receiveDamage(getHitDamage());
            audioSource.Play();
        }
    }
   

    public int getHitDamage()
    {
        int hitDamage = (int)(velocity.magnitude * damage);
        if (hitDamage > 200)
            hitDamage = 200;
        Debug.Log(hitDamage);
        return hitDamage;
    }
}
