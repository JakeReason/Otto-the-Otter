using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNext : MonoBehaviour {

    public GameObject m_fadeToBlack;
    public Animator endGameDoor;
    public Animator playerAnim;
    public GameObject freelookCamera;
    public GameObject endGameCamera;
    public GameObject winterZone;
    public Transform centreDoor;
    public AudioSource doorOpen;
    public AudioClip bliz;
    public AudioClip door;

    public GameObject m_Player;
    private Player playerScript;

    private bool startMove;
	private bool loadNext;


    public int speed = 1;



    private DeathFade m_deathFade;


    private void Awake()
    {
        startMove = false;
		loadNext = false;
		playerScript = m_Player.GetComponent<Player>();
        m_deathFade = m_fadeToBlack.GetComponent<DeathFade>();
        endGameCamera.SetActive(false);
        if(winterZone != null)
        {
        winterZone.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			loadNext = true;
			endGameDoor.SetBool("GameEnd", true);
            endGameCamera.SetActive(true);
            freelookCamera.SetActive(false);
            doorOpen.PlayOneShot(door);
            playerScript.enabled = false;
            playerAnim.SetBool("Landing", true);
        }
    }


    private void WhatsNext()
    {
		loadNext = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void FadeToBlack()
    {
        m_deathFade.DoFadeIn();
    }

    private void HereComesTheSnow()
    {
        if(winterZone!= null)
        {
        winterZone.SetActive(true);
        }
        if(bliz != null)
        {
            doorOpen.PlayOneShot(bliz);
        }
    }

    private void OttoStart()
    {
        startMove = true;
    }


    private void Update()
    {
        if(startMove == true)
        {
            m_Player.transform.LookAt(centreDoor);
            m_Player.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            playerAnim.SetBool("Running", true);
        }

		if (!startMove && loadNext)
		{
			playerAnim.SetBool("Running", false);
			playerAnim.SetBool("Grapple", false);
			playerAnim.SetBool("Jumping", false);
			playerAnim.SetBool("Falling", false);
			playerAnim.SetBool("Landing", false);
			playerAnim.SetBool("Bounce", false);
			playerAnim.SetBool("Damaged", false);
			playerAnim.SetBool("Dying", false);
			playerAnim.SetBool("Waiting", false);
			playerAnim.SetBool("Respawn", false);
		}
	}

	public bool GetStartMove()
	{
		return startMove;
	}

	public bool GetLoadNext()
	{
		return loadNext;
	}
}
