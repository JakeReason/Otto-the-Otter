using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class EnterTheSnow : MonoBehaviour {

    public GameObject snowDome;
    public ParticleSystem snow;
    public GameObject leafs;
    public AudioSource summer;
    public AudioSource winter;
    public GameObject camera;
    public PostProcessingProfile m_postProcessingProfile;
    private PostProcessingBehaviour m_postBehaviour;
    public GameObject cameraFrost;
    public Animator frostedAnim;
    public AudioSource freeze;
    public GameObject playerCamera;

    // Use this for initialization
    void Start () {
        frostedAnim.SetBool("Base", true);
        winter.enabled = false;
        snowDome.SetActive(false);
        m_postBehaviour = camera.GetComponent<PostProcessingBehaviour>();
        cameraFrost.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        frostedAnim.SetBool("Frosty", true);
        summer.enabled = false;
        winter.enabled = true;
        freeze.enabled = true;
        snowDome.SetActive(true);
        snow.Play();
        leafs.SetActive(false);
        cameraFrost.SetActive(true);
        playerCamera.SetActive(false);
    }

    public void winterShadder()
    {
        //cameraFrostScript.GetComponent<FrostEffect>().enabled = false;
        m_postBehaviour.profile = m_postProcessingProfile;
    }

    public void backToCamera()
    {
        cameraFrost.SetActive(false);
        playerCamera.SetActive(true);
    }

}
