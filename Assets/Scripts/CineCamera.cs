using System.Collections;
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
	public bool waitfor = false;




	void Awake()
	{
		yesCamera.gameObject.SetActive (false);
		noCamera.gameObject.SetActive (false);
	}


	void OnTriggerEnter(Collider other){
		switchToCine ();
		waitfor = true;
	}

	void OnTriggerExit(Collider other){
		switchOffCine ();
		noCamera.gameObject.SetActive (false);
		StartCoroutine (updateOff());

	}

	public void switchToCine()
	{
		vcam.gameObject.SetActive (true);
		freeLook.gameObject.SetActive (false);
		noCamera.gameObject.SetActive(true);
	}

	public void switchOffCine()
	{
		freeLook.gameObject.SetActive (true);
		vcam.gameObject.SetActive (false);
		yesCamera.gameObject.SetActive(true);
	}


	void LateUpdate ()
	{
		if (waitfor == false)
		{
			yesCamera.gameObject.SetActive (false);
		}
	}

	IEnumerator updateOff()
	{
		yield return new WaitForSeconds (3.0f);
		waitfor = false;

	}


}
