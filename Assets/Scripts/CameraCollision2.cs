using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision2 : MonoBehaviour {

	public float minDistance = 1.0f;
	public float maxDistance = 4.0f;
	public float smooth = 10.0f;
	Vector3 dollyDir;
	public Vector3 dollyDirAdjusted;
	public float distance;
	public float m_fChangeTime = 0.0f;

	// Use this for initialization
	void Awake ()
	{
		dollyDir = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 desiredCameraPos = transform.parent.TransformPoint (dollyDir * maxDistance);
		RaycastHit hit;
		RaycastHit Hit;
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		m_fChangeTime += Time.deltaTime;

		if (m_fChangeTime >= 1.0f)
		{
			m_fChangeTime = 1.0f;
		}

		if (Physics.Raycast(transform.position, forward, out Hit, distance - 0.8f))
		{
			if (Hit.collider.gameObject.layer == 8)
			{
				Hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, m_fChangeTime);
			}
		}
		else
		{
			m_fChangeTime = 0.0f;
		}
		Debug.DrawLine(transform.parent.position, desiredCameraPos, Color.red);

		if (Physics.Linecast (transform.parent.position, desiredCameraPos, out hit))
		{
			if(hit.collider.gameObject.layer != 8)
			{
				distance = Mathf.Clamp ((hit.distance * 0.87f), minDistance, maxDistance);
			}
		}
		else
		{
			distance = maxDistance;
		}

		transform.localPosition = Vector3.Lerp (transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
