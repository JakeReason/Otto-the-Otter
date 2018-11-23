//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas. Edited by Matt Le Nepveu.
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
	// Used to play the death sound.
	private AudioClip m_enemyDeathAudioClip;

	[SerializeField]
	// Used to play the hit sound.
	private AudioClip m_enemyHitAudioClip;

	// Used to play audio for the enemy.
	private AudioSource m_audioSource;

	// The enemy model used to turn in visible.
	public GameObject m_wolfModel;

	// Used to get the detector script.
	public GameObject m_detector;

	// Used to clear the current object in the hook detector.
	private Detector m_detectorScript;

	// Used to determine if the audio has been played.
	private bool m_bAudioPlayed;

	// Used to make the object not hookable.
	public GameObject m_hookableObj;

	// A reward when the enemy dies.
	public GameObject m_clamStack;

	// Speed of the enemy when chasing the player.
	public float m_fChaseSpeed;

	// The hitbox of the enemy.
    public GameObject m_enemyHitBox;

	// Stores the original speed.
	private float m_fOriginalSpeed;

	// Used to animate the enemy.
	private Animator m_animator;

	// A bool to check if the enemy is dead.
	private bool m_bDead;

	// A bool which is called at the end of the death animation.
	private bool m_bDeathEnd;

	// Used to stun the enemy.
	private bool m_bStunned;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets the animator component to animate the enemy.
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
		// Sets the detector script refernce up.
		m_detectorScript = m_detector.GetComponent<Detector>();
		// Gets the audio scource on the gameobject.
		m_audioSource = GetComponent<AudioSource>();
		// Stores the original speed.
		m_fOriginalSpeed = m_agent.speed;

		// Sets the enemy animation conditions to false.
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
	// Update is called once per frame. Updates the enemy making it patrol between 
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
			// Sets the animation conditions.
			m_animator.SetBool("Walk", false);
			m_animator.SetBool("Run", false);
			m_animator.SetBool("Idle", false);
			m_animator.SetBool("90 Spin", false);
			m_animator.SetBool("Hit", false);
			m_animator.SetBool("Death", true);
			// Turns off the collider of the enemy.
			GetComponent<BoxCollider>().enabled = false;
			// Turns off the NavMeshAgent of the enemy.
			GetComponent<NavMeshAgent>().enabled = false;
			// Turns off the hookable object collider.
			m_hookableObj.GetComponent<Collider>().enabled = false;
			// Turns off the hit box of the enemy.
			m_enemyHitBox.SetActive(false);
			// Clears the current hooked/targeted object of the hook.
			m_detectorScript.ClearTarget(m_hookableObj);
			// Checks if the dead bool is true which is set at the start of the animation.
			if (m_bDead)
			{
				// Checks if audio is playing from the audio scource and if it has been 
				// played before.
				if (!m_audioSource.isPlaying && !m_bAudioPlayed)
				{
					// Spawn a stack of clams.
					Instantiate(m_clamStack, transform.position, transform.rotation);
					// Plays the death audio.
					m_audioSource.PlayOneShot(m_enemyDeathAudioClip);
					// Sets played to true.
					m_bAudioPlayed = true;
				}
				// Checks if audio is playing from the audio scource and if it has been 
				// played before and if the end of the death animation as reached by
				// the death end bool.
				if (m_bAudioPlayed && !m_audioSource.isPlaying && m_bDeathEnd)
				{
					// Changes the hookable object tag to prevent problems whith the
					// hook.
					m_hookableObj.tag = "Untagged";
					// Sets the parent to false which disables the enemy.
					this.transform.parent.gameObject.SetActive(false);
				}
			}
		}
		// Checks if the enemy is stunned.
		else if(!m_bStunned)
		{
			// Checks if the enemy is on the navmesh.
			if (m_agent.isOnNavMesh)
			{
				// Choose the next destination point when the agent gets
				// close to the current one and start a cooldown.
				if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
				{
					// Decreases the cooldown.
					m_fCooldown -= Time.deltaTime;
					// Sets the agents speed to the original speed.
					m_agent.speed = m_fOriginalSpeed;
					// Sets the animation conditions.
					m_animator.SetBool("Walk", false);
					m_animator.SetBool("Run", false);
					m_animator.SetBool("Idle", true);
					m_animator.SetBool("90 Spin", false);
					// Checks if the enemy is going backwards.
					if (!m_bGoBackWards)
					{
						// Rotates the enemy towards the next waypoint.
						var waypointRotation = Quaternion.LookRotation(m_targetPoints[1].position - transform.position);
						transform.rotation = Quaternion.Slerp(transform.rotation, waypointRotation, m_fWaitRotateSpeed * Time.deltaTime);
						// Sets the animation conditions.
						m_animator.SetBool("90 Spin", true);
						m_animator.SetBool("Idle", false);
						m_animator.SetBool("Walk", false);
						m_animator.SetBool("Run", false);
					}
					// Checks if the enemy is going backwards.
					if (m_bGoBackWards)
					{
						// Rotates the enemy towards the next waypoint.
						var waypointRotation = Quaternion.LookRotation(m_targetPoints[0].position - transform.position);
						transform.rotation = Quaternion.Slerp(transform.rotation, waypointRotation, m_fWaitRotateSpeed * Time.deltaTime);
						// Sets the animation conditions.
						m_animator.SetBool("90 Spin", true);
						m_animator.SetBool("Idle", false);
						m_animator.SetBool("Walk", false);
						m_animator.SetBool("Run", false);
					}
					// When the cooldown is over move to next point and resets cooldown time.
					if (m_fCooldown <= 0)
					{
						// Sets the animation conditions.
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
						// Sets the cooldown time back to the original.
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
						// Deals damage to the player.
						m_playerScript.Damage();
					}
				}
			}
		}	
	}

	//--------------------------------------------------------------------------------
	// Updates the enemy making it patrol between 
	// waypoints or attack the player if the player is in attack range.
	//--------------------------------------------------------------------------------
	public void TakeDamage()
	{
		// Negates health from the enemy.
		--m_fHealth;
		// Sets the speed to zero.
		m_agent.speed = 0;
		// Sets stunned to true.
		m_bStunned = true;
		// Checks if the health is above zero and plays the hit sound.
        if(m_fHealth > 0)
        {
		    m_audioSource.PlayOneShot(m_enemyHitAudioClip);
		}
		// Calls the ResetSpeed coroutine.
		StartCoroutine("ResetSpeed");
	}

	//--------------------------------------------------------------------------------
	// Used to set dead to true in the animation.
	//--------------------------------------------------------------------------------
	public void Death()
	{
		m_bDead = true;
	}

	//--------------------------------------------------------------------------------
	// Used to set the death end to true at the end of the animation.
	//--------------------------------------------------------------------------------
	public void DeathEnd()
	{
		m_bDeathEnd = true;
	}

	//--------------------------------------------------------------------------------
	// Coroutine used to reset the speed after 1.5
	// seconds and sets stunned to false.
	//--------------------------------------------------------------------------------
	IEnumerator ResetSpeed()
	{
		// Waits for 1.5 seconds before being called
		yield return new WaitForSeconds(1.5f);
		// Sets the speed bakc to the original speed.
		m_agent.speed = m_fOriginalSpeed;
		// Sets stunned to false.
		m_bStunned = false;
	}
}
