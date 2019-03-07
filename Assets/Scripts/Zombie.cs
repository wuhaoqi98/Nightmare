using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    GameObject player;
    NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(player.transform.position);
	}
}
