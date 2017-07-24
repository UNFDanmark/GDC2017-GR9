using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControlScript : MonoBehaviour {

	
    public EnemyFactoryScript factory;
	public int cooldown = 10;
	private float lastSpawn = 0;

    // Use this for initialization
    void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if (!(Time.timeSinceLevelLoad - lastSpawn < cooldown)){
			GameObject tempEnemy = factory.getEnemy();
			float posX = Random.Range(-4.5f, 4.5f);
			float posZ = Random.Range(-4.5f, 4.5f);
			tempEnemy.GetComponent<AbstractEnemy>().Intro(posX,posZ); //activates the enemies entrance throug
			lastSpawn = Time.timeSinceLevelLoad;
		}
	}
}



