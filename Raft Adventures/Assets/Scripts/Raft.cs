using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

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
	public float[] musicChange;
	private bool active3 = false;
	public GameObject MusicPlayer;
	public GameObject Score;
	private bool dead;
	private float DeathTime;
	public float DeathDelay = 2;
    void Awake() {
        player = GameObject.Find("Character"); //gets acess to Character object
		factory = GameObject.Find("EnemyControl").GetComponent<EnemyControlScript>();
		MusicPlayer = GameObject.Find("Music Player");
		Score = GameObject.Find("Score System");
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			List<GameObject> allEnemies = factory.getAllEnemies();
			forces = calcStability(allEnemies);
			TotalWeight = calcLoad(allEnemies);
			UpdateRotation();
			UpdatePlaceInWater();
			checkLose();
			UpdateMusic();
		} else {
			die();
		}
	}

	void checkLose() {
		float xAsMinus = Mathf.Abs(transform.eulerAngles.x - 360) > transform.eulerAngles.x ? transform.eulerAngles.x : transform.eulerAngles.x - 360;
		float zAsMinus = Mathf.Abs(transform.eulerAngles.z - 360) > transform.eulerAngles.z ? transform.eulerAngles.z : transform.eulerAngles.z - 360;
		//print(transform.eulerAngles.x.ToString() + " " + transform.eulerAngles.z.ToString());
		if (Mathf.Abs(xAsMinus)>LoseRotation || Mathf.Abs(zAsMinus) > LoseRotation|| TotalWeight > weightLimit) {
			dead = true;
			factory.Stop();
			MusicPlayer.GetComponent<MusicPlayerScript>().IPlayNR(3);
			Score.GetComponent<ScoreSystemScript>().die();
			ScoreSystemScript.tipped = !(TotalWeight > weightLimit);
			DeathTime = Time.time;
			GetComponent<AudioSource>().Play();
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
		Vector3 moveToPoint = new Vector3(transform.position.x, -(TotalWeight / weightLimit) * transform.lossyScale.y/1.1f, transform.position.z);
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
	void UpdateMusic() {

		float xAsMinus = Mathf.Abs(transform.eulerAngles.x - 360) > transform.eulerAngles.x ? transform.eulerAngles.x : transform.eulerAngles.x - 360;
		float zAsMinus = Mathf.Abs(transform.eulerAngles.z - 360) > transform.eulerAngles.z ? transform.eulerAngles.z : transform.eulerAngles.z - 360;
		
		if (Mathf.Abs(xAsMinus) > musicChange[1] || Mathf.Abs(zAsMinus) > musicChange[1]) {
			MusicPlayer.GetComponent<MusicPlayerScript>().PlayNR(2);
		}else if ((Mathf.Abs(xAsMinus) > musicChange[0]|| Mathf.Abs(zAsMinus) > musicChange[0])) {
			MusicPlayer.GetComponent<MusicPlayerScript>().PlayNR(1);
		}
		
	}

	float calcLoad(List<GameObject> allEnemies) {
		float tempMass = 0;
		foreach(GameObject GO in allEnemies) {
			tempMass +=GO.GetComponent<AbstractEnemy>().Mass;
		}
		return tempMass;
	}
	void die() {
		transform.Translate(new Vector3(0, -2, 0) * Time.deltaTime);
		if(Time.time-DeathTime > DeathDelay) {
			SceneManager.LoadScene("Final Score");
		}
	}
}
