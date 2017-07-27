using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public abstract void Intro(float posX, float posZ);
	protected abstract void Score();
	GameObject Shadow;
	private bool isShadow = false;
	public float Mass {
		get {
			if (!isShadow && Physics.Raycast(transform.position, new Vector3(0, -1, 0), transform.lossyScale.y / 2 + 1f, 1 << LayerMask.NameToLayer("Enemies") | 1 << LayerMask.NameToLayer("Raft"))) {
				return GetComponent<Rigidbody>().mass;
			} else {
				return 0;
			}
		}
	}//makes sure it doesnt count without touching the raft

	void OnTriggerEnter(Collider water) {
		//print("hi");
		Score();
		Destroy(gameObject,0.1f);
		
	}
	public void castShadowDown() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 20, 1 << LayerMask.NameToLayer("Raft")| 1 << LayerMask.NameToLayer("Water"))) {
			Shadow = Instantiate(gameObject);
			Shadow.transform.position = new Vector3(transform.position.x, transform.lossyScale.y / 2, transform.position.z);
			Shadow.layer = LayerMask.NameToLayer("Shadow");
			MeshRenderer CMR = Shadow.transform.GetChild(0).GetComponent<MeshRenderer>();
			CMR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly; //removes shadows	
			Destroy(Shadow,Mathf.Sqrt(2*transform.position.y/-Physics.gravity.y));
		}
	}
	
}



