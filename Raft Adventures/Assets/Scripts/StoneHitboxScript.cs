using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneHitboxScript : AbstractEnemy {
	public GameObject score;
	public AudioClip hit;
    // Update is called once per frame
    public override void Intro(float posX,float posZ){
        transform.position=new Vector3(posX, 20, posZ);
		transform.Rotate(0 ,0 , Random.Range(0, 360));
		castShadowDown();
		StartCoroutine(DelayedSFX());

    }

	IEnumerator DelayedSFX() {
		yield return new WaitForSeconds(Mathf.Sqrt(2 * transform.position.y / -Physics.gravity.y));
		GetComponent<AudioSource>().PlayOneShot(hit);
	}
	protected override void Score() {
		score.GetComponent<ScoreSystemScript>().IncreaseScore((int)GetComponent<Rigidbody>().mass * 50);
		GetComponent<AudioSource>().Play();
	}
}
