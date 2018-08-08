//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBiscuit : MonoBehaviour
{
	// Player GameObject used to access the player.
	private GameObject m_player;

	// Player Script used to access the player script.
	private Player m_playerScript;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets reference to the player gameObject.
		m_player = GameObject.FindGameObjectWithTag("Player");
		// Gets reference to the player script.
		m_playerScript = m_player.GetComponent<Player>();
	}

	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks when the player collides with this object and restores
	// health to the player and sets itself not to be active.
	//
	// Param:
	//		other: used to find the other colliding object.
	//
	//--------------------------------------------------------------------------------
	private void OnTriggerEnter(Collider other)
	{
		// Checks if the player picks/runs into the collectable.
		if (other.tag == "Player")
		{
			// Restores health to the player.
			m_playerScript.RestoreHealth();
			gameObject.SetActive(false);
		}
	}
}
