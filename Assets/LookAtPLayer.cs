using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPLayer : MonoBehaviour {

	private GameObject player;
	public Transform playerTrasform;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTrasform = player.transform;
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt (playerTrasform);
		
	}
}
