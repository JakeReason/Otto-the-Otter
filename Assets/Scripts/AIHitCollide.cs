//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas. Edited by Matt Le Nepveu.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitCollide : MonoBehaviour
{
	// Used to access the enemy GameObject.
	public GameObject m_enemy;

	// Used to get access to the enemey model GameObject.
	public GameObject m_enemyModel;

	// Used to change the colour of the enemy.
	public float m_fHitTime;

	// The flashing rate of the enemy when they have been hit.
	public float m_fFlashingRate;

	// The force applied to the player when they jump on the enemy.
	public float m_fBounceForce = 3.0f;

	// Used to get access to the enemy script.
	private BasicEnemy m_enemyScript;

	// A bool used to determin if the enemy has been hit.
	bool m_bHit;

	// Stores the original colour of the enemy.
	Color m_originalColour;

	// Timer used to flash the colour of the enemy when they are hit.
	float m_timer;

	// Animator used to animate the enemy being hit.
	Animator m_animator;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets the Animator component.
		m_animator = m_enemy.GetComponent<Animator>();
		// Gets the basic enemy script.
		m_enemyScript = m_enemy.GetComponent<BasicEnemy>();
		// Stores the original colour of the object so it can be set back later. 
		m_originalColour = m_enemyModel.GetComponent<SkinnedMeshRenderer>().material.color;
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. Checks if the enemy has been hit and changes
	// the colour of then to indicate that they have been.
	//--------------------------------------------------------------------------------
	private void Update()
	{
		// Checks if hit is true.
		if (m_bHit)
		{
			// Increases the timer.
			m_timer += Time.deltaTime;
			
			// Checks if the timer is above 1.3 seconds and sets the hit bool to false.
			if(m_timer > 1.3f)
				m_animator.SetBool("Hit", false);
			// Checks if the timer is less then or equal to the hit time.
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
				// Sets time to zero.
				m_timer = 0;
				// Sets hit bool to false.
				m_bHit = false;
			}
		}
	}

	//--------------------------------------------------------------------------------
	// OnTriggerEnter checks if the player collides with this object and sets the 
	// animator bools to true and false based on the situation and the hit bool.
	//
	// Param:
	//		other: used to find the other colliding object.
	//
	//--------------------------------------------------------------------------------
	private void OnTriggerEnter(Collider other)
	{
		// Checks if it colliddes with the player.
		if (other.CompareTag("Player"))
		{
			// If hit is false then it sets the animation conditions and hit to true.
			if(!m_bHit)
			{
				m_animator.SetBool("90 Spin", false);
				m_animator.SetBool("Idle", false);
				m_animator.SetBool("Walk", false);
				m_animator.SetBool("Run", false);
				m_animator.SetBool("Hit", true);
				m_bHit = true;
			}
		}
	}
}
