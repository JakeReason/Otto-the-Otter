using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	[Range(2.0f, 8.0f)]
	public float m_fBounceForce = 3.0f;

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
	
	// Update is called once per frame
	void Update()
	{
		if (Physics.Raycast(transform.position, Vector3.up, 1.0f))
		{
			m_playerScript.Bounce(m_fBounceForce);
		}
	}
}
