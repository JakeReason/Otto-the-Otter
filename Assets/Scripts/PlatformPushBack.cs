//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPushBack : MonoBehaviour {

	// Collectable manager GameObject used to get access to the collectable manager.
	public GameObject m_platformObject;

	// Collectable manager Script used to get access to the collectable manager script.
	private Platforms m_platformsScript;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	private void Awake()
	{
		// Gets reference to the collectable manager script.
		m_platformsScript = m_platformObject.GetComponent<Platforms>();
	}

	//--------------------------------------------------------------------------------
	// OnTriggerStay checks if the player is colliding with this object and pushes them
	// back from the object.
	//
	// Param:
	//		other: used to find the other colliding object.
	//
	//--------------------------------------------------------------------------------
	private void OnTriggerStay(Collider other)
	{
		// Checks if the player is colliding with this object.
		if (other.CompareTag("Player"))
		{
			// Pushes the player back.
			m_platformsScript.PushBack(other);
		}
	}

}
