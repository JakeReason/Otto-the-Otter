//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLife : MonoBehaviour {

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

	// Audio scource used to play a sound when picked up.
	private AudioSource m_audioSource;

	// A bool used to check if the collectable has been picked up.
	private bool m_bPickedUp;

	// Used to rotate the object.
    public int speed = 1;
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
	// picked up is true and turns the gameobject, meshrenderer, and collider on and 
	// off.
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
				// Sets the object to false if the audio is not playing and if it has
				// been picked up.
				gameObject.SetActive(false);
			}
			else if (m_audioSource.isPlaying && m_bPickedUp)
			{
				// Sets the mesh renderer and collider to false if audio is being
				// played and picked up is true.
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<Collider>().enabled = false;
			}
		}
		// Rotates the object.
        transform.Rotate(Vector3.down * speed * Time.deltaTime, Space.World);
    }

	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and adds a life
	// to the collectable manager, sets itself not to be active, and plays audio when
	// picked up.
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
			// Adds a life to the collectable manager.
			m_CM.AddLife();
			// Plays audio.
			m_audioSource.Play();
			// Sets m_bPickedUp to true.
			m_bPickedUp = true;
		}
	}
}
