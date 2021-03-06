﻿//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas. NOT BEING USED.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	// Camera's move speed.
	private float CameraMoveSpeed = 120.0f;

	[SerializeField]
	// GameObject for the camera to follow.
	private GameObject CameraFollowObj;

	[SerializeField]
	// Clamps the min down angle.
	private float AngleHieght = 30.0f;

	// Clamps the max up angle.
	private float MaxClampAngleX = 80.0f;

	// Clamps the min down angle.
	private float MinClampAngleX = -45.0f;

	[SerializeField]
	// Used to apply the rotation with the input from the analog stick or mouse.
	private float inputSensitivity = 150.0f;

	// Mouse X input used for looking around.
	private float mouseX;

	// Mouse Y input used for looking around.
	private float mouseY;

	// Final input for rotation on the X axis.
	private float finalInputX;

	// Final input for rotation on the Y axis.
	private float finalInputZ;

	// Rotation on the Y.
	private float rotY = 0.0f;

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
		MaxClampAngleX = AngleHieght;
		MinClampAngleX = AngleHieght;
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, Updates the camera's rotation.
	//--------------------------------------------------------------------------------
	void FixedUpdate ()
	{
		CameraInput();
		SetRotation();
		RotateCamera();
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
		transform.position = Vector3.Lerp (transform.position, target.position, step);
	}

	//--------------------------------------------------------------------------------
	// Sets up the input from mthe mouse and controller
	//--------------------------------------------------------------------------------
	void CameraInput()
	{
		// Setup the rotation of the right sticks axis.
		float inputX = Input.GetAxis("RightStickHorizontal");
		float inputZ = Input.GetAxis("RightStickVertical");
		// Setup the rotation of the mouse axis.
		mouseX = Input.GetAxis("MouseX");
		mouseY = Input.GetAxis("MouseY");
		// Sets the final input for rotation.
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;
	}

	//--------------------------------------------------------------------------------
	// Sets the rotation of the camera based on the input.
	//--------------------------------------------------------------------------------
	void SetRotation()
	{
		// Sets the rotation based on the input and sensitivity.
		rotY += finalInputX * inputSensitivity * Time.deltaTime;
		rotX += finalInputZ * inputSensitivity * Time.deltaTime;
		// Clamps the rotation so gimble lock does not occur including other problems.
		rotX = Mathf.Clamp(rotX, MinClampAngleX, MaxClampAngleX);
	}

	//--------------------------------------------------------------------------------
	// Rotates the camera.
	//--------------------------------------------------------------------------------
	void RotateCamera()
	{
		// Set localRotation to rotx and roty.
		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		// Lerp between current rotation to new rotation.
		transform.rotation = Quaternion.Lerp(transform.rotation, localRotation, m_fLerpSpeed * Time.deltaTime);
	}

	//--------------------------------------------------------------------------------
	// Resets the camera behind the player.
	//--------------------------------------------------------------------------------
	private void ResetCamera()
	{
		// If the right stick button is pressed then set the camera to be behind the player.
		if (XCI.GetButtonDown(XboxButton.RightStick))
		{
			rotX = 20.0f;
			rotY = CameraFollowObj.transform.rotation.eulerAngles.y;
		}
	}
}
