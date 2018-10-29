﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTUT : MonoBehaviour {
    public GameObject gameUI;
    public GameObject cameraUI;
    public GameObject m_player;
    public GameObject gifCam;
    public GameObject freeLook;

    public Animator fading;

    private Player m_playerScript;

    private void Awake()
    {
        m_playerScript = m_player.GetComponent<Player>();
        m_playerScript.enabled = false;
        gameUI.SetActive(false);
        gifCam.SetActive(true);
        freeLook.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetButtonDown("Skip"))
        {
            StartCoroutine(delay());
            fading.SetBool("Fading", true);
            
        }
    }
   private IEnumerator delay()
    {
        yield return new WaitForSeconds(2.0f);
        gameUI.SetActive(true);
        gifCam.SetActive(false);
        freeLook.SetActive(true);
        cameraUI.SetActive(false);
        m_playerScript.enabled = true;
        StopCoroutine(delay());
        Destroy(this);
    }

}