using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCamScript : MonoBehaviour {

    // Collectable manager GameObject used to get access to the collectable manager.
    private GameObject m_collectableManager;

    // Collectable manager Script used to get access to the collectable manager script.
    private CollectableManager m_CM;

    public GameObject playerCam;
    public GameObject doorCam;
    public GameObject gameCanvus;

    public Animator doorAnim;
    public Animator doorAnim2;
    public Animator doorAnim3;


    // Use this for initialization
    void Start () {
        // Gets reference to the collectable manager gameObject.
        m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");
        m_CM = m_collectableManager.GetComponent<CollectableManager>();
        doorCam.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(m_CM.m_fFlowersCollected >= 2)
        {
            StartCoroutine(ThatEnoughDoor());
            playerCam.SetActive(false);
            doorCam.SetActive(true);
            gameCanvus.SetActive(false);
            doorAnim.SetBool("Is In View", true);
            doorAnim2.SetBool("Is In View", true);
            doorAnim3.SetBool("Is In View", true);
        }
    }

    private IEnumerator ThatEnoughDoor()
    {
        yield return new WaitForSeconds(6f);
        playerCam.SetActive(true);
        doorCam.SetActive(false);
        gameCanvus.SetActive(true);
        StopCoroutine(ThatEnoughDoor());
        Destroy(this);
    }

}
