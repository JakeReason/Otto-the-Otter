//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollectable : MonoBehaviour
{
	[SerializeField]
	// The amount of sticks to add when collected.
	private int m_nAddStickAmount;

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets reference to the collectable manager gameObject.
		m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");
		// Gets reference to the collectable manager script.
		m_CM = m_collectableManager.GetComponent<CollectableManager>();
	}

	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and adds an 
	// amount of sticks to the collectable manager and sets itself not to be active.
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
			// Adds the stick to the collectable manager.
			m_CM.AddSticks(m_nAddStickAmount);
			gameObject.SetActive(false);
		}
	}
}
