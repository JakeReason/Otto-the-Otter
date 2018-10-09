using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	public float m_fBounceForce = 3.0f;

	public LayerMask m_playerLayer;

	public GameObject m_player;
	private Player m_playerScript;

	// Use this for initialization
	void Awake()
	{
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
			//Debug.Log("BOUNCE!");
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
