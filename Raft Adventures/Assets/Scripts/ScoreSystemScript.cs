using UnityEngine;
using System.Collections;

public class ScoreSystemScript : MonoBehaviour {

	private bool old = false;
	private int score = 0;
	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad(gameObject);
		if(old && GameObject.FindGameObjectsWithTag("Score System").Length > 1) {
			Destroy(gameObject);
		}
		old = true;
	
	}
	

	// Update is called once per frame
	public void IncreaseScore(int TBI) {
		print(score);
		score += TBI;
	}
	
}

