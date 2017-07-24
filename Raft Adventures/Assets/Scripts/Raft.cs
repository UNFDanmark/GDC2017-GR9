using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Raft : MonoBehaviour {
    //public GameObject StoneHitboxPrefab;
    private GameObject player;
	private EnemyFactoryScript factory;
	public float TipLimit = 5;
    void Awake() {
        player = GameObject.Find("Character"); //gets acess to Character object
		factory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactoryScript>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float[] forces = calcStability(factory.getAllEnemies());

		foreach(float i in forces) {
			if (Math.Abs(i) > TipLimit) {
				print("You Lose");
			}
		}
	}

	void FixedUpdate() {
		
	}

	float[] calcStability(List<GameObject> allEnemies) {
		float forceX = player.GetComponent<Rigidbody>().mass * (player.transform.position.x > 0 ? 1 : -1); //gets 1 if positive and -1 if negative
		float forceZ = player.GetComponent<Rigidbody>().mass * (player.transform.position.z > 0 ? 1 : -1);
		foreach (GameObject GO in allEnemies) {
			//physics rotational mechanisms
			float mass = GO.GetComponent<Rigidbody>().mass;
			forceX += mass * (GO.transform.position.x > 0 ? 1 : -1);
			forceZ += mass * (GO.transform.position.z > 0 ? 1 : -1);
		}
		//print(forceX.ToString() + " " + forceZ.ToString());
		return new float[] { forceX, forceZ };
	}
}
