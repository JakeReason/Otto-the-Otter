using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CineCamera : MonoBehaviour {

	public GameObject player;
	public GameObject vcam;
	public GameObject freeLook;
	public GameObject yesCamera;
	public GameObject noCamera;
	//public GameObject playerModel;
	//private Player playerScript;
	public Animator m_Animator;
	public Collider thisColider;
    public AudioSource soundBuild;
    public AudioClip treebuild;

	private bool cineOver;
	private bool timeLeft;
	private bool playerRun;

	private Player playerScript;
	//private bool m_bPlayerWait;

	//sets the active of the two UI elements to False
	void Awake()
	{
		playerScript = player.GetComponent<Player>();

		yesCamera.gameObject.SetActive (false);
		noCamera.gameObject.SetActive (false);
        vcam.SetActive(false);

		cineOver = false;
		timeLeft = true;
		playerRun = true;

		//m_bPlayerWait = false;
	}

	//When the player enters the trigger it checks to see if it is the player then calls for the camera to switch with camera 2
	//then sets the timeleft to true, and turns the player controller off so the player can't move,
	//aswell as starting a timer for the player controller
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) 
		{
			//_Animator.SetBool("Walking", false);
			switchToCine ();
			timeLeft = true;
			playerRun = false;
			playerScript.SetCutscene(true);
			//m_bPlayerWait = true;
			//playerScript.enabled = false;
			StartCoroutine (playerWait ());
            soundBuild.PlayOneShot(treebuild, 0.7f);
            StartCoroutine(bigTreeCine());

		}

	}

	//when the player leaves it turns the UI off and starts a timer for the UI to eventually turn off

	//void OnTriggerExit(Collider other){
	//	if (other.tag == "Player") 
	//	{
	//		switchOffCine ();
	//		noCamera.gameObject.SetActive (false);
	//		StartCoroutine (updateOff ());
	//		thisColider.enabled = false ;
	//	}
	//}
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
        }

		//if (playerRun == true)
		//{
            //playerScript.enabled = true;
		//}

        if (cineOver == true)
        {
            switchOffCine();
            Destroy(this.gameObject);
        }

	}
	//waits for 3 seconds then turns the UI off and stops coroutine
	private IEnumerator updateOff()
	{
		yield return new WaitForSeconds (3.0f);
		timeLeft = false;
		StopCoroutine (updateOff ());
		Destroy (this.gameObject);
	}

	//waits for 2 seconds and then turns the player script back on
	private IEnumerator playerWait()
	{
		yield return new WaitForSeconds (3.5f);
		playerRun = true;
		playerScript.SetCutscene(false);
		//m_bPlayerWait = false;
		StopCoroutine (playerWait ());
	}


    private IEnumerator bigTreeCine()
    {
        yield return new WaitForSeconds(3.5f);
        cineOver = true;
        StopCoroutine(bigTreeCine ());
    }

	//public bool GetPlayerWait()
	//{
	//	return m_bPlayerWait;
	//}
}
