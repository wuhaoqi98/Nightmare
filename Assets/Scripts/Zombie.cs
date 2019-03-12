using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    public int health = 100;
    public float activeRange = 10;

    GameObject player;
    NavMeshAgent nav;
    Animator anim;

    private bool isDead = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead)
            return;

        if(health <= 0)
        {
            onDeath();
            return;
        }
        
        float dist = (transform.position - player.transform.position).magnitude;
        if(dist <= activeRange)
        {
            nav.isStopped = false;
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.isStopped = true;
        }
        
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Katana>())
        {
            Katana katana = other.GetComponent<Katana>();
            int damage = katana.getHitDamage();
            health -= damage;
            
            Debug.Log( damage);
        }
    }

    private void onDeath()
    {
        nav.enabled = false;
        anim.applyRootMotion = false;
        anim.SetTrigger("isDead");
        Destroy(gameObject, 3);
        isDead = true;
    }

    public void receiveDamage(int damage)
    {
        health -= damage;
    }
}
