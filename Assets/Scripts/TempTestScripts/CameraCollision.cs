using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

	[SerializeField]
	private Transform m_player;
	[SerializeField]
	private float m_fDistance;
	[SerializeField]
	private float m_fMinDistance = 1.0f;
	[SerializeField]
	private float m_fMaxDistance = 10.0f;
	[SerializeField]
	private float m_fSmooth = 10.0f;
	[SerializeField]
	private Vector3 m_v3DollyDir;
	[SerializeField]
	private Vector3 m_v3DollyAdjusted;

	// Use this for initialization
	void Awake ()
	{
		m_v3DollyDir = transform.localPosition.normalized;
		m_fDistance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit Hit;
		Vector3 v3DesiredCameraPos = transform.TransformPoint(m_v3DollyDir * m_fMaxDistance);
		m_v3DollyDir = transform.localPosition.normalized;

		if (Physics.Linecast(transform.parent.position, v3DesiredCameraPos, out Hit))
		{
			m_fDistance = Mathf.Clamp((Hit.distance * 0.9f), m_fMinDistance, m_fMaxDistance);
		}
		else
		{
			m_fDistance = m_fMaxDistance;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, m_v3DollyDir * m_fDistance, Time.deltaTime * m_fSmooth);
		transform.LookAt(m_player.position);
	}
}
