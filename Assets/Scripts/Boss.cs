using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Zombie {

    public float timeBetweenSpawn;
    public GameObject boss_small;
    public Transform spawnTransform;

    private float timer = 0;
	
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (GameManager.mode == 1)
            return;
        timer += Time.deltaTime;
        if(timer >= timeBetweenSpawn && GameObject.FindGameObjectsWithTag("Zombie").Length <= 100)
        {
            GameObject obj = Instantiate(boss_small);
            obj.transform.position = spawnTransform.position;
            obj.transform.rotation = spawnTransform.rotation;
            timer = 0;
        }
	}
}
