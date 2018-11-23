//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	// Animator for the checkpoints used to play an animation when reached.
	public Animator checkpointAnim;

	// Audio scource used to play a sound when checkpoint is reached.
	private AudioSource m_audioSource;

	// A bool used to check if the checkpoint has been touched before.
	private bool m_bTouched;

	// Particle system which play confetti when reached.
    public ParticleSystem confetti;

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
	// Update is called once every frame. It checks if the audio is playing and turns
	// the collider on and off.
	//--------------------------------------------------------------------------------
	private void Update()
	{
		// Checks if audio is playing from the audio scource.
		if (m_audioSource.isPlaying)
		{
			// Turns the collider off if audio is playing to prevent the audio 
			// triggering multiple times.
			GetComponent<Collider>().enabled = false;
		}
		else
		{
			// Turns the collider back on when the audio stops.
			GetComponent<Collider>().enabled = true;
		}

	}
	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and sets the 
	// current checkpoint in the collectable manager and makes sure the animation and 
	// sounds dont play again just changing the checkpoint to keep track of the last 
	// touched checkpoint.
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
			// Sets the current checkpoint in the collectable manager.
			m_CM.SetCurrentCheckPoint(transform);
			// Sets the animator condition to true so the animation plays.
			checkpointAnim.SetBool("Checkpoint", true);
			// Checks if it has been touched before otherwise it continues.
			if (!m_bTouched)
			{
				// If it has not then it plays the animation and sounds.
				m_audioSource.Play();
                confetti.Play();
            }
			// Sets touched to true.
            m_bTouched = true;
		}
	}
}
