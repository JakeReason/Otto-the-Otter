using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detector : MonoBehaviour
{
	private float m_fPrevDistance;
	private bool m_bInRange;
	private List<GameObject> m_hookables;
	private Transform m_target;
	private Transform m_player;

	// Use this for initialization
	void Awake()
	{
		m_fPrevDistance = 0.0f;

		m_bInRange = false;

		m_hookables = new List<GameObject>();

		m_target = null;

		m_player = transform.parent.parent.parent;
	}

	void Update()
	{
		if (m_hookables.Count == 0)
		{
			m_target = null;
		}
		else if (m_hookables.Count == 1)
		{
			m_target = m_hookables[0].transform;
		}
		else if (m_hookables.Count > 1)
		{
			DistanceCheck();
		}
		else
		{
			Debug.Log("HOOKABLES LIST HAS INVALID COUNT!");
		}
	}

	private void DistanceCheck()
	{
		for (int i = 0; i < m_hookables.Count; ++i)
		{
			float fDistance = Vector3.Distance(m_player.position, 
											   m_hookables[i].transform.position);

			if (fDistance <= m_fPrevDistance)
			{
				m_target = m_hookables[i].transform;
			}

			m_fPrevDistance = fDistance;
		}

		m_fPrevDistance = 0.0f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hookable"))
		{
			m_bInRange = true;

			m_hookables.Add(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Hookable"))
		{
			m_hookables.Remove(other.gameObject);

			if (m_hookables.Count <= 0)
			{
				m_bInRange = false;
			}
		}
	}

	public bool GetInRange()
	{
		return m_bInRange;
	}

	public Transform GetTarget()
	{
		return m_target;
	}
}
