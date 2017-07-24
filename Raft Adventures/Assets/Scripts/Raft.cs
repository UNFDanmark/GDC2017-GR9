using UnityEngine;
using System.Collections;

public class Raft : MonoBehaviour {
    //public GameObject StoneHitboxPrefab;
    private CharacterScript playerScript;
	private EnemyFactoryScript factory;

    void Awake() {
        playerScript = GameObject.Find("Character").GetComponent<CharacterScript>(); //gets acess to Character behavior and methods
		factory = GameObject.Find("Character").GetComponent<EnemyFactoryScript>();

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
