//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
	[SerializeField]
	// Min distance from the player.
	private float minDistance = 1.0f;

	[SerializeField]
	// Max distance from the player.
	private float maxDistance = 4.0f;

	[SerializeField]
	// Used for setting the postion.
	private float smooth = 10.0f;

	[SerializeField]
	// Distance from the camera to the player.
	private float distance;

	[HideInInspector]
	// Used to change the aplha of walls in another script.
	public float m_fChangeTime = 0.0f;

	// Normalized local position.
	public Vector3 dollyDir;

	public float m_fDistanceFromGround = 1.0f;

	public LayerMask CollisionLayers;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		// Sets the dolly direction.
		dollyDir = transform.localPosition.normalized;
		// Sets the distance.
		distance = transform.localPosition.magnitude;
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, Updates the camera's position if it is 
	// colliding with anything as well as sets a variable used to set some walls to
	// be slightly invisible.
	//--------------------------------------------------------------------------------
	void FixedUpdate ()
	{
		// Sets the desiredCameraPos.
		Vector3 desiredCameraPos = transform.parent.TransformPoint (dollyDir * maxDistance);
		// Used for linecast.
		RaycastHit hit;
		// Used for raycast.
		RaycastHit Hit;
		// Sets forward vector to the transforms forward.
		Vector3 forward = transform.TransformDirection(Vector3.forward);

		// Starts increasing change time.
		m_fChangeTime += Time.deltaTime;
		// Makes sure change time does not go above 1.
		if (m_fChangeTime >= 1.0f)
		{
			m_fChangeTime = 1.0f;
		}

		// Checks if the camera has something in front of it.
		if (Physics.Raycast(transform.position, forward, out Hit, distance - 0.8f))
		{
		}
		else
		{
			// If not then set the change time to 0.
			m_fChangeTime = 0.0f;
		}

		// Checks if the camera has floor is below of it, and slides up towards the player.
		if (Physics.Raycast(transform.position, -transform.up, out Hit, m_fDistanceFromGround))
		{
			//dollyDir.y += 0.01f;
		}
		else
		{
		}

		Debug.DrawRay(transform.position, -transform.up);

		// Line cast checking for any obstruction that is not on layer 8.
		if (Physics.Linecast (transform.parent.position, desiredCameraPos, out hit))
		{
			if(hit.collider.gameObject.layer != 10)
			{
				// Set the distance to the hit distance of the object clamped.
				distance = Mathf.Clamp ((hit.distance * 0.87f), minDistance, maxDistance);
			}
		}
		else
		{
			distance = maxDistance;
		}

		// Sets the new position for the camera.
		transform.localPosition = Vector3.Lerp (transform.localPosition, dollyDir * distance, smooth * Time.deltaTime);
	}
}
