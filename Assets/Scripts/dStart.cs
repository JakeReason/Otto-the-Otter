//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dStart : MonoBehaviour {

    public GameObject gameManager;
    private StartGame gameStartScript;

	// Use this for initialization
	void Awake () {
        gameStartScript = gameManager.GetComponent<StartGame>();
	}
	

    public void DoorStart()
    {
        gameStartScript.StartDoor();
    }

    public void SystemsGo()
    {
        gameStartScript.StartSystems();
    }

}
