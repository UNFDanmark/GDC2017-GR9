using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public abstract void Intro(float posX, float posZ);

	private bool isHitting=false;
	public float Mass {
		get {
			if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), transform.lossyScale.y / 2 + 1f, 1 << LayerMask.NameToLayer("Enemies") | 1 << LayerMask.NameToLayer("Default"))) {
				return GetComponent<Rigidbody>().mass;
			} else {
				return 0;
			}
		}
	}//makes sure it doesnt count without touching the raft

	void OnTriggerEnter(Collider water) {
		Destroy(gameObject,0.1f);
	}

	void OnCollisionEnter(Collision hit) {
		isHitting = true;
	}
	void OnCollisionExit(Collision hit) {
		isHitting = false;
	}
}



