using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    public int health = 100;

    GameObject player;
    NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        nav.SetDestination(player.transform.position);
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            int damage = (int)(weapon.velocity.magnitude * 1000);
            health -= damage;
            
            Debug.Log( damage);
        }
    }
}
