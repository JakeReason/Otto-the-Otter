﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CineCamera : MonoBehaviour {

	public GameObject vcam;
	public GameObject freeLook;
	public GameObject yesCamera;
	public GameObject noCamera;
	private bool timeLeft = true;
	public GameObject playerModel;
	private Player playerScript;
	private bool playerRun = true;
	public Animator m_Animator;

	//sets the active of the two UI elements to False
	void Awake()
	{
		playerScript = playerModel.GetComponent<Player>();


		if (!playerScript)
		{
			Debug.Log("No Player Script!");
		}

		yesCamera.gameObject.SetActive (false);
		noCamera.gameObject.SetActive (false);
	}

	//When the player enters the trigger it checks to see if it is the player then calls for the camera to switch with camera 2
	//then sets the timeleft to true, and turns the player controller off so the player can't move,
	//aswell as starting a timer for the player controller
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") 
		{
			m_Animator.SetBool("Walking", false);
			switchToCine ();
			timeLeft = true;
			playerRun = false;
			playerScript.enabled = false;
			StartCoroutine (playerWait ());
		}
	}

	//when the player leaves it turns the UI off and starts a timer for the UI to eventually turn off

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") 
		{
			switchOffCine ();
			noCamera.gameObject.SetActive (false);
			StartCoroutine (updateOff ());
		}
	}
	//switches from camera 1 to camera 2
	public void switchToCine()
	{
		vcam.gameObject.SetActive (true);
		freeLook.gameObject.SetActive (false);
		noCamera.gameObject.SetActive(true);
	}
	//switches from camera 2 to camera 1
	public void switchOffCine()
	{
		freeLook.gameObject.SetActive (true);
		vcam.gameObject.SetActive (false);
		yesCamera.gameObject.SetActive(true);
	}

	//checks to see if the player has been unable to move for longer than 2 seconds and if the player
	//has left the trigger box more than 3 seconds ago
	void LateUpdate ()
	{
		if (timeLeft == false)
		{
			yesCamera.gameObject.SetActive (false);
			Destroy (this);
		}

		if (playerRun == true)
		{
			playerScript.enabled = true;
		}
	}
	//waits for 3 seconds then turns the UI off and stops coroutine
	private IEnumerator updateOff()
	{
		yield return new WaitForSeconds (3.0f);
		timeLeft = false;
		StopCoroutine (updateOff ());

	}
	//waits for 2 seconds and then turns the player script back on
	private IEnumerator playerWait()
	{
		yield return new WaitForSeconds (2.5f);
		playerRun = true;
		StopCoroutine (playerWait ());

	}

}
