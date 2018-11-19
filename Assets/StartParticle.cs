using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticle : MonoBehaviour {
    public ParticleSystem winterBliz;


	// Use this for initialization
	void Start () {
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            winterBliz.Play(true);
        }
    }


}
