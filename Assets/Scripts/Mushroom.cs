using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	public LayerMask m_playerLayer;

	public AudioClip m_audio;

	public GameObject m_player;

	public float m_fBounceForce = 3.0f;

	private Player m_playerScript;

	private Animator m_animator;

	private AudioSource m_audioSource;

	// Use this for initialization
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
			//Debug.Log("No Audio Source!");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_audioSource.PlayOneShot(m_audio);

			m_animator.SetBool("Bounce", true);

			m_playerScript.Bounce(m_fBounceForce);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_animator.SetBool("Bounce", false);
		}
	}
}
