using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision2 : MonoBehaviour
{
	[SerializeField]
	private float minDistance = 1.0f;
	[SerializeField]
	private float maxDistance = 4.0f;
	[SerializeField]
	private float smooth = 10.0f;
	[SerializeField]
	private float distance;
	[HideInInspector]
	public float m_fChangeTime = 0.0f;

	Vector3 dollyDir;

	// Use this for initialization
	void Awake ()
	{
		// Sets the dolly direction.
		dollyDir = transform.localPosition.normalized;
		// Sets the distance.
		distance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update ()
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

		// Draws a debug line in the scene. TODO: Remove later on.
		Debug.DrawLine(transform.parent.position, desiredCameraPos, Color.red);

		// Line cast checking for any obstruction that is not on layer 8.
		if (Physics.Linecast (transform.parent.position, desiredCameraPos, out hit))
		{
			if(hit.collider.gameObject.layer != 8)
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
		transform.localPosition = Vector3.Lerp (transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
