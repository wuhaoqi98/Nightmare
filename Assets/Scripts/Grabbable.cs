using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    public int id;
    public int ammo;

    GameObject sphere;

	// Use this for initialization
	void Start () {
        sphere = transform.Find("Sphere").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (ammo == 0)
            sphere.SetActive(false);
        else
            sphere.SetActive(true);
    }
}
