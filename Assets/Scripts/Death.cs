//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public Collider death;
    public Collider previousDeath;


	// Use this for initialization
	void Start () {
        death.enabled = false;


		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            death.enabled = true;
            death.isTrigger = true;
            previousDeath.enabled = false;
        }
    }

}
