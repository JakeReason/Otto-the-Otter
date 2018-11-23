//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// An int to indicate what flower it is being collected.
	public int m_nFlowerToCollect;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	// Audio scource used to play a sound when picked up.
	private AudioSource m_audioSource;

	// A bool used to check if the collectable has been picked up.
	private bool m_bPickedUp;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake ()
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
			else if (m_audioSource.isPlaying && m_bPickedUp)
			{
				// Sets the mesh renderer and collider to false if audio is being
				// played and picked up is true.
				GetComponent<SkinnedMeshRenderer>().enabled = false;
				GetComponent<Collider>().enabled = false;
				// Sets the children to false.
				transform.GetChild(0).gameObject.SetActive(false);
			}

		}
	}
	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and adds
	// the flower to the collectable manager and sets itself not to be active.
	//
	// Param:
	//		other: used to find the other colliding object.
	//
	//--------------------------------------------------------------------------------
	private void OnTriggerEnter(Collider other)
	{
		// Checks if the player picks/runs into the collectable.
		if(other.CompareTag("Player"))
		{
			// Adds a flower to the collectable manager.
			m_CM.AddFlower(m_nFlowerToCollect);
			// Plays audio.
			m_audioSource.Play();
			// Sets m_bPickedUp to true.
			m_bPickedUp = true;
		}
	}
}
