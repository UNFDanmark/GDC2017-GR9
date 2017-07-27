using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public Button myButton;

	// Use this for initialization
	void Start() {
		myButton.onClick.AddListener(StartGame);
	}
	public void StartGame() {
		SceneManager.LoadScene("Main scene");
	}
}

