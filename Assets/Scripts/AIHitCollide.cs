using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitCollide : MonoBehaviour {

	public GameObject m_enemy;
	private BasicEnemy m_enemyScript;

	// Use this for initialization
	void Start () {
		m_enemyScript = m_enemy.GetComponent<BasicEnemy>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			--m_enemyScript.m_fHealth;

		}
	}
}
