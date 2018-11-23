//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {

    public GameObject freeLook;
    public GameObject trackNDoly;


	// Use this for initialization
	void Start () {
        trackNDoly.SetActive(false);


		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trackNDoly.SetActive(true);
            freeLook.SetActive(false);

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            trackNDoly.SetActive(false);
            freeLook.SetActive(true);

        }
    }
}
