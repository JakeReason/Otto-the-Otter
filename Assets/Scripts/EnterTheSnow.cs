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

    // Use this for initialization
    void Start () {

        winter.enabled = false;
        snowDome.SetActive(false);
        m_postBehaviour = camera.GetComponent<PostProcessingBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        summer.enabled = false;
        winter.enabled = true;
        snowDome.SetActive(true);
        snow.Play();
        leafs.SetActive(false);
        //change postporccs
        m_postBehaviour.profile = m_postProcessingProfile;
    }
}
