using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public GameObject death;
    public GameObject previousDeath;


	// Use this for initialization
	void Start () {
        death.SetActive(false);


		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            previousDeath.SetActive(false);
            death.SetActive(true);
        }
    }

}
