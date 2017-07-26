using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Raft : MonoBehaviour {
    //public GameObject StoneHitboxPrefab;
    private GameObject player;
	private EnemyControlScript factory;
	public float TipLimit = 5;
	float[] forces = new float[2];
	float TotalWeight;
	public float weightLimit = 10;
	public float nullifier = 0;
	public float RotateFactor = 10;
	public float LoseRotation = 10;

	private Quaternion _facing;

    void Awake() {
        player = GameObject.Find("Character"); //gets acess to Character object
		factory = GameObject.Find("EnemyControl").GetComponent<EnemyControlScript>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		List<GameObject> allEnemies = factory.getAllEnemies();
		forces = calcStability(allEnemies);
		TotalWeight = calcLoad(allEnemies);
		UpdateRotation();
		UpdatePlaceInWater();
		checkLose();
	}

	void checkLose() {
		//print(transform.eulerAngles.x.ToString() + " " + transform.eulerAngles.z.ToString());
		if (!((360 - LoseRotation < transform.eulerAngles.x || transform.eulerAngles.x < LoseRotation )&&( 360 - LoseRotation < transform.eulerAngles.z || transform.eulerAngles.z < LoseRotation))) {//fuck that
			print("You Lose");
		}
		if (TotalWeight >= weightLimit) {
			print("You Lose2");
		}
	}

	void UpdateRotation() {
		//print(forces[0].ToString() + " " + forces[1].ToString());
		transform.Rotate(new Vector3(forces[1], 0, -forces[0]) * Time.deltaTime * (1 / RotateFactor), Space.World);
		float step = nullifier * Time.deltaTime * (1 / RotateFactor) * 6.28f / 360;

		transform.rotation = Quaternion.LookRotation(Vector3.MoveTowards(transform.forward,new Vector3(0, 0, 1),step),transform.up);
		transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.MoveTowards(transform.up, new Vector3(0, 1, 0), step));
	}

	void UpdatePlaceInWater() {
		Vector3 moveToPoint = new Vector3(transform.position.x, -(TotalWeight / weightLimit) * transform.lossyScale.y/2, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, moveToPoint, 0.05f*Time.deltaTime);
	}
	float[] calcStability(List<GameObject> allEnemies) {
		float forceX = player.GetComponent<Rigidbody>().mass * (player.transform.position.x > 0 ? 1 : -1); //gets 1 if positive and -1 if negative
		float forceZ = player.GetComponent<Rigidbody>().mass * (player.transform.position.z > 0 ? 1 : -1);
		foreach (GameObject GO in allEnemies) {
			float mass = GO.GetComponent<AbstractEnemy>().Mass;
			forceX += mass * (GO.transform.position.x > 0 ? 1 : -1);
			forceZ += mass * (GO.transform.position.z > 0 ? 1 : -1);
		}
		//print(forceX.ToString() + " " + forceZ.ToString());
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
