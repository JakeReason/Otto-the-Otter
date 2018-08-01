using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour {

    private NavMeshAgent m_agent;
    private float m_fOriginalCooldown;
	private float m_fOriginalAttackCooldown;
	private float m_fDistanceFromPlayer;
	private Player m_playerScript;

	[SerializeField]
	private float m_fHealth = 1;
	[SerializeField]
	private GameObject m_player;
	[SerializeField]
	private Transform m_playerTransform;
	[SerializeField]
    private Transform[] m_targetPoints;
    [SerializeField]
    private int m_nDestPoint = 0;
    [SerializeField]
    private float m_fCooldown = 1;
	[SerializeField]
	private float m_fDistance = 5;
	[SerializeField]
	private float m_fAttackDistance = 1;
	[SerializeField]
	private float m_fRotateSpeed = 1;
	[SerializeField]
	private float m_fAttackCooldown = 1;
	[SerializeField]
	private bool m_bGoBackWards = false;

	// Use this for initialization
	void Start ()
    {
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

		m_playerScript = m_player.GetComponent<Player>();
    }
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

	// Update is called once per frame
	void Update ()
    {
		// Sets the target rotation to the player.
		var targetRotation = Quaternion.LookRotation(m_playerTransform.position - transform.position);
		// Sets the distance from the player to the enemy.
		m_fDistanceFromPlayer = Vector3.Distance(transform.position, m_playerTransform.position);
		// Checks if the enemy is dead.
		if(m_fHealth <= 0)
		{
			gameObject.SetActive(false);
		}
		// Checks if the agent is on the navmesh.
		if (m_agent.isOnNavMesh)
        {
			// Choose the next destination point when the agent gets
			// close to the current one and start a cooldown.
			if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
            {
                m_fCooldown -= Time.deltaTime;
				// When the cooldown is over move to next point and resets cooldown time.
                if (m_fCooldown <= 0)
                {
					// If the last waypoint has been reached turn around.
					if(m_targetPoints[m_nDestPoint].tag == "LastWaypoint")
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
					if(m_bGoBackWards)
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
				// Sets the destination to the player position.
				m_agent.SetDestination(m_playerTransform.position);
			}
			// When the enemy is in range of attack stop moving, look at player and attack. 
			if(m_fDistanceFromPlayer <= m_fAttackDistance)
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
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Hook")
		{
			--m_fHealth;
		}
	}
}
