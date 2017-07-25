using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public abstract void Intro(float posX, float posZ);
	public GameObject ShadowMaker;
	public float Mass {
		get {
			if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), transform.lossyScale.y / 2 + 1f, 1 << LayerMask.NameToLayer("Enemies") | 1 << LayerMask.NameToLayer("Raft"))) {
				return GetComponent<Rigidbody>().mass;
			} else {
				return 0;
			}
		}
	}//makes sure it doesnt count without touching the raft

	void OnTriggerEnter(Collider water) {
		Destroy(gameObject,0.1f);
	}

	void Start() { //write code to initialize an copy of this object on the raft, and set mesh rendere shadows to only shadows via recursion: http://answers.unity3d.com/questions/205391/how-to-get-list-of-child-game-objects.html
		RaycastHit hit;
		if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 20, 1 << LayerMask.NameToLayer("Default"))) {

			//should be changed to object radius

		}
	}

	void Update() {

	}
}



