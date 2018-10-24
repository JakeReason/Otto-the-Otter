using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class StartGame : MonoBehaviour {
    public GameObject doorCam;
    public GameObject playerCam;
    public GameObject gameCanvus;
    public GameObject m_player;


    private bool animOver;
    private Player m_playerScript;
    // Use this for initialization
    void Start () {
        m_playerScript = m_player.GetComponent<Player>();

        m_playerScript.enabled = false;
        playerCam.SetActive(false);
        doorCam.SetActive(true);
        gameCanvus.SetActive(false);
        StartCoroutine(gameStart());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Skip"))
        {
            doorCam.SetActive(false);
            playerCam.SetActive(true);
            gameCanvus.SetActive(true);
            m_playerScript.enabled = true;
            Destroy(this);
        }
    } 

    private IEnumerator gameStart()
    {
        yield return new WaitForSeconds(25.0f);
        doorCam.SetActive(false);
        playerCam.SetActive(true);
        gameCanvus.SetActive(true);
        m_playerScript.enabled = true;
        Destroy(this);
    }
}
