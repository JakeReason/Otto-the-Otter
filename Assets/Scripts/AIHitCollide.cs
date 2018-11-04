using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitCollide : MonoBehaviour
{

	public GameObject m_enemy;
	public GameObject m_enemyModel;
	public float m_fHitTime;
	public float m_fFlashingRate;
	private BasicEnemy m_enemyScript;
	bool m_bHit;
	Color m_originalColour;
	float m_timer;

	// Use this for initialization
	void Awake()
	{
		m_enemyScript = m_enemy.GetComponent<BasicEnemy>();
		m_originalColour = m_enemyModel.GetComponent<SkinnedMeshRenderer>().material.color;
	}
	private void Update()
	{
		if (m_bHit)
		{
			m_timer += Time.deltaTime;
			if (m_timer <= m_fHitTime)
			{
				// Detects if the sine wave value is less than 0
				if (Mathf.Sin(m_timer / m_fFlashingRate) >= 0)
				{
					// Sets Otto's material colour to equal that of the flash colour
					m_enemyModel.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
				}
				// Otherwise the player's colour is reverted to his initial colour
				else
				{
					m_enemyModel.GetComponent<SkinnedMeshRenderer>().material.color = m_originalColour;
				}
			}
			// Otherwise the player's colour is reverted to his initial colour
			else
			{
				m_enemyModel.GetComponent<SkinnedMeshRenderer>().material.color = m_originalColour;
				m_timer = 0;
				m_bHit = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_enemyScript.TakeDamage();
			m_bHit = true;
		}
	}
}
