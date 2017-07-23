using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControlScript : MonoBehaviour {

	List<AbstractEnemy> allEnemys = 
    private EnemyFactoryScript factory;
	public List
    // Use this for initialization
    void Awake () {
        factory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactoryScript>();
        factory.getEnemy();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}



