using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFade : MonoBehaviour {

    public GameObject startButton;
    private MainMenu startButtonScript;

	// Use this for initialization
	void Awake () {
        startButtonScript = startButton.GetComponent<MainMenu>();
    }

  void Fadeaway()
    {
        startButtonScript.StartGame(1);
    }
}
