using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollectable : MonoBehaviour
{
	[SerializeField]
	private int m_nAddStickAmount;

	private GameObject m_collectableManager;
	private CollectableManager m_CM;

	// Use this for initialization
	void Awake()
	{
		// Gets reference to the collectable manager gameObject.
		m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");
		// Gets reference to the collectable manager script.
		m_CM = m_collectableManager.GetComponent<CollectableManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		// Checks if the player picks/runs into the collectable.
		if (other.tag == "Player")
		{
			// Adds the stick to the collectable manager.
			m_CM.AddSticks(m_nAddStickAmount);
			gameObject.SetActive(false);
		}
	}
}
