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

	public AudioClip m_flowerNearClip;
	public AudioClip m_flowerPickUpClip;

	public int m_nFlowerToCollect;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	private AudioSource m_audioSource;

	private bool m_bPickedUp;

	private GameObject m_player;

	

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		// Gets reference to the collectable manager gameObject.
		m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");
		// Gets reference to the collectable manager script.
		m_CM = m_collectableManager.GetComponent<CollectableManager>();

		m_audioSource = GetComponent<AudioSource>();

		m_player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (m_audioSource)
		{
			if ((transform.position - m_player.transform.position).sqrMagnitude < 25.0f && !m_bPickedUp)
			{
				if(!m_audioSource.isPlaying)
					m_audioSource.PlayOneShot(m_flowerNearClip);
			}
			if (!m_audioSource.isPlaying && m_bPickedUp)
			{
				gameObject.SetActive(false);
			}

		}
	}
	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and adds
	// a family member to the collectable manager and sets itself not to be active.
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
			// Adds the family member to the collectable manager.
			m_audioSource.Stop();
			m_CM.AddFlower(m_nFlowerToCollect);
			m_audioSource.PlayOneShot(m_flowerPickUpClip);
			gameObject.SetActive(false);
		}
	}
}
