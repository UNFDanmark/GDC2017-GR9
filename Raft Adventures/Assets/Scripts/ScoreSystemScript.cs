using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScoreSystemScript : MonoBehaviour {

	public static int score = 0;
	private static Text ScoreText;
	// Use this for initialization
	void Start() {
		score = 0;
		DontDestroyOnLoad(gameObject);
		if (GameObject.FindGameObjectsWithTag("Score System").Length > 1) {
			Destroy(gameObject);
		}
		ScoreText = GameObject.Find("Text").GetComponent<Text>();
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		ScoreText = GameObject.Find("Text").GetComponent<Text>();
		print(ScoreText.text);
	}

	public void IncreaseScore(int TBI) {

		score += TBI;
		ScoreText.text = "Score: " + score;
		print(ScoreText.text);
	}

}
