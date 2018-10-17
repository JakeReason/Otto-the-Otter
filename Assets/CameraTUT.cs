using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTUT : MonoBehaviour {
    public GameObject gameUI;
    public GameObject cameraUI;
    public GameObject m_player;

    private Player m_playerScript;

    private void Awake()
    {
        m_playerScript = m_player.GetComponent<Player>();
        m_playerScript.enabled = false;
        gameUI.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetButtonDown("Skip"))
        {
            gameUI.SetActive(true);
            m_playerScript.enabled = true;
            cameraUI.SetActive(false);
        }
    }



}
