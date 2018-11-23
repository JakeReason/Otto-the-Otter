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

	// Audio scource used to play a sound when picked up.
	private AudioSource m_audioSource;

	// A bool used to check if the collectable has been picked up.
	private bool m_bPickedUp;

	// Particle Object used to get access to a particle system.
	public GameObject m_particleObject;

	// Fish biscuit particle system.
	public ParticleSystem m_particleSystem;

	// Rotation speed.
    public int speed = 1;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets reference to the player gameObject.
		m_player = GameObject.FindGameObjectWithTag("Player");
		// Gets reference to the player script.
		m_playerScript = m_player.GetComponent<Player>();
		// Gets reference to the audio source attached to the gameobject.
		m_audioSource = GetComponent<AudioSource>();
		// Gets reference to the particle system.
		m_particleSystem = m_particleObject.GetComponent<ParticleSystem>();
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. It checks if the audio is playing and if 
	// picked up is true then turns the gameobject off. Also rotates the object.
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
			else if(m_audioSource.isPlaying && m_bPickedUp)
			{
				// Sets the mesh renderer and collider to false if audio is being
				// played and picked up is true.
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<Collider>().enabled = false;
			}
			// Rotates the object.
            transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);

		}
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
		if (other.CompareTag("Player"))
		{
			// Restores health to the player.
			m_playerScript.RestoreHealth();
			// Plays audio.
			m_audioSource.Play();
			// Plays the particle.
			m_particleSystem.Play();
			// Sets m_bPickedUp to true.
			m_bPickedUp = true;
		}
	}
}
