using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public abstract void Intro(float posX, float posZ);

	private bool dragging;
	private float distance;
	private bool isHitting=false;
	public float Mass { get { return isHitting ? GetComponent<Rigidbody>().mass : 0; } } //makes sure it doesnt count without touching the raft

	void OnTriggerEnter(Collider water) {
		Destroy(gameObject,0.1f);
	}

	void OnCollisionEnter(Collision hit) {
		isHitting = true;
		print(isHitting);
		print(Mass);
	}
	void OnCollisionExit(Collision hit) {
		isHitting = false;
	}

	void OnMouseUp() {
		dragging = false;
	}

	void Update() {
		if (dragging) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
			transform.position = rayPoint;
		}
	}

}



