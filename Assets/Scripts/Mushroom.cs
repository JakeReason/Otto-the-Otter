//--------------------------------------------------------------------------------
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
		m_animator = GetComponent<Animator>();

		if (!m_animator)
		{
			Debug.Log("No Animator on Mushroom!");
		}

		m_animator.SetBool("Bounce", false);

		m_playerScript = m_player.GetComponent<Player>();

		if (!m_playerScript)
		{
			Debug.Log("No Player Script!");
		}

		m_audioSource = GetComponent<AudioSource>();

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
		if (other.CompareTag("Player"))
		{
			m_audioSource.PlayOneShot(m_audio);

			m_animator.SetBool("Bounce", true);

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
		if (other.CompareTag("Player"))
		{
			m_animator.SetBool("Bounce", false);
		}
	}
}
