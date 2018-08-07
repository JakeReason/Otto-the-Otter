using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBiscuit : MonoBehaviour
{
	private GameObject m_player;
	private Player m_playerScript;

	// Use this for initialization
	void Awake()
	{
		// Gets reference to the player gameObject.
		m_player = GameObject.FindGameObjectWithTag("Player");
		// Gets reference to the player script.
		m_playerScript = m_player.GetComponent<Player>();
	}

	private void OnTriggerEnter(Collider other)
	{
		// Checks if the player picks/runs into the collectable.
		if (other.tag == "Player")
		{
			// Restores health to the player.
			m_playerScript.RestoreHealth();
			gameObject.SetActive(false);
		}
	}
}
