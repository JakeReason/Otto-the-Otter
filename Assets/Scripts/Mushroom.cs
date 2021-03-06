﻿//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu. Editted by: Jeremy Zoitas.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Mushroom script
public class Mushroom : MonoBehaviour
{
	// Represents the player's layermask
	public LayerMask m_playerLayer;

	// Stores the audioclip for when the player bounces on the mushroom
	public AudioClip m_audio;

	// Used to access the player script
	public GameObject m_player;

	// Float represents the amount of force applied to the player
	public float m_fBounceForce = 3.0f;

    // Used to access the variables in the player script
	private Player m_playerScript;

    // Accesses the animator component for the mushroom
	private Animator m_animator;

    // Indicates the mushroom classes' AudioSource
	private AudioSource m_audioSource;

    //--------------------------------------------------------------------------------
    // Function is used for initialization.
    //--------------------------------------------------------------------------------
    void Awake()
	{
        // Gets the animator component off of the Mushrooms
		m_animator = GetComponent<Animator>();

        // Logs an error message in debug if the animator could not be found
		if (!m_animator)
		{
			Debug.Log("No Animator on Mushroom!");
		}

        // Initially sets the bounce bool to be false
		m_animator.SetBool("Bounce", false);

        // Accesses the player script from the player game object itself
		m_playerScript = m_player.GetComponent<Player>();

        // Error message is logged in debug mode if the player script isn't accessed
		if (!m_playerScript)
		{
			Debug.Log("No Player Script!");
		}

        // Obtains the audio source component from the mushroom 
		m_audioSource = GetComponent<AudioSource>();

        // Logs an error message in debug mode if audio source couldn't be accessed
		if (!m_audioSource)
		{
			Debug.Log("No Audio Source!");
		}
	}

    //--------------------------------------------------------------------------------
    // Function is called when the player is enters a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
	{
        // Checks if the mushroom and player collide
		if (other.CompareTag("Player"))
		{
            // Plays the mushroom audio on collision
			m_audioSource.PlayOneShot(m_audio);

            // Bounce bool is set to true in animator
			m_animator.SetBool("Bounce", true);

            // Bounce function is called to make the player bounce
			m_playerScript.Bounce(m_fBounceForce, false);
		}
	}
//--------------------------------------------------------------------------------
    // Function is called when the player is exits a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    
    private void OnTriggerExit(Collider other)
	{
        // Sets bounce bool to false when player exits mushroom trigger
		if (other.CompareTag("Player"))
		{
			m_animator.SetBool("Bounce", false);
		}
	}
}
