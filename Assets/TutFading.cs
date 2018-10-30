using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutFading : MonoBehaviour {
    public GameObject gameManager;
    private CameraTUT gameManagerScript;

	// Use this for initialization
	void Awake () {
        gameManagerScript = gameManager.GetComponent<CameraTUT>();
	}
	
	// Update is called once per frame
	void StartFade () {
        gameManagerScript.delay();
	}
}
