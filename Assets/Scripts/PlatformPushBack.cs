using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPushBack : MonoBehaviour {

	// Collectable manager GameObject used to get access to the collectable manager.
	public GameObject m_platformObject;

	// Collectable manager Script used to get access to the collectable manager script.
	private Platforms m_platformsScript;

	private void Awake()
	{
		// Gets reference to the collectable manager script.
		m_platformsScript = m_platformObject.GetComponent<Platforms>();
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_platformsScript.PushBack(other);
		}
	}

}
