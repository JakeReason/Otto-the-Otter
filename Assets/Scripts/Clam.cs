//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam : MonoBehaviour
{
	[SerializeField]
	// The amount of clams to add when collected.
	private int m_nClamAmount;

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	// Audio scource used to play a sound when picked up.
	private AudioSource m_audioSource;

	// A bool used to check if the collectable has been picked up.
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
		// Gets reference to the audio source attached to the gameobject.
		m_audioSource = GetComponent<AudioSource>();
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. It checks if the audio is playing and if 
	// picked up is true then turns the gameobject off.
	//--------------------------------------------------------------------------------
	private void Update()
	{
		// Checks if audio source exists.
		if (m_audioSource)
		{
			// Checks if audio is playing from the audio scource and if it has been 
			// picked up.
			if (!m_audioSource.isPlaying && m_bPickedUp)
			{
				// Sets gameobject to false.
				gameObject.SetActive(false);
			}

		}
	}

	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and adds an 
	// amount of clams to the collectable manager and sets itself not to be active.
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
			// Adds the clams to the collectable manager.
			m_CM.AddClams(m_nClamAmount);
			// Plays audio.
			m_audioSource.Play();
			// Sets m_bPickedUp to true.
			m_bPickedUp = true;
		}
	}
}
