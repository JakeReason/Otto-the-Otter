using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheSnow : MonoBehaviour {

    public GameObject snowDome;
    public ParticleSystem snow;
    public GameObject leafs;
    public AudioSource summer;
    public AudioSource winter;

	// Use this for initialization
	void Start () {

        winter.enabled = false;
        snowDome.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        summer.enabled = false;
        winter.enabled = true;
        snowDome.SetActive(true);
        snow.Play();
        leafs.SetActive(false);
    }


}
