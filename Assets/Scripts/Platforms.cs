using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
	[SerializeField]
	// 
	private bool m_bOrbit = false;

	[SerializeField]
	// 
	private bool m_bMoveUp = false;

	[SerializeField]
	// 
	private bool m_bMoveDown = false;

	[SerializeField]
	// 
	private bool m_bMoveLeft = false;

	[SerializeField]
	// 
	private bool m_bMoveRight = false;

	[SerializeField]
	// 
	private bool m_bMoveForward = false;

	[SerializeField]
	// 
	private bool m_bMoveBackward = false;

	[SerializeField]
	// 
	private bool m_bRotateUp = false;

	[SerializeField]
	// 
	private bool m_bRotateDown = false;

	[SerializeField]
	// 
	private bool m_bRotateLeft = false;

	[SerializeField]
	// 
	private bool m_bRotateRight = false;

	[SerializeField]
	// 
	private bool m_bRotateForward = false;

	[SerializeField]
	// 
	private bool m_bRotateBackward = false;

	[SerializeField]
	// 
	private bool m_bFollowPath = false;

	[SerializeField]
	// 
	private float m_fSpeed = 1;

	[SerializeField]
	// 
	private float m_fRotationSpeed = 10;

	[SerializeField]
	// 
	private float m_fReturnTime = 3;

	[SerializeField]
	// 
	private float m_fWaitTime = 1;

	[SerializeField]
	//
	private float m_fStartWaitTime = 1;

	[SerializeField]
	// An array of transform used to create a patrol route.
	private Transform[] m_targetPoints;

	//
	private float m_fMoveTimer = 0;


	//
	private float m_fResetTime;

	// Keeps track of the current waypoint number.
	private int m_nDestPoint = 0;

	// Used to keep track of whether the enemy of going backwards. 
	private bool m_bGoBackWards = false;

	// Use this for initialization
	void Awake()
	{
		m_fResetTime += m_fReturnTime * 2 + m_fWaitTime * m_fWaitTime;
		m_fWaitTime += m_fReturnTime + m_fStartWaitTime;
	}

	// Update is called once per frame
	void Update()
	{
		m_fMoveTimer += Time.deltaTime;
		Move();
		Rotate();
		Orbit();
		FollowPath();
	}

	void Rotate()
	{
		if (m_bRotateForward)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.Rotate(Vector3.forward * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.Rotate(-Vector3.forward * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bRotateBackward)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.Rotate(-Vector3.forward * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.Rotate(Vector3.forward * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bRotateUp)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.Rotate(Vector3.up * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.Rotate(-Vector3.up * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bRotateDown)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.Rotate(-Vector3.up * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.Rotate(Vector3.up * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bRotateRight)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.Rotate(Vector3.right * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.Rotate(-Vector3.right * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bRotateLeft)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.Rotate(-Vector3.right * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.Rotate(Vector3.right * (m_fRotationSpeed * Time.deltaTime));
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
	}

	void Move()
	{
		if (m_bMoveForward)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.position += transform.forward * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.position -= transform.forward * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bMoveBackward)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.position -= transform.forward * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.position += transform.forward * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bMoveUp)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.position += transform.up * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.position -= transform.up * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bMoveDown)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.position -= transform.up * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.position += transform.up * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bMoveRight)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.position += transform.right * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.position -= transform.right * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
		if (m_bMoveLeft)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fStartWaitTime)
			{
				transform.position -= transform.right * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer > m_fWaitTime)
			{
				transform.position += transform.right * m_fSpeed * Time.deltaTime;
			}
			if (m_fMoveTimer >= m_fResetTime)
			{
				m_fMoveTimer = 0;
			}
		}
	}

	public void Orbit()
	{
		if(m_bOrbit)
		{
			transform.position += transform.right * m_fSpeed * Time.deltaTime;
			transform.Rotate(Vector3.up * (m_fRotationSpeed * Time.deltaTime));
		}
	}

	//--------------------------------------------------------------------------------
	// Sets the enemies new waypoint to the next waypoint.
	//--------------------------------------------------------------------------------
	void GoToNextPoint()
	{
		// Returns if no points have been set up
		if (m_targetPoints.Length == 0)
			return;

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

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		m_nDestPoint = (m_nDestPoint - 1) % m_targetPoints.Length;
	}

	void FollowPath()
	{
		if (m_bFollowPath)
		{
			for (int i = 0; i < m_targetPoints.Length; ++i)
			{
				if (transform.position == m_targetPoints[i].position)
				{
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
				}
			}
			// Set the agent to go to the currently selected destination.
			transform.position = Vector3.MoveTowards(transform.position, m_targetPoints[m_nDestPoint].position, m_fSpeed * Time.deltaTime);
		}

	}


}
