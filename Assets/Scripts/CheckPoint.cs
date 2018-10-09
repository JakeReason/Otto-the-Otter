using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	public Animator checkpointAnim;

	private AudioSource m_audioSource;

	private bool m_bTouched;

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
		if (m_audioSource.isPlaying)
		{
			GetComponent<Collider>().enabled = false;
		}
		else
		{
			GetComponent<Collider>().enabled = true;
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
			m_CM.SetCurrentCheckPoint(transform);
			checkpointAnim.SetBool("Checkpoint", true);
			if (!m_bTouched)
			{
				m_audioSource.Play();
			}
			m_bTouched = true;
		}
	}
}
