using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Camera cam;
	private Color MouseOverColor = Color.blue;
	private Color OriginalColor = Color.black;
	private GameObject selectedGameObject;
	Rigidbody thisRigidbody;
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
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			selectedGameObject = null;
		}
		if (Input.GetMouseButton(0) && selectedGameObject != null) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 300f, 1 << LayerMask.NameToLayer("Enemy Height"))) {
				selectedGameObject.transform.position = hit.point;

			}
		}
	}
}

/*
 Vector3 getPointVector( Vector3 camPoint, Vector3 hitPoint,float height) {
		return ((height- camPoint.y) / transform.position.y) * transform.position + camPoint;
	Vector3 vecline = (selectedGameObject.transform.position - transform.position);
	selectedGameObject.transform.position = getPointVector(vecline,hit.point, selectedGameObject.transform.lossyScale.y);
*/