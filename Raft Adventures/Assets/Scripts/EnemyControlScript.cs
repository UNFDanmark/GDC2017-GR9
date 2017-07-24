using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControlScript : MonoBehaviour {

	List<AbstractEnemy> allEnemies = new List<AbstractEnemy>();
    public EnemyFactoryScript factory;
	public int cooldown = 1;
	private int lastSpawn = 0;

    // Use this for initialization
    void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if (!(Time.timeSinceLevelLoad - lastSpawn < cooldown)){
			AbstractEnemy tempEnemy = factory.getEnemy();
			tempEnemy.Intro(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
		}
	}
}



