using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

	public Animator anim;
	public GameObject path;



	// Use this for initialization
	void Start ()
	{
		anim.enabled = false;
		path.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter (Collider other)
	{
		path.SetActive (true);
		anim.enabled = true;


	}

}
