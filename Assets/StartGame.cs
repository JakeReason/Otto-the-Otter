using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    public GameObject doorCam;
    public GameObject playerCam;
    public GameObject gameCanvus;

    private bool animOver;
    // Use this for initialization
    void Start () {
        playerCam.SetActive(false);
        doorCam.SetActive(true);
        gameCanvus.SetActive(false);
        StartCoroutine(gameStart());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private IEnumerator gameStart()
    {
        yield return new WaitForSeconds(25.0f);
        doorCam.SetActive(false);
        playerCam.SetActive(true);
        gameCanvus.SetActive(true);
        Destroy(this);
    }
}
