using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public int damage = 30;
    
    protected AudioSource audioSource;
    protected ParticleSystem blood;

	// Use this for initialization
	protected virtual void Start () {
        audioSource = GetComponent<AudioSource>();
        blood = GameObject.Find("BloodFX").GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    void Update() {

    }
}
