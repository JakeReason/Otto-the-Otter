using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	public float m_fBounceForce = 3.0f;

	public LayerMask m_nPlayerLayer;

	private GameObject m_player;
	private Player m_playerScript;

	// Use this for initialization
	void Awake()
	{
		m_player = GameObject.FindGameObjectWithTag("Player");

		if (!m_player)
		{
			Debug.Log("No Player!");
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
}
