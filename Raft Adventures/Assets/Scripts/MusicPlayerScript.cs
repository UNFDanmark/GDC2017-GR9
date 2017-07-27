using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayerScript : MonoBehaviour {

	public AudioClip[] clips;
	private AudioSource AS;
	int currentClip = -1;
	public int nextClip = -1;
	float clipLength = 10;
	float StartTime = 0;
	bool dead = false;


	void Awake() {
		DontDestroyOnLoad(gameObject);
		AS = gameObject.GetComponent<AudioSource>();
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name.Equals("Main scene")) {
			if (currentClip != 0) {
				AS.Stop();
				AS.PlayOneShot(clips[0]);
				currentClip = 0;
				dead = false;
				nextClip = -1;
				StartTime = Time.time;
				clipLength = clips[currentClip].length;
			}
		} else if (scene.name.Equals("Main Menu")) {
			AS.PlayOneShot(clips[0]);
			currentClip = 0;
			StartTime = Time.time;
			clipLength = clips[currentClip].length;

		}
	}

	void Update() {
		if (Time.time-StartTime > clipLength-0.3) {
			if (nextClip == -2) return;
			if (nextClip ==-1) {
				AS.PlayOneShot(clips[currentClip]);
			}else {
				AS.PlayOneShot(clips[nextClip]);
				currentClip = nextClip;
			}
			clipLength = clips[currentClip].length;
			nextClip = -1;
			StartTime = Time.time;
		}
	}

	public void PlayNR(int NR) {
		if (dead) return;
		nextClip = NR;
	}
	public void IPlayNR(int NR) {
		dead = true;
		AS.Stop();
		AS.PlayOneShot(clips[NR]);
		currentClip = NR;
		nextClip = -2;
		StartTime = Time.time;
		clipLength = clips[currentClip].length;
		
	}
}
