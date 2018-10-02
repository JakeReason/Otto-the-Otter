//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam : MonoBehaviour
{
	[SerializeField]
	// The amount of sticks to add when collected.
	private int m_nClamAmount;

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	private AudioSource m_audioSource;

	private bool m_bPickedUp;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets reference to the collectable manager gameObject.
		m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");
		// Gets reference to the collectable manager script.
		m_CM = m_collectableManager.GetComponent<CollectableManager>();

		m_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if(m_audioSource)
		{
			if (!m_audioSource.isPlaying && m_bPickedUp)
			{
				gameObject.SetActive(false);
			}

		}
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
		if (other.CompareTag("Player"))
		{
			// Adds the stick to the collectable manager.
			m_CM.AddClams(m_nClamAmount);
			m_audioSource.Play();
			m_bPickedUp = true;
		}
	}
}
