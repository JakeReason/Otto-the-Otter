using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{

	public bool m_bOrbit = false;
	public bool m_bMoveUp = false;
	public bool m_bMoveDown = false;
	public bool m_bMoveLeft = false;
	public bool m_bMoveRight = false;
	public bool m_bMoveForward = false;
	public bool m_bMoveBackward = false;
	public bool m_bRotateUp = false;
	public bool m_bRotateDown = false;
	public bool m_bRotateLeft = false;
	public bool m_bRotateRight = false;
	public bool m_bRotateForward = false;
	public bool m_bRotateBackward = false;
	public bool m_bFollowPath = false;
	public float m_fSpeed = 1;
	public float m_fRotationSpeed = 10;
	private float m_fMoveTimer = 0;
	public float m_fReturnTime = 3;
	public float m_fWaitTime = 1;
	public float m_fOriginalTime = 1;
	private float m_fResetTime;

	[SerializeField]
	// An array of transform used to create a patrol route.
	private Transform[] m_targetPoints;

	[SerializeField]
	// Keeps track of the current waypoint number.
	private int m_nDestPoint = 0;

	[SerializeField]
	// Used to keep track of whether the enemy of going backwards. 
	private bool m_bGoBackWards = false;

	// Use this for initialization
	void Awake()
	{
		m_fResetTime += m_fReturnTime * 2 + m_fWaitTime * m_fWaitTime;
		m_fWaitTime += m_fReturnTime;
		// Sets the first point to go to.
		GoToNextPoint();
	}

	// Update is called once per frame
	void Update()
	{
		m_fMoveTimer += Time.deltaTime;
		Move();
		Rotate();
		FollowPath();
	}

	void Rotate()
	{
		if (m_bRotateForward)
		{
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
			if (m_bMoveUp)
			{
				if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
		}
		if (m_bMoveLeft)
		{
			if (m_bMoveUp)
			{
				if (m_fMoveTimer < m_fReturnTime && m_fMoveTimer > m_fOriginalTime)
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
	}

	public void Orbit()
	{

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
		transform.position = Vector3.MoveTowards(transform.position, m_targetPoints[m_nDestPoint].position, m_fSpeed * Time.deltaTime);

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
		transform.position = Vector3.MoveTowards(transform.position, m_targetPoints[m_nDestPoint].position, m_fSpeed * Time.deltaTime);

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

		}

	}


}
