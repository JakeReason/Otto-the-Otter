using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CameraScript : MonoBehaviour {

	[SerializeField]
	private XboxController controller;

	[SerializeField]
	private const float Y_ANGLE_MIN = -89.9f;
	[SerializeField]
	private const float Y_ANGLE_MAX = 89.9f;

	[SerializeField]
	private Transform m_lookAt;
	[SerializeField]
	private Transform m_cameraTransform;

	private Camera m_camera;
	float changeTime = 1.0f;

	[SerializeField]
	private float m_fDistance = 10.0f;
	[SerializeField]
	private float m_fCurrentX = 0.0f;
	[SerializeField]
	private float m_fCurrentY = 0.0f;
	[SerializeField]
	private float m_fSensitivityX = 4.0f;
	[SerializeField]
	private float m_fSensitivityY = 1.0f;

	// Use this for initialization
	void Start () {
		m_cameraTransform = transform;
		m_fCurrentY = 20.0f;
		m_camera = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		m_fCurrentX += XCI.GetAxis(XboxAxis.RightStickX);
		m_fCurrentY += XCI.GetAxis(XboxAxis.RightStickY);

		m_fCurrentY = Mathf.Clamp(m_fCurrentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

		if (XCI.GetButton(XboxButton.RightStick))
		{
			m_fCurrentX = m_lookAt.rotation.eulerAngles.y;
			m_fCurrentY = 20.0f;
		}

		RaycastHit Hit;
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		changeTime -= Time.deltaTime;
		if (changeTime <= 0.5f)
		{
			changeTime = 0.5f;
		}
		if (Physics.Raycast(transform.position, forward, out Hit, m_fDistance - 0.8f))
		{
			Hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, changeTime);
		}
		else
		{
				changeTime = 1.0f;
		}
	}

	private void LateUpdate()
	{
		Vector3 v3Dir = new Vector3(0, 0, -m_fDistance);
		Quaternion rotation = Quaternion.Euler(m_fCurrentY, m_fCurrentX, 0);
		m_cameraTransform.position = m_lookAt.position + rotation * v3Dir;
		m_cameraTransform.LookAt(m_lookAt.position);
	}
}
