using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CameraFollow2 : MonoBehaviour {

	[SerializeField]
	private float CameraMoveSpeed = 120.0f;
	[SerializeField]
	private GameObject CameraFollowObj;
	[SerializeField]
	private float MaxClampAngle = 80.0f;
	[SerializeField]
	private float MinClampAngle = -45.0f;
	[SerializeField]
	private float inputSensitivity = 150.0f;
	[SerializeField]
	private float mouseX;
	[SerializeField]
	private float mouseY;
	[SerializeField]
	private float finalInputX;
	[SerializeField]
	private float finalInputZ;
	[SerializeField]
	private float rotY = 0.0f;
	[SerializeField]
	private float rotX = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rot.x = 20.0f;
		rotY = rot.y;
		rotX = rot.x;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// We setup the rotation of the sticks here
		float inputX = Input.GetAxis ("RightStickHorizontal");
		float inputZ = Input.GetAxis ("RightStickVertical");
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;

		rotY += finalInputX * inputSensitivity * Time.deltaTime;
		rotX += finalInputZ * inputSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp (rotX, MinClampAngle, MaxClampAngle);

		if(XCI.GetButtonDown(XboxButton.RightStick))
		{
			rotX = 20.0f;
			rotY = CameraFollowObj.transform.rotation.eulerAngles.y;
		}

		Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
		transform.rotation = localRotation;

	}

	void LateUpdate () {
		CameraUpdater ();
	}

	void CameraUpdater() {
		// set the target object to follow
		Transform target = CameraFollowObj.transform;

		//move towards the game object that is the target
		float step = CameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
