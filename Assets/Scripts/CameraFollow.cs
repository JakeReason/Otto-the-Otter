//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
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

	public float m_fTurnSmoothSpeed = 0.5f;

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
	void FixedUpdate ()
	{
		// TODO: Smooth the camera collision with teh ground.
		// TODO: Make the camera do a smooth movetowards when the player falls.
		// TODO: Fix the diagonal distance bug. Check if fixed should be fixed.
		Debug.Log("X");
		Debug.Log(XCI.GetAxisRaw(XboxAxis.LeftStickX));
		Debug.Log("Y");
		Debug.Log(XCI.GetAxisRaw(XboxAxis.LeftStickY));
		CameraInput();
		// TODO: Slowly rotate the camera when the player runs away left and right aswell as towards. 
		// May not be needed if this works.
		// Set finalinput to euqual a value so the camera slowly turns as the player runs.
		// use velocity to determine if the player os moving.
		// make the camera slow down when hitting the angle clamps
		if (XCI.GetAxis(XboxAxis.LeftStickY) > 0 && XCI.GetAxis(XboxAxis.LeftStickX) < 0 
			&& XCI.GetAxis(XboxAxis.RightStickX) == 0 && XCI.GetAxis(XboxAxis.RightStickY) == 0)
		{
			finalInputX = -m_fTurnSmoothSpeed;
			
		}
		else if (XCI.GetAxis(XboxAxis.LeftStickY) > 0 && XCI.GetAxis(XboxAxis.LeftStickX) > 0
			&& XCI.GetAxis(XboxAxis.RightStickX) == 0 && XCI.GetAxis(XboxAxis.RightStickY) == 0)
		{
			finalInputX = m_fTurnSmoothSpeed;
		}
		else if (XCI.GetAxis(XboxAxis.LeftStickY) < 0 && XCI.GetAxis(XboxAxis.LeftStickX) < 0
			&& XCI.GetAxis(XboxAxis.RightStickX) == 0 && XCI.GetAxis(XboxAxis.RightStickY) == 0)
		{
			finalInputX = -m_fTurnSmoothSpeed;
		}
		else if (XCI.GetAxis(XboxAxis.LeftStickY) < 0 && XCI.GetAxis(XboxAxis.LeftStickX) > 0
			&& XCI.GetAxis(XboxAxis.RightStickX) == 0 && XCI.GetAxis(XboxAxis.RightStickY) == 0)
		{
			finalInputX = m_fTurnSmoothSpeed;
		}

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

	void CameraInput()
	{
		// Setup the rotation of the right sticks axis.
		float inputX = Input.GetAxis("RightStickHorizontal");
		float inputZ = Input.GetAxis("RightStickVertical");
		// Setup the rotation of the mouse axis.
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");
		// Sets the final input for rotation.
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;
	}

	void SetRotation()
	{
		// Sets the rotation based on the input and sensitivity.
		rotY += finalInputX * inputSensitivity * Time.deltaTime;
		rotX += finalInputZ * inputSensitivity * Time.deltaTime;
		// Clamps the rotation so gimble lock does not occur including other problems.
		rotX = Mathf.Clamp(rotX, MinClampAngle, MaxClampAngle);
	}

	void RotateCamera()
	{
		// Set localRotation to rotx and roty.
		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		// Lerp between current rotation to new rotation.
		transform.rotation = Quaternion.Lerp(transform.rotation, localRotation, m_fLerpSpeed * Time.deltaTime);
	}

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
