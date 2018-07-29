using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

	[SerializeField]
	public float m_fDistance;
	[SerializeField]
	private float m_fMinDistance = 1.0f;
	[SerializeField]
	private float m_fMaxDistance = 10.0f;
	[SerializeField]
	private float m_fSmooth = 10.0f;
	[SerializeField]
	private Vector3 m_v3CameraPosNormalized;

	// Use this for initialization
	void Awake ()
	{
		m_v3CameraPosNormalized = transform.localPosition.normalized;
		//m_fDistance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		RaycastHit hit;
		Vector3 v3DesiredCameraPos = transform.position;

		Debug.DrawLine(transform.parent.position, transform.position, Color.red);

		if (Physics.Linecast(transform.parent.position, v3DesiredCameraPos, out hit))
		{
			//if(hit.collider.gameObject.tag != "Player")
			//{
				m_fDistance = Mathf.Clamp(hit.distance, m_fMinDistance, m_fMaxDistance);
			//}
		}
		//else if(hit.distance >= 10) // Wring Logic Keeps setting camera to 10 when it should not be
		//{
		//	m_fDistance = m_fMaxDistance;
		//}

		Physics.Linecast(transform.parent.position, v3DesiredCameraPos, out hit);

		if(hit.distance >= 10 || hit.collider == null)
		{
			m_fDistance = m_fMaxDistance;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, m_v3CameraPosNormalized * m_fDistance, Time.deltaTime * m_fSmooth);
		m_v3CameraPosNormalized = transform.localPosition.normalized;
		GetComponent<CameraScript>().m_fDistance = m_fDistance;
	}
}
