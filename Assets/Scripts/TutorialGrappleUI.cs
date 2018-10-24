using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrappleUI : MonoBehaviour {
    public GameObject grappleMe;

	// Use this for initialization
	void Start () {
        grappleMe.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HookDetector")
        {
            grappleMe.SetActive(true);
        }


    }

    private void OnTriggerExit (Collider other)
    {
        if(other.tag == "HookDetector")
        {
            grappleMe.SetActive(false);
        }
    }

}
