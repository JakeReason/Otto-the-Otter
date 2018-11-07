//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
	[SerializeField]
	// Enemies health
	public float m_fHealth = 1;

	[SerializeField]
	// Player GameObject used to access the player.
	private GameObject m_player;

	[SerializeField]
	// Player Transform used to access the player transform.
	private Transform m_playerTransform;

	[SerializeField]
	// An array of transform used to create a patrol route.
	private Transform[] m_targetPoints;

	[SerializeField]
	// Keeps track of the current waypoint number.
	private int m_nDestPoint = 0;

	[SerializeField]
	// How long the enemy waits till moving to the next waypoint.
	private float m_fCooldown = 1;

	[SerializeField]
	// Distance to seek the player.
	private float m_fDistance = 5;

	[SerializeField]
	// Distance to attack the player
	private float m_fAttackDistance = 1;

	[SerializeField]
	// Speed of rotation.
	private float m_fRotateSpeed = 1;

	[SerializeField]
	// Speed of rotation.
	private float m_fWaitRotateSpeed = 1;

	[SerializeField]
	// The cooldown on the attack.
	private float m_fAttackCooldown = 1;

	[SerializeField]
	// Used to keep track of whether the enemy of going backwards. 
	private bool m_bGoBackWards = false;

	// Used to gain access to the NavMeshAgent.
	private NavMeshAgent m_agent;

	// Used to store the originalCooldown.
	private float m_fOriginalCooldown;

	// Used to store the originalAttackCooldown.
	private float m_fOriginalAttackCooldown;

	// Stores the distance from the player.
	private float m_fDistanceFromPlayer;

	// Player Script used to access the player script.
	private Player m_playerScript;

	[SerializeField]
	private AudioClip m_enemyDeathAudioClip;
	[SerializeField]
	private AudioClip m_enemyHitAudioClip;

	private AudioSource m_audioSource;

	public GameObject m_wolfModel;

	public GameObject m_detector;

	private Detector m_detectorScript;

	private bool m_bAudioPlayed;

	public GameObject m_hookableObj;

	public GameObject m_clamStack;

	public float m_fChaseSpeed;

    public GameObject m_enemyHitBox;

	private float m_fOriginalSpeed;

	private Animator m_animator;

	private bool m_bDead;

	private bool m_bDeathEnd;

	private bool m_bStunned;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		m_animator = GetComponent<Animator>();
		// Gets the NavMeshAgent on the gameobject.
		m_agent = GetComponent<NavMeshAgent>();
		// Stores the original cooldown time.
		m_fOriginalCooldown = m_fCooldown;
		// Sets the first point to go to.
		GoToNextPoint();
		// Stores the attack cooldwon time.
		m_fOriginalAttackCooldown = m_fAttackCooldown;
		// Sets the attack cooldown to 0.
		m_fAttackCooldown = 0;
		// Sets the playerScript reference up.
		m_playerScript = m_player.GetComponent<Player>();

		m_detectorScript = m_detector.GetComponent<Detector>();

		m_audioSource = GetComponent<AudioSource>();

		m_fOriginalSpeed = m_agent.speed;

		m_animator.SetBool("Walk", false);
		m_animator.SetBool("Run", false);
		m_animator.SetBool("Idle", false);
		m_animator.SetBool("90 Spin", false);
		m_animator.SetBool("Hit", false);
		m_animator.SetBool("Death", false);
	}

	//--------------------------------------------------------------------------------
	// Sets the enemies new waypoint to the next waypoint.
	//--------------------------------------------------------------------------------
	void GoToNextPoint()
	{
		// Returns if no points have been set up
		if (m_targetPoints.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		m_agent.destination = m_targetPoints[m_nDestPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		m_nDestPoint = (m_nDestPoint + 1) % m_targetPoints.Length;
	}

	//--------------------------------------------------------------------------------
	// Sets the enemies new waypoint to the last waypoint.
	//--------------------------------------------------------------------------------
	void GoToLastPoint()
	{
		// Returns if no points have been set up
		if (m_targetPoints.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		m_agent.destination = m_targetPoints[m_nDestPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		m_nDestPoint = (m_nDestPoint - 1) % m_targetPoints.Length;
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, Updates the enemy making it patrol between 
	// waypoints or attack the player if the player is in attack range.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Sets the target rotation to the player.
		var targetRotation = Quaternion.LookRotation(m_playerTransform.position - transform.position);
		// Sets the distance from the player to the enemy.
		m_fDistanceFromPlayer = Vector3.Distance(transform.position, m_playerTransform.position);
		// Checks if the enemy is dead.
		if (m_fHealth <= 0)
		{
			m_animator.SetBool("Walk", false);
			m_animator.SetBool("Run", false);
			m_animator.SetBool("Idle", false);
			m_animator.SetBool("90 Spin", false);
			m_animator.SetBool("Hit", false);
			m_animator.SetBool("Death", true);
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
			m_hookableObj.GetComponent<Collider>().enabled = false;
			m_enemyHitBox.SetActive(false);
			m_detectorScript.ClearTarget(m_hookableObj);
			if (m_bDead)
			{
				if (!m_audioSource.isPlaying && !m_bAudioPlayed)
				{
					Instantiate(m_clamStack, transform.position, transform.rotation);
					m_audioSource.PlayOneShot(m_enemyDeathAudioClip);
					m_bAudioPlayed = true;
				}
				if (m_bAudioPlayed && !m_audioSource.isPlaying && m_bDeathEnd)
				{
					m_hookableObj.tag = "Untagged";
					this.transform.parent.gameObject.SetActive(false);
				}
			}
		}
		else if(!m_bStunned)
		{
			if (m_agent.isOnNavMesh)
			{
				// Choose the next destination point when the agent gets
				// close to the current one and start a cooldown.
				if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
				{
					m_fCooldown -= Time.deltaTime;
					m_agent.speed = m_fOriginalSpeed;
					m_animator.SetBool("Walk", false);
					m_animator.SetBool("Run", false);
					m_animator.SetBool("Idle", true);
					m_animator.SetBool("90 Spin", false);
					
					if (!m_bGoBackWards)
					{
						var waypointRotation = Quaternion.LookRotation(m_targetPoints[1].position - transform.position);
						transform.rotation = Quaternion.Slerp(transform.rotation, waypointRotation, m_fWaitRotateSpeed * Time.deltaTime);

						//transform.rotation = Quaternion.RotateTowards(transform.rotation, m_targetPoints[1].rotation, m_fWaitRotateSpeed);

						m_animator.SetBool("90 Spin", true);
						m_animator.SetBool("Idle", false);
						m_animator.SetBool("Walk", false);
						m_animator.SetBool("Run", false);
					}
					if (m_bGoBackWards)
					{
						var waypointRotation = Quaternion.LookRotation(m_targetPoints[0].position - transform.position);
						transform.rotation = Quaternion.Slerp(transform.rotation, waypointRotation, m_fWaitRotateSpeed * Time.deltaTime);

						//transform.rotation = Quaternion.RotateTowards(transform.rotation, m_targetPoints[0].rotation, m_fWaitRotateSpeed);
							
						m_animator.SetBool("90 Spin", true);
						m_animator.SetBool("Idle", false);
						m_animator.SetBool("Walk", false);
						m_animator.SetBool("Run", false);
					}
					// When the cooldown is over move to next point and resets cooldown time.
					if (m_fCooldown <= 0)
					{
						m_animator.SetBool("Walk", true);
						m_animator.SetBool("Run", false);
						m_animator.SetBool("90 Spin", false);
						m_animator.SetBool("Idle", false);
						// If the last waypoint has been reached turn around.
						if (m_targetPoints[m_nDestPoint].tag == "LastWaypoint")
						{
							m_bGoBackWards = true;
						}
						// If the First waypoint has been reached turn around.
						if (m_targetPoints[m_nDestPoint].tag == "FirstWaypoint")
						{
							m_bGoBackWards = false;
						}
						// If the enemy is going backwards then go to the next waypoint
						// going backwards from the array of waypoints.
						if (m_bGoBackWards)
						{
							GoToLastPoint();
						}
						// If the enemy is not going backwards then go to the next waypoint
						// in order of the array of waypoints.
						if (!m_bGoBackWards)
						{
							GoToNextPoint();
						}
						m_fCooldown = m_fOriginalCooldown;
					}
				}
				// If the player is in range and not in attack range the enemy moves closer.
				if (m_fDistanceFromPlayer <= m_fDistance && m_fDistanceFromPlayer >= m_fAttackDistance)
				{
					m_animator.SetBool("Walk", false);
					m_animator.SetBool("Run", true);
					m_animator.SetBool("90 Spin", false);
					m_animator.SetBool("Idle", false);
					// Sets the destination to the player position.
					m_agent.SetDestination(m_playerTransform.position);
					m_agent.speed = m_fChaseSpeed;
				}
				// When the enemy is in range of attack stop moving, look at player and attack. 
				if (m_fDistanceFromPlayer <= m_fAttackDistance)
				{
					// Sets the destination of the enemies position to stop movement.
					m_agent.SetDestination(transform.position);
					// Smooth lookAt/rotation to player.
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_fRotateSpeed * Time.deltaTime);
					// Reduces cooldown time.
					m_fAttackCooldown -= Time.deltaTime;
					// When the cooldown is over move to next point and resets cooldown time.
					if (m_fAttackCooldown <= 0)
					{
						m_fAttackCooldown = m_fOriginalAttackCooldown;
						m_playerScript.Damage();
					}
				}
			}
		}
		// Checks if the agent is on the navmesh.
		
	}

	public void TakeDamage()
	{
		--m_fHealth;
		m_agent.speed = 0;
		m_bStunned = true;
        if(m_fHealth > 0)
        {
		    m_audioSource.PlayOneShot(m_enemyHitAudioClip);
		}
		StartCoroutine("ResetSpeed");
	}

	public void Death()
	{
		m_bDead = true;
	}

	public void DeathEnd()
	{
		m_bDeathEnd = true;
	}

	IEnumerator ResetSpeed()
	{
		// Waits for 0.1 seconds before being called
		yield return new WaitForSeconds(1.5f);

		m_agent.speed = m_fOriginalSpeed;

		m_bStunned = false;
	}
}
