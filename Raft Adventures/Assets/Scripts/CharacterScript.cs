using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{

    //start of changeble values from unity. Remember to reset
    public float moveSpeed = 2;
    private Rigidbody thisRigidbody;
	private Animator ModelAnimator;
	public float crossfadeTime = 0.1f;
	public GameObject cam;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
		ModelAnimator = GameObject.Find("Charactor").GetComponent<Animator>();
    }

    // Use this for initialization
	// Update is called once per frame

    void FixedUpdate()
    {
        Move();
    }
	void Move() {

		Vector3 UpDownVec = cam.transform.forward.normalized * Input.GetAxis("Vertical");
		Vector3 LeftRightVec = cam.transform.right.normalized * Input.GetAxis("Horizontal");
		Vector3 sumVec = (LeftRightVec + UpDownVec) * moveSpeed;
		sumVec.y = thisRigidbody.velocity.y;
		//Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, thisRigidbody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
		thisRigidbody.velocity = sumVec;
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed), 0.15f);
			ModelAnimator.CrossFade("Run", crossfadeTime);
		} else {
			ModelAnimator.CrossFade("Idle", crossfadeTime);
		}
	}
}


