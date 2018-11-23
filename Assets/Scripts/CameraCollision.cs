//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas. NOT BEING USED.
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

	// Normalized local position.
	public Vector3 dollyDir;

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
