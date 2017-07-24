using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public abstract void Intro(float posX, float posZ);

	void OnTriggerEnter(Collider water) {
		Destroy(gameObject,0.1f);
	}

}

