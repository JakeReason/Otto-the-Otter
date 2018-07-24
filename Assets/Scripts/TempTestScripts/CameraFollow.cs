using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private float m_fCameraMovementSpeed = 120.0f;
	[SerializeField]
	private GameObject m_CameraFollowObject;
	[SerializeField]
	private float m_fClampAngel = 80.0f;
	[SerializeField]
	private float m_fInputSensitivity = 150.0f;
	[SerializeField]
	private GameObject m_CameraObj;
	[SerializeField]
	private GameObject m_PlayerObj;
	[SerializeField]
	private float m_fCamDistanceToPlayerX;
	[SerializeField]
	private float m_fCamDistanceToPlayerY;
	[SerializeField]
	private float m_fCamDistanceToPlayerZ;
	[SerializeField]
	private float m_fMouseX;
	[SerializeField]
	private float m_fMouseY;
	[SerializeField]
	private float m_fFinalInputX;
	[SerializeField]
	private float m_fFinalInputY;
	[SerializeField]
	private float m_fSmoothX;
	[SerializeField]
	private float m_fSmoothY;
	[SerializeField]
	private float m_fRotX = 0.0f;
	[SerializeField]
	private float m_fRotY = 0.0f;

	private float m_fChangeTime = 1.0f;

	// Use this for initialization
	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		m_fRotX = rot.x;
		m_fRotY = rot.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float fInputX = XCI.GetAxis(XboxAxis.RightStickY);
		float fInputY = XCI.GetAxis(XboxAxis.RightStickX);
		m_fMouseX = Input.GetAxis("Mouse X");
		m_fMouseY = Input.GetAxis("Mouse Y");
		m_fFinalInputX = fInputX + m_fMouseX;
		m_fFinalInputY = fInputY + m_fMouseY;

		m_fRotX += m_fFinalInputX * m_fInputSensitivity * Time.deltaTime;
		m_fRotY += m_fFinalInputY * m_fInputSensitivity * Time.deltaTime;

		m_fRotX = Mathf.Clamp(m_fRotX, -m_fClampAngel, m_fClampAngel);

		Quaternion localRotation = Quaternion.Euler(m_fRotX, m_fRotY, 0.0f);
		transform.rotation = localRotation;

		//RaycastHit Hit;
		//Vector3 forward = transform.TransformDirection(Vector3.forward);
		//m_fChangeTime -= Time.deltaTime;

		//if (m_fChangeTime <= 0.5f)
		//{
		//	m_fChangeTime = 0.5f;
		//}

		//if (Physics.Raycast(transform.position, forward, out Hit, m_fDistance - 0.8f))
		//{
		//	if (Hit.collider.gameObject.layer == 8)
		//	{
		//		Hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, m_fChangeTime);
		//	}
		//}
		//else
		//{
		//	m_fChangeTime = 1.0f;
		//}
	}

	private void LateUpdate()
	{
		CameraUpdate();
	}

	void CameraUpdate()
	{
		Transform target = m_CameraFollowObject.transform;

		float fStep = m_fCameraMovementSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, fStep);
	}
}
