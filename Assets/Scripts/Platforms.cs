//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas. Edited by Matt Le Nepveu.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
	[SerializeField]
	// Used to determine to orbit.
	private bool m_bOrbit = false;

	[SerializeField]
	// Used to determine which way to move.
	private bool m_bMoveUp = false;

	[SerializeField]
	// Used to determine which way to move.
	private bool m_bMoveDown = false;

	[SerializeField]
	// Used to determine which way to move.
	private bool m_bMoveLeft = false;

	[SerializeField]
	// Used to determine which way to move.
	private bool m_bMoveRight = false;

	[SerializeField]
	// Used to determine which way to move.
	private bool m_bMoveForward = false;

	[SerializeField]
	// Used to determine which way to move.
	private bool m_bMoveBackward = false;

	[SerializeField]
	// Used to determine which way to rotate.
	private bool m_bRotateUp = false;

	[SerializeField]
	// Used to determine which way to rotate.
	private bool m_bRotateDown = false;

	[SerializeField]
	// Used to determine which way to rotate.
	private bool m_bRotateLeft = false;

	[SerializeField]
	// Used to determine which way to rotate.
	private bool m_bRotateRight = false;

	[SerializeField]
	// Used to determine which way to rotate.
	private bool m_bRotateForward = false;

	[SerializeField]
	// Used to determine which way to rotate.
	private bool m_bRotateBackward = false;

	[SerializeField]
	// Used to determine if it needs to follow a path.
	private bool m_bFollowPath = false;

	[SerializeField]
	// Used to set the speed.
	private float m_fSpeed = 1;

	[SerializeField]
	// Used to set the rotation speed.
	private float m_fRotationSpeed = 10;

	[SerializeField]
	// How long it waits before returning.
	private float m_fReturnTime = 3;

	[SerializeField]
	// How long it waits before moving.
	private float m_fWaitTime = 1;

	[SerializeField]
	// How long it waits before moving at the start.
	private float m_fStartWaitTime = 1;

	[SerializeField]
	// An array of transform used to create a patrol route.
	private Transform[] m_targetPoints;

	// Used to move the platforms
	private float m_fMoveTimer = 0;

	// Used to  move the platforms along a path.
	private float m_fFollowMoveTimer = 2;

	[SerializeField]
	// How long it waits before moving.
	private float m_fFollowWaitTime = 1;

	// A float used to determine when to reset the timer.
	private float m_fResetTime;

	[SerializeField]
	// The speed of pushing the player back.
	private float m_fPushBackSpeed = 1;

	// Keeps track of the current waypoint number.
	private int m_nDestPoint = 0;

	// Used to keep track of whether the enemy of going backwards. 
	private bool m_bGoBackWards = false;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Sets the reset time.
		m_fResetTime += m_fReturnTime * 2 + m_fWaitTime * m_fWaitTime;
		// Sets the wait time.
		m_fWaitTime += m_fReturnTime + m_fStartWaitTime;
		// Sets the follow timer.
		m_fFollowMoveTimer = m_fFollowWaitTime;
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame. Updates the platforms and moves them.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Used for moving the patforms.
		m_fMoveTimer += Time.deltaTime;
		// Moves the platforms.
		Move();
		// Rotates the platforms.
		Rotate();
		// Makes the platforms orbit.
		Orbit();
		// Makes the platforms follow a path.
		FollowPath();
	}

	//--------------------------------------------------------------------------------
	// Rotates the platforms based on the bools ticked in inspector.
	//--------------------------------------------------------------------------------
	void Rotate()
	{
		// Rotates the platform a certain direction and makes it come rotate back 
		// after a certain amount of time and resets the timer.
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
		// Rotates the platform a certain direction and makes it come rotate back 
		// after a certain amount of time and resets the timer.
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
		// Rotates the platform a certain direction and makes it come rotate back 
		// after a certain amount of time and resets the timer.
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
		// Rotates the platform a certain direction and makes it come rotate back 
		// after a certain amount of time and resets the timer.
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
		// Rotates the platform a certain direction and makes it come rotate back 
		// after a certain amount of time and resets the timer.
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
		// Rotates the platform a certain direction and makes it come rotate back 
		// after a certain amount of time and resets the timer.
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

	//--------------------------------------------------------------------------------
	// Moves the platforms based on the bools ticked in inspector.
	//--------------------------------------------------------------------------------
	void Move()
	{
		// Moves the platform a certain direction and makes it come back after a 
		// certain amount of time and resets the timer.
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
		// Moves the platform a certain direction and makes it come back after a 
		// certain amount of time and resets the timer.
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
		// Moves the platform a certain direction and makes it come back after a 
		// certain amount of time and resets the timer.
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
		// Moves the platform a certain direction and makes it come back after a 
		// certain amount of time and resets the timer.
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
		// Moves the platform a certain direction and makes it come back after a 
		// certain amount of time and resets the timer.
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
		// Moves the platform a certain direction and makes it come back after a 
		// certain amount of time and resets the timer.
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

	//--------------------------------------------------------------------------------
	// Makes the platform orbit and rotate.
	//--------------------------------------------------------------------------------
	public void Orbit()
	{
		if (m_bOrbit)
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

	//--------------------------------------------------------------------------------
	// Makes the platform follow a path.
	//--------------------------------------------------------------------------------
	void FollowPath()
	{
		// Checks if the follow path is ticked.
		if (m_bFollowPath)
		{
			for (int i = 0; i < m_targetPoints.Length; ++i)
			{
				// Checks if the position is equal to the target postion.
				if (transform.position == m_targetPoints[i].position)
				{
					// Decreases th move timer.
					m_fFollowMoveTimer -= Time.deltaTime;
					if (m_fFollowMoveTimer <= 0)
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
						m_fFollowMoveTimer = m_fFollowWaitTime;
					}
				}
			}
			// Updates the postion.
			transform.position = Vector3.MoveTowards(transform.position, m_targetPoints[m_nDestPoint].position, m_fSpeed * Time.deltaTime);
		}

	}

	//--------------------------------------------------------------------------------
	// Pushes the other object back away from the platform.
	//
	// Param:
	//		other: used to find the other colliding object.
	//--------------------------------------------------------------------------------
	public void PushBack(Collider other)
	{
		if (m_bRotateForward)
		{
			//transform.position = transform.position - Hit.transform.position * 1 * Time.deltaTime;
			other.transform.position += -transform.right * m_fPushBackSpeed * Time.deltaTime;
		}
		if (m_bRotateBackward)
		{
			//transform.position = transform.position - Hit.transform.position * 1 * Time.deltaTime;
			other.transform.position += transform.right * m_fPushBackSpeed * Time.deltaTime;
		}
		if (m_bRotateDown)
		{
			//transform.position = transform.position - Hit.transform.position * 1 * Time.deltaTime;
			other.transform.position += transform.right * m_fPushBackSpeed * Time.deltaTime;
		}
		if (m_bRotateUp)
		{
			//transform.position = transform.position - Hit.transform.position * 1 * Time.deltaTime;
			other.transform.position += -transform.right * m_fPushBackSpeed * Time.deltaTime;
		}
	}

	//--------------------------------------------------------------------------------
	// OnTriggerStay checks if the player is colliding with this object and Sets it as
	// a parent so the player follows/stays on the platform when it moves.
	//
	// Param:
	//		other: used to find the other colliding object.
	//
	//--------------------------------------------------------------------------------
	private void OnTriggerStay(Collider other)
	{
		// Sets the other object as the parent.
		other.transform.parent = this.transform;
		// Pushes back if it is rotating.
		PushBack(other);
	}

	//--------------------------------------------------------------------------------
	// OnTriggerExit checks if the other object has left the collider and removes the 
	// parent.
	//
	// Param:
	//		other: used to find the other colliding object.
	//
	//--------------------------------------------------------------------------------
	private void OnTriggerExit(Collider other)
	{
		other.transform.parent = null;
	}
}
