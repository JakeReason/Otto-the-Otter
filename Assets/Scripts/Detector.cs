using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Detector : MonoBehaviour
{
	public Transform m_player;
	public LayerMask m_treeLayer;
	public Image m_selectImage;

	private float m_fPrevDistance;
	private List<GameObject> m_hookables;
	private Transform m_target;


	// Use this for initialization
	void Awake()
	{
		m_fPrevDistance = 0.0f;

		m_hookables = new List<GameObject>();

		m_target = null;
	}

	void Update()
	{
		if (m_hookables.Count == 0)
		{
			m_target = null;
			m_selectImage.enabled = false;
		}
		else if (m_hookables.Count == 1)
		{
			if (!ObjectsBetween(m_hookables[0].transform))
			{
				m_target = m_hookables[0].transform;
				m_selectImage.enabled = true;
				Vector3 targetPos = Camera.main.WorldToScreenPoint(m_target.position);
				m_selectImage.transform.position = targetPos;
			}
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

	private bool ObjectsBetween(Transform desiredTarget)
	{
		Debug.DrawLine(m_player.position, desiredTarget.position);

		if (Physics.Linecast(m_player.position, desiredTarget.position, m_treeLayer))
		{
			return true;
		}
		else
		{
			return false;
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
				if (!ObjectsBetween(m_hookables[0].transform))
				{
				    m_target = m_hookables[i].transform;
				    m_selectImage.enabled = true;
				    Vector3 v3TargetPos = Camera.main.WorldToScreenPoint(m_target.position);
				    m_selectImage.transform.position = v3TargetPos;
				}
			}

			m_fPrevDistance = fDistance;
		}

		m_fPrevDistance = 0.0f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hookable"))
		{
			m_hookables.Add(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Hookable"))
		{
			m_hookables.Remove(other.gameObject);
		}
	}

	public Transform GetTarget()
	{
		return m_target;
	}

	public void ClearTarget(GameObject objToRemove)
	{
		m_hookables.Remove(objToRemove);
	}
}
