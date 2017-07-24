using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Camera cam;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,300f,1<<LayerMask.NameToLayer("Enemies"))) {
				print(hit.transform.gameObject.GetType());
			}
		}
	}
	

}

