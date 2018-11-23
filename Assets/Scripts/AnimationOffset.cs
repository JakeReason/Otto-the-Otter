//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffset : MonoBehaviour {

	private Animator anim;
	private float randomIdleStart;
	public float minSpeed = .1f;
	public float maxSpeed = 1f;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		anim.GetComponent<Animator> ().speed = Random.Range (minSpeed, maxSpeed);

		randomIdleStart = Random.Range (0, anim.GetCurrentAnimatorStateInfo (0).length); //Set a random part of the animation to start from

		anim.Play ("Jump", 0, randomIdleStart);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
