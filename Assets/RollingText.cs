using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingText : MonoBehaviour {
    public Transform rollingCredits;

    public float rollSpeed = 5f; 

	// Use this for initialization
	void Start () {
        StartCoroutine(CreditsOver());
	}
	
	// Update is called once per frame
	void Update () {
        rollingCredits.Translate(0, rollSpeed * Time.deltaTime, 0); 
	}

    IEnumerator CreditsOver()
    {
        yield return new WaitForSeconds(65f);
        SceneManager.LoadScene("Main Menu");
    }

}
