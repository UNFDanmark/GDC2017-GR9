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
	float TotalWeight;
	public float weightLimit = 10;
	public float nullifier = 2;
	public float RotateFactor = 10;
	public float LoseRotation = 10;

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
		float forceX = Math.Abs(forces[0]) > nullifier ? (forces[0] - nullifier * Math.Sign(forces[0])) : 0; //makes sure to substract the nullifier
		float forceZ = Math.Abs(forces[1]) > nullifier ? (forces[1] - nullifier * Math.Sign(forces[1])) : 0;
		float curXpos = transform.eulerAngles.z;//thats right
		float curZpos = -transform.eulerAngles.x;
		//print(getRotation(5, 0));
		transform.Rotate(new Vector3(forces[1], 0, -forces[0]) * Time.deltaTime * (1 / RotateFactor), Space.World);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, 1)), nullifier /120);

	}

	float getRotation(float force,float curPos) {
		float rotate;
		print(force.ToString() + " " + curPos.ToString());

		if (curPos < 360 && curPos > 360-LoseRotation) {
			float maxRotate = force * Time.deltaTime * (1 / RotateFactor);
			rotate = maxRotate < 360 - curPos ? maxRotate : 360 - curPos;
		} else {
			float maxRotate = force * Time.deltaTime * (1 / RotateFactor);
			rotate = maxRotate > curPos ? maxRotate : -curPos;
		}
		return rotate;
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
