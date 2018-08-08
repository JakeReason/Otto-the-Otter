//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CameraFollow2 : MonoBehaviour
{
	[SerializeField]
	// Camera's move speed.
	private float CameraMoveSpeed = 120.0f;

	[SerializeField]
	// GameObject for the camera to follow.
	private GameObject CameraFollowObj;

	[SerializeField]
	// Clamps the max up angle.
	private float MaxClampAngle = 80.0f;

	[SerializeField]
	// Clamps the min down angle.
	private float MinClampAngle = -45.0f;

	[SerializeField]
	// Used to apply the rotation with the input from the analog stick or mouse.
	private float inputSensitivity = 150.0f;

	[SerializeField]
	// Mouse X input used for looking around.
	private float mouseX;

	[SerializeField]
	// Mouse Y input used for looking around.
	private float mouseY;

	[SerializeField]
	// Final input for rotation on the X axis.
	private float finalInputX;

	[SerializeField]
	// Final input for rotation on the Y axis.
	private float finalInputZ;

	[SerializeField]
	// Rotation on the Y.
	private float rotY = 0.0f;

	[SerializeField]
	// Rotation on the X.
	private float rotX = 0.0f;

	[SerializeField]
	// Speed of the lerp rotation.
	private float m_fLerpSpeed = 0.1f;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rot.x = 20.0f;
		rotY = rot.y;
		rotX = rot.x;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, Updates the camera's rotation.
	//--------------------------------------------------------------------------------
	void Update ()
	{
		// TODO: Set the camera up to move behind the player when they are moving and the camera is not moving.
		// TODO: Make the camera do a smooth movetowards when the player falls.
		// TODO: Make a first person camera.

		// Setup the rotation of the right sticks axis.
		float inputX = Input.GetAxis ("RightStickHorizontal");
		float inputZ = Input.GetAxis ("RightStickVertical");
		// Setup the rotation of the mouse axis.
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;
		// Sets the rotation based on the input and sensitivity.
		rotY += finalInputX * inputSensitivity * Time.deltaTime;
		rotX += finalInputZ * inputSensitivity * Time.deltaTime;
		// Clamps the rotation so gimble lock does not occur including other problems.
		rotX = Mathf.Clamp (rotX, MinClampAngle, MaxClampAngle);

		//if (((XCI.GetAxis(XboxAxis.LeftStickX) >= 0.40f || XCI.GetAxis(XboxAxis.LeftStickY) >= 0.40f) 
		//	|| (XCI.GetAxis(XboxAxis.LeftStickX) <= -0.40f || XCI.GetAxis(XboxAxis.LeftStickY) <= -0.40f)) // Left Stick moving
		//	&& ((XCI.GetAxis(XboxAxis.RightStickX) <= 0.40f && XCI.GetAxis(XboxAxis.RightStickY) <= 0.40f) 
		//	&& (XCI.GetAxis(XboxAxis.RightStickX) >= -0.40f && XCI.GetAxis(XboxAxis.RightStickY) >= -0.40f))) // Right Stick not moving
		//{
		//	rotX = 20.0f;
		//	rotY = CameraFollowObj.transform.rotation.eulerAngles.y;
		//	m_fLerpSpeed = 0.05f;
		//	m_bWorldSpace = true;
		//}
		//else
		//{
		//	m_bWorldSpace = false;
		//}

		// If the right stick button is pressed then set the camera to be behind the player.
		if(XCI.GetButtonDown(XboxButton.RightStick))
		{
			rotX = 20.0f;
			rotY = CameraFollowObj.transform.rotation.eulerAngles.y;
		}
		// Set localRotation to rotx and roty.
		Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
		// Lerp between current rotation to new rotation.
		transform.rotation = Quaternion.Lerp( transform.rotation, localRotation, Time.time * m_fLerpSpeed);

	}

	//--------------------------------------------------------------------------------
	// LateUpdate is called after all other update-functions, Updates camera position.
	//--------------------------------------------------------------------------------
	void LateUpdate ()
	{
		// Updates the camera's position.
		CameraUpdater ();
	}

	//--------------------------------------------------------------------------------
	// Updates the players position based on the target transform.
	//--------------------------------------------------------------------------------
	void CameraUpdater()
	{
		// Set the target object to the follow object.
		Transform target = CameraFollowObj.transform;

		// Move towards the game object that is the target.
		float step = CameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
