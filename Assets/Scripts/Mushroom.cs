using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	public float m_fBounceForce = 3.0f;

	public LayerMask m_playerLayer;

	public GameObject m_player;

	private Player m_playerScript;

	private Animator m_animator;

	// Use this for initialization
	void Awake()
	{
		m_animator = GetComponent<Animator>();

		if (!m_animator)
		{
			Debug.Log("No Animator on Mushroom!");
		}

		m_playerScript = m_player.GetComponent<Player>();

		if (!m_playerScript)
		{
			Debug.Log("No Player Script!");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_playerScript.Bounce(m_fBounceForce);
		}
	}

	//private void OnTriggerStay(Collider other)
	//{
	//	if (other.CompareTag("Player"))
	//	{
	//		m_playerScript.Bounce(m_fBounceForce);
	//	}
	//}

	//private void OnCollisionEnter(Collision collision)
	//{
	//	if (collision.gameObject.CompareTag("Player"))
	//	{
	//		m_playerScript.Bounce(m_fBounceForce);
	//	}
	//}
}
