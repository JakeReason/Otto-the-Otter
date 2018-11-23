//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretLife : MonoBehaviour {

    public GameObject secretPath;


	// Use this for initialization
	void Awake () {
        secretPath.SetActive(false);
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SecretStart());
        }
    }


    IEnumerator SecretStart()
    {
        yield return new WaitForSeconds(2f);
        secretPath.SetActive(true);
    }

}
