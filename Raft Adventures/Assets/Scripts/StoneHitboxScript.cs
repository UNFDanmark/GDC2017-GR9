using UnityEngine;
using System.Collections;
using System;

public class StoneHitboxScript : AbstractEnemy {
    // Update is called once per frame
    public override void Intro(float posX,float posZ){
        transform.position=new Vector3(posX, 20, posZ);
    }
}
