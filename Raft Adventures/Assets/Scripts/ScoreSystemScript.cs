using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScoreSystemScript : MonoBehaviour {

	public static int score = 0;
	private static Text ScoreText;
	public static bool dead = false;
	public static bool tipped;
	// Use this for initialization
	void Start() {
		score = 0;
		DontDestroyOnLoad(gameObject);
		if (GameObject.FindGameObjectsWithTag("Score System").Length > 1) {
			Destroy(gameObject);
		}
		ScoreText = GameObject.Find("Text").GetComponent<Text>();
		dead = false;
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name.Equals("Main scene")) {
			ScoreText = GameObject.Find("Text").GetComponent<Text>();
		}
		if (scene.name.Equals("Final Score")) {
			ScoreText = GameObject.Find("Text").GetComponent<Text>();
			ScoreText.text = "Final Score: " + score;
			Text reasonText = GameObject.Find("Lose Reason").GetComponent<Text>();
			if (tipped) {
				reasonText.text = "Your raft tipped";
			} else {
				reasonText.text = "Your raft couldn't take anymore weight";
			}
		}
	}

	public void IncreaseScore(int TBI) {
		if (dead) return;
		score += TBI;
		ScoreText.text = "Score: " + score;
		print(ScoreText.text);
	}
	public void die() {
		dead = true;
	}

}
