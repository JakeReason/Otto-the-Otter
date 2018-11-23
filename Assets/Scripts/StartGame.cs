//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class StartGame : MonoBehaviour {
    public GameObject doorCam;
    public GameObject playerCam;
    public GameObject startDoor;
    public GameObject brokenDoor;
    public GameObject gameCanvus;
    public GameObject m_player;
    public Animator doorAnim;
    public Animator brookanim;


    private bool animOver;
    private Player m_playerScript;
    // Use this for initialization
    void Awake()
    {
        m_playerScript = m_player.GetComponent<Player>();

        m_playerScript.enabled = false;
        playerCam.SetActive(false);
        doorCam.SetActive(true);
        gameCanvus.SetActive(false);
        doorAnim.SetBool("Close", false);
        brookanim.SetBool("StartFade", false);
        brokenDoor.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Skip"))
        {
            doorCam.SetActive(false);
            playerCam.SetActive(true);
            gameCanvus.SetActive(true);
            StartCoroutine(SkipAnim());
        }
    } 
    public void StartSystems()
    {
        doorCam.SetActive(false);
        playerCam.SetActive(true);
        gameCanvus.SetActive(true);
        m_playerScript.enabled = true;
        brokenDoor.SetActive(true);
        startDoor.SetActive(false);
        brookanim.SetBool("StartFade", true);
        Destroy(this);
    }


    public void StartDoor()
    {
        doorAnim.SetBool("Close", true);
    }
  
    private IEnumerator SkipAnim()
    {
        yield return new WaitForSeconds(2f);
        brokenDoor.SetActive(true);
        startDoor.SetActive(false);
        brookanim.SetBool("StartFade", true);
        Destroy(this);
        m_playerScript.enabled = true;
    }

    public void GoodbyeDoor()
    {
        Destroy(brokenDoor);
    }

}
