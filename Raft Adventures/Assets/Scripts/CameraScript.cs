using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Camera cam;
	private Color MouseOverColor = Color.blue;
	private Color OriginalColor = Color.black;
	private GameObject selectedGameObject;
	float distance;

	// Use this for initialization
	void Start() {
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,300f,1<<LayerMask.NameToLayer("Enemies"))) {
				selectedGameObject = hit.transform.gameObject;
				distance = Vector3.Distance(hit.transform.position, transform.position);
			}
		}
		if (Input.GetMouseButton(0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			//selectedGameObject.transform.position = ray.GetPoint(distance);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				selectedGameObject.transform.position = new Vector3(hit.point.x, 2, hit.point.z);
			}
		}
	}
}
