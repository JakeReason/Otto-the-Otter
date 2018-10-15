using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheSnow : MonoBehaviour {

    public GameObject snowDome;
    public ParticleSystem snow;
    public GameObject leafs;


	// Use this for initialization
	void Start () {

        snowDome.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        snowDome.SetActive(true);
        snow.Play();
        leafs.SetActive(false);
    }


}
