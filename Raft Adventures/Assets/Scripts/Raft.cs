using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Raft : MonoBehaviour {
    //public GameObject StoneHitboxPrefab;
    private GameObject player;
	private EnemyFactoryScript factory;
	public float TipLimit = 5;
	float[] forces = new float[2];
	float loaded;
	public float weightLimit = 10;
    void Awake() {
        player = GameObject.Find("Character"); //gets acess to Character object
		factory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactoryScript>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		List<GameObject> allEnemies = factory.getAllEnemies();
		float[] forces = calcStability(allEnemies);
		float loaded = calcLoad(allEnemies);
		checkLose();
	}

	void checkLose() {
		
		
		foreach (float i in forces) {
			if (Math.Abs(i) >= TipLimit||loaded >= weightLimit) {
				print("You Lose");
			}
		}
	}
	float[] calcStability(List<GameObject> allEnemies) {
		float forceX = player.GetComponent<Rigidbody>().mass * (player.transform.position.x > 0 ? 1 : -1); //gets 1 if positive and -1 if negative
		float forceZ = player.GetComponent<Rigidbody>().mass * (player.transform.position.z > 0 ? 1 : -1);
		float load = 0;
		foreach (GameObject GO in allEnemies) {
			//physics rotational mechanisms
			float mass = GO.GetComponent<AbstractEnemy>().Mass;
			//print(mass);
			forceX += mass * (GO.transform.position.x > 0 ? 1 : -1);
			forceZ += mass * (GO.transform.position.z > 0 ? 1 : -1);
		}
		print(forceX.ToString() + " " + forceZ.ToString());
		return new float[] { forceX, forceZ };
	}
	float calcLoad(List<GameObject> allEnemies) {
		float tempMass = 0;
		foreach(GameObject GO in allEnemies) {
			tempMass +=GO.GetComponent<AbstractEnemy>().Mass;
		}
		return tempMass;
	}
}
