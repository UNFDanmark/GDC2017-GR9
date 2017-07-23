using UnityEngine;
using System.Collections;
using System;

public class StoneHitboxScript : AbstractEnemy {
    // Use this for initialization
    public void Start () {
	    
	}

    // Update is called once per frame
    public void Update () {
	
	}

    public override void Intro(float posX,float posZ){
        transform.Translate(posX, 20, posZ);
    }
}
