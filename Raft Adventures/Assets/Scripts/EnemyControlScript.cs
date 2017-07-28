using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControlScript : MonoBehaviour {

	private float WaveCD = 1;
	private float LastWave = 0;
	public float WaveFactor = 0.1f;
	private float EnemyCD = 2;
	private float LastEnemy = 0;
	public float EnemyFactor = 0.01f;
	private float maxWeight = 1.5f;
	public float WeightFactor = 0.75f;
	public Raft raft;
	private bool stopped = false;
	public GameObject[] stones = new GameObject[5];

	public float[] StoneConstants = {100,10,0.5f,0.02f,0.005f};
	public float[] stoneMin = { 1000, 650, 500, 250, 125 };

	private float[] PrimeMultiplier = { 1.5f,1.3f,1.15f,1,1};
	private float[] StoneChance = new float[5];
	List<GameObject> allEnemies = new List<GameObject>();

	private float[][] fields = new float[][]{ 
		//new float[]{-4.5f, 4.5f ,-4.5f,-1.5f}, 
		//new float[]{-4.5f, 4.5f, 1.5f, 4.5f }, 
		//new float[]{-4.5f, -1.5f, -4.5f, 4.5f }, 
		//new float[]{-4.5f, -1.5f, -4.5f, 4.5f },
		new float[]{-4.5f, 2.5f ,-4.5f, 2.5f}, 
		new float[]{2.5f, 4.5f ,-4.5f, 2.5f}, 
		new float[]{-4.5f, 2.5f ,2.5f, 4.5f}, 
		new float[]{2.5f, 4.5f ,2.5f, 4.5f} 
	};

	void Start() {
		updateValues();
	}
	// Update is called once per frame
	 void Update () {
		if (stopped) return;
		updateValues();
		sendWave();
		sendEnemy();
	}
	
	private bool sendWave() {
		//print("WaveCD: " + WaveCD);
		//print("Time.timeSinceLevelLoad - LastWave :" + (Time.timeSinceLevelLoad - LastWave));
		if (WaveCD > Time.timeSinceLevelLoad - LastWave) return false;
		LastWave = Time.timeSinceLevelLoad;
		int tile = Random.Range((int)0, (int)4);
		float[] bounds = fields[tile];
		int biggestEnemyType = getEnemyType();
		float CurWeight = 0;
		int i = biggestEnemyType;
		maxWeight = Mathf.Min(maxWeight, raft.weightLimit*1f/2f);
		while(CurWeight+1.1 < maxWeight) {
			float stoneWeight = stones[i].GetComponent<Rigidbody>().mass;
			while (CurWeight+stoneWeight<maxWeight) {
				getEnemy(stones[i],Random.Range(bounds[0],bounds[1]), Random.Range(bounds[2], bounds[3]));
				CurWeight += stoneWeight*PrimeMultiplier[biggestEnemyType];
			}

			i--;
		}
		return true;
	}

	private bool sendEnemy() {
		if (EnemyCD > Time.timeSinceLevelLoad - LastEnemy) return false;
		GameObject tempEnemy = stones[getEnemyType()];
		getEnemy(tempEnemy, Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f));
		LastEnemy = Time.timeSinceLevelLoad;
		return true;
	}

	void updateValues() {
		for(int i=0;i<StoneChance.Length;i++) {
			StoneChance[i] = Mathf.Min(StoneConstants[i]*Mathf.Pow(Time.timeSinceLevelLoad,i+1),stoneMin[i]);
		}
		EnemyCD = 8 - (10 / ((Time.timeSinceLevelLoad + 0.35f) + 0.6f * (Time.timeSinceLevelLoad + 0.35f) * Mathf.Cos((Time.timeSinceLevelLoad + 0.35f) / 2))) - 4 * Mathf.Log((Time.timeSinceLevelLoad + 0.35f), 10);
		EnemyCD = Mathf.Max(EnemyCD, 0.3f);
		WaveCD = EnemyCD * 10;
		

		maxWeight = 1.5f + Time.timeSinceLevelLoad * WeightFactor;
	}

	public GameObject getEnemy(GameObject TBI,float posX,float posZ) {
		//enemy instantiation and selection logic
		GameObject thisEnemy = Instantiate(TBI);
		thisEnemy.GetComponent<AbstractEnemy>().Intro(posX, posZ); //activates the enemies entrance
		allEnemies.Add(thisEnemy);
		return thisEnemy;

	}
	int getEnemyType() {
		float totalChance = 0;
		foreach(float SC in StoneChance) {
			totalChance += SC;
		}
		float number = Random.Range(0, totalChance);
		for(int i = 0; i < StoneChance.Length;i++) {
			number -= StoneChance[i];
			if(number < 0) {
				return i;
			}
		}
		return 0;
	}
	public void UpdateList() {
		allEnemies.RemoveAll(delegate (GameObject o) { return o == null; });//removes all null objects
	}
	public List<GameObject> getAllEnemies() {
		UpdateList();
		return allEnemies;
	}
	public void Stop() {
		stopped = true;
	}
}



