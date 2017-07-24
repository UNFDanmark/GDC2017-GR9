using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public abstract void Intro(float posX, float posZ);

	private bool dragging;
	private float distance;

	void OnTriggerEnter(Collider water) {
		Destroy(gameObject,0.1f);
	}
	void OnMouseDown() {
		distance = Vector3.Distance(transform.position, Camera.main.transform.position);
		dragging = true;
		print("hi");
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



