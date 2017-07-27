﻿using UnityEngine;
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


    void FixedUpdate()
    {
        Move();
    }
	void Move() {
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, thisRigidbody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
		thisRigidbody.velocity = movement;
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed), 0.15f);
		}
	}
}


