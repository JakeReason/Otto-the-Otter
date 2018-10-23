using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : MonoBehaviour {
    public GameObject iceCreamUI;
    public AudioSource audioSource;
    public AudioClip pickUp;
    public int speed = 1;
    private bool isPickedup;

	// Use this for initialization
	void Start () {
        iceCreamUI.SetActive(false);
		
	}

    private void Update()
    {
        transform.Rotate(Vector3.down * speed * Time.deltaTime, Space.World);



        if (audioSource)
        {
            if(!audioSource.isPlaying && isPickedup)
            {
                gameObject.SetActive(false);
            }
            else if (audioSource.isPlaying && isPickedup)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            iceCreamUI.SetActive(true);
            audioSource.Play();
            isPickedup = true;
        }
    }
}
