//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPLayer : MonoBehaviour
{
	// Player GameObject used to access the player.
	private GameObject player;

	// PlayerTransform used to access the player transform.
	private Transform playerTrasform;

	// Use this for initialization
	void Start()
	{
		// Gets reference to the player gameObject.
		player = GameObject.FindGameObjectWithTag("Player");
		// Gets reference to the players transform.
		playerTrasform = player.transform;
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. Makes the object look at the player.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Makes the object look at the players transfrom.
		transform.LookAt(playerTrasform);
	}
}
