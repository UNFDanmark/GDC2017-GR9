using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{

    //start of changeble values from unity. Remember to reset
    public float moveSpeed = 2;
    private Rigidbody thisRigidbody;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }
	// Update is called once per frame
	void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        thisRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);//-"vertical" bc goes wrong way
    }
}

