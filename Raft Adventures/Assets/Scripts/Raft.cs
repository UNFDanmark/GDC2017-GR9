using UnityEngine;
using System.Collections;

public class Raft : MonoBehaviour {
    //public GameObject StoneHitboxPrefab;
    private CharacterScript playerScript;

    void Awake() {
        playerScript = GameObject.Find("Character").GetComponent<CharacterScript>(); //gets acess to Character behavior and methods

    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
